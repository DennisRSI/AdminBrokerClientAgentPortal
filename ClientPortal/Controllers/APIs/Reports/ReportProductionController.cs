using ClientPortal.Models;
using Codes.Service.Domain;
using Codes.Service.Interfaces;
using Codes.Service.Services;
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
    public class ReportProductionController : Controller
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IReportService _reportService;
        private readonly IAccountService _accountService;
        private readonly ICodeService _context;
        private readonly IAccountQueryFactory _accountQueryFactory;

        public ReportProductionController(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager,
                                            IReportService reportService, IAccountService accountService, ICodeService context, IAccountQueryFactory accountQueryFactory)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _reportService = reportService;
            _accountService = accountService;
            _context = context;
            _accountQueryFactory = accountQueryFactory;
        }

        [HttpGet("load/{type}")]
        public async Task<IActionResult> LoadAsync(string type)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var accountQuery = _accountQueryFactory.GetAccountQuery(user.BrokerId, user.AgentId, user.ClientId);

            var model = new ProductionLoadViewModel
            {
                ReportType = GetReportType(type),
                Brokers = accountQuery.GetBrokers().Select(b => new SelectListItem() { Value = b.Id.ToString(), Text = b.FullName }),
                Clients = accountQuery.GetClients().Select(c => new SelectListItem() { Value = c.Id.ToString(), Text = c.CompanyName }),
                Agents = accountQuery.GetAgents().Select(a => new SelectListItem() { Value = a.Id.ToString(), Text = a.FullName }),
                Campaigns = accountQuery.GetCampaigns().Select(a => new SelectListItem() { Value = a.Id.ToString(), Text = a.CompanyName }),
            };

            return PartialView("Load", model);
        }

        [HttpGet("gethtmldetail/{type}/{id}/{name}/{paymentStatus}/{checkOutStart}/{checkOutEnd}/{bookingStart}/{bookingEnd}")]
        public async Task<IActionResult> GetHtmlDetail(
            string type, int id, string name, int paymentStatus,
            string checkOutStart, string checkOutEnd, string bookingStart, string bookingEnd
            )
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var accountQuery = _accountQueryFactory.GetAccountQuery(user.BrokerId, user.AgentId, user.ClientId);

            int? brokerId = null;
            int? agentId = null;
            int? clientId = null;

            switch (type)
            {
                case "broker":
                    brokerId = id;
                    break;

                case "agent":
                    agentId = id;
                    break;

                case "client":
                    clientId = id;
                    break;
            }

            var query = new ProductionDetailQuery()
            {
                BookingStartDate = DateTime.ParseExact(bookingStart, "yyyy-MM-dd", null),
                BookingEndDate = DateTime.ParseExact(bookingEnd, "yyyy-MM-dd", null),
                CheckOutStartDate = DateTime.ParseExact(checkOutStart, "yyyy-MM-dd", null),
                CheckOutEndDate = DateTime.ParseExact(checkOutEnd, "yyyy-MM-dd", null),
                BrokerId = brokerId,
                AgentId = agentId,
                ClientId = clientId
            };

            var model = _reportService.GetProductionResultDetail(query);

            model.Type = type.First().ToString().ToUpper() + type.Substring(1);
            model.AccountName = name;

            return PartialView("HtmlDetail", model);
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
                        new SelectListItem() { Text = "By Client", Value = "client" },
                        new SelectListItem() { Text = "By Agent", Value = "agent" }
                    };

                case "broker":
                    return new List<SelectListItem>()
                    {
                        new SelectListItem() { Text = "By Client", Value = "client" },
                        new SelectListItem() { Text = "By Agent", Value = "agent" },
                        new SelectListItem() { Text = "By Campaign", Value = "campaign" }
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
