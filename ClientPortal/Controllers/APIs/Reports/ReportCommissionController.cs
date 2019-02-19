using ClientPortal.Models;
using Codes.Service.Domain;
using Codes.Service.Interfaces;
using Codes.Service.Services;
using Codes.Service.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClientPortal.Controllers.APIs
{
    [Authorize]
    [Route("api/[controller]")]
    public class ReportCommissionController : Controller
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IReportCommissionService _reportCommissionService;
        private readonly IAccountService _accountService;
        private readonly ICodeService _context;
        private readonly IAccountQueryFactory _accountQueryFactory;

        public ReportCommissionController(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager,
                                            IReportCommissionService reportCommissionService, IAccountService accountService, ICodeService context, IAccountQueryFactory accountQueryFactory)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _reportCommissionService = reportCommissionService;
            _accountService = accountService;
            _context = context;
            _accountQueryFactory = accountQueryFactory;
        }

        [HttpGet("load/{type}")]
        public async Task<IActionResult> LoadAsync(string type)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var accountQuery = _accountQueryFactory.GetAccountQuery(user.BrokerId, user.AgentId, user.ClientId);

            var model = new CommissionLoadViewModel
            {
                UserType = type,
                ReportType = GetReportType(type),
                Brokers = accountQuery.GetBrokers().Select(b => new SelectListItem() { Value = b.Id.ToString(), Text = b.FullName }),
                Clients = accountQuery.GetClients().Select(c => new SelectListItem() { Value = c.Id.ToString(), Text = c.CompanyName }),
                Agents = accountQuery.GetAgents().Select(a => new SelectListItem() { Value = a.Id.ToString(), Text = a.FullName }),
                Campaigns = accountQuery.GetCampaigns().Select(a => new SelectListItem() { Value = a.Id.ToString(), Text = a.CompanyName }),
            };

            if (type == "Broker")
            {
                model.Brokers = new List<SelectListItem>()
                                    {
                                        new SelectListItem() { Value = user.BrokerId.ToString(), Text = "Hidden", Selected = true }
                                    };
            }

            return PartialView("Load", model);
        }

        [HttpGet("gethtml/{type}/{id}/{name}/{paymentStatus}/{checkOutStart}/{checkOutEnd}")]
        public async Task<IActionResult> GetHtml(string type, int id, string name, string paymentStatus, string checkOutStart, string checkOutEnd)
        {
            IEnumerable<int> accounts = null;

            if (id == 0)
            {
                var user = await _userManager.GetUserAsync(HttpContext.User);
                var accountQuery = _accountQueryFactory.GetAccountQuery(user.BrokerId, user.AgentId, user.ClientId);

                switch (type)
                {
                    case "broker":
                        accounts = accountQuery.GetBrokers().Select(b => b.Id);
                        break;

                    case "agent":
                        accounts = accountQuery.GetAgents().Select(a => a.Id);
                        break;

                    case "client":
                        accounts = accountQuery.GetClients().Select(c => c.Id);
                        break;

                    case "source":
                        accounts = new List<int> { 0 };
                        break;
                }
            }
            else
            {
                accounts = new List<int> { id };
            }

            var query = new CommissionQuery
            {
                QueryType = type,
                PaymentStatus = paymentStatus,
                CheckOutStartDate = DateTime.ParseExact(checkOutStart, "yyyy-MM-dd", null),
                CheckOutEndDate = DateTime.ParseExact(checkOutEnd, "yyyy-MM-dd", null),
                AccountIds = accounts
            };

            CommissionResultViewModel model = null;

            switch (type)
            {
                case "broker":
                    model = await _reportCommissionService.GetCommissionResultBrokerAsync(query);
                    break;

                case "client":
                    model = await _reportCommissionService.GetCommissionResultClientAsync(query);
                    break;

                case "source":
                    model = await _reportCommissionService.GetCommissionResultSourceAsync(query);
                    break;
            }


            model.Type = type.CapitalizeFirstLetter();
            model.AccountName = name;

            return PartialView("Html", model);
        }

        private List<SelectListItem> GetReportType(string type)
        {
            switch (type.ToLower())
            {
                case "admin":
                    return new List<SelectListItem>()
                    {
                        new SelectListItem() { Text = "By Source", Value = "source" },
                        new SelectListItem() { Text = "By Broker", Value = "broker" },
                    };

                case "broker":
                    return new List<SelectListItem>()
                    {
                        new SelectListItem() { Text = "By Broker", Value = "broker" }
                    };

                case "agent":
                    return new List<SelectListItem>()
                    {
                        new SelectListItem() { Text = "By Client", Value = "client" },
                        new SelectListItem() { Text = "By Campaign", Value = "campaign" }
                    };

                case "client":
                    return new List<SelectListItem>()
                    {
                        new SelectListItem() { Text = "By Campaign", Value = "campaign" }
                    };
            }

            return null;
        }
    }
}
