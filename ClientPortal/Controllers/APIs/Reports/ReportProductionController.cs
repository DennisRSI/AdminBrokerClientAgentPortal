using ClientPortal.Models;
using Codes1.Service.Domain;
using Codes1.Service.Interfaces;
using Codes1.Service.Services;
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
        private readonly IReport1Service _reportService;
        private readonly IAccount1Service _accountService;
        private readonly ICode1Service _context;
        private readonly IAccountQueryFactory _accountQueryFactory;
        private readonly IReportProduction1Service _reportProductionService;

        public ReportProductionController(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager,
                                            IReport1Service reportService, IAccount1Service accountService, ICode1Service context,
                                            IReportProduction1Service reportProductionService, IAccountQueryFactory accountQueryFactory)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _reportService = reportService;
            _accountService = accountService;
            _context = context;
            _reportProductionService = reportProductionService;
            _accountQueryFactory = accountQueryFactory;
        }

        [HttpGet("load/{type}")]
        public async Task<IActionResult> LoadAsync(string type)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var accountQuery = _accountQueryFactory.GetAccountQuery(user.BrokerId, user.AgentId, user.ClientId);

            var model = new ProductionLoadViewModel
            {
                UserType = user.Role,
                ReportType = GetReportType(type),
                Brokers = accountQuery.GetBrokers().Select(b => new SelectListItem() { Value = b.Id.ToString(), Text = b.FullName }).OrderBy(o => o.Text),
                Clients = accountQuery.GetClients().Select(c => new SelectListItem() { Value = c.Id.ToString(), Text = c.CompanyName }).OrderBy(o => o.Text),
                Agents = accountQuery.GetAgents().Select(a => new SelectListItem() { Value = a.Id.ToString(), Text = a.FullName }).OrderBy(o => o.Text),
                Campaigns = accountQuery.GetCampaigns().Select(a => new SelectListItem() { Value = a.Id.ToString(), Text = a.CompanyName }).OrderBy(o => o.Text)
            };

            return PartialView("Load", model);
        }

        [HttpGet("gethtmlsummary/{type}/{id}/{name}/{paymentStatus}/{checkOutStart}/{checkOutEnd}/{bookingStart}/{bookingEnd}")]
        public async Task<IActionResult> GetHtmlSummary(
            string type, int id, string name, string paymentStatus,
            string checkOutStart, string checkOutEnd, string bookingStart, string bookingEnd
            )
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var accountQuery = _accountQueryFactory.GetAccountQuery(user.BrokerId, user.AgentId, user.ClientId);
            IEnumerable<int> accounts = null;

            switch (type)
            {
                case "broker":
                    accounts = accountQuery.GetBrokers().Select(b => b.Id);
                    break;

                case "agent":
                    accounts = accountQuery.GetAgents().Select(a => a.Id);
                    break;

                case "client":
                    //accounts = accountQuery.GetClients().Select(c => c.Id);
                    break;

                case "campaign":
                    //accounts = accountQuery.GetCampaigns().Select(c => c.Id);
                    break;

                case "source":
                    accounts = new List<int> { 0 };
                    break;
            }

            var query = new ProductionSummaryQuery()
            {
                PaymentStatus = paymentStatus,
                BookingStartDate = DateTime.ParseExact(bookingStart, "yyyy-MM-dd", null),
                BookingEndDate = DateTime.ParseExact(bookingEnd, "yyyy-MM-dd", null),
                CheckOutStartDate = DateTime.ParseExact(checkOutStart, "yyyy-MM-dd", null),
                CheckOutEndDate = DateTime.ParseExact(checkOutEnd, "yyyy-MM-dd", null),
                QueryType = type,
                AccountIds = accounts,
                AgentId = user.AgentId,
                ClientId = user.ClientId,
                BrokerId = user.BrokerId,
                Role = user.Role
            };

            ProductionResultSummaryViewModel model = null;

            if (type == "campaign")
            {
                //model = await _reportProductionService.GetProductionResultCampaignAsync(query);
                //model = await _reportProductionService.V2_GetProductionResultCampaignAsync(0, 90000000, null, user.BrokerId, user.ClientId, user.AgentId,
                //    DateTime.ParseExact(bookingStart, "yyyy-MM-dd", null),
                //    DateTime.ParseExact(bookingEnd, "yyyy-MM-dd", null),
                //    DateTime.ParseExact(checkOutStart, "yyyy-MM-dd", null),
                //    DateTime.ParseExact(checkOutEnd, "yyyy-MM-dd", null),  null);

                model = await _reportProductionService.V2_GetProductionResultCampaignAsync(0, 90000000, null, user.BrokerId, user.ClientId, user.AgentId,
                        null,
                        null,
                        null,
                        null);
            }
            else if(type == "client")
            {
                model = await _reportProductionService.V2_getProductionResultClientAsync(0, 90000000, null, user.BrokerId, user.ClientId, user.AgentId,
                        null,
                        null,
                        DateTime.ParseExact(checkOutStart, "yyyy-MM-dd", null),
                        DateTime.ParseExact(checkOutEnd, "yyyy-MM-dd", null),
                        null);
            }
            else
            {
                model = await _reportService.GetProductionResultSummaryAsync(query);
            }

            model.Type = type.CapitalizeFirstLetter();
            model.AccountName = "All";

            return PartialView("HtmlSummary", model);
        }

        [HttpGet("gethtmldetail/{type}/{id}/{name}/{paymentStatus}/{checkOutStart}/{checkOutEnd}/{bookingStart}/{bookingEnd}")]
        public async Task<IActionResult> GetHtmlDetail(
            string type, int id, string name, string paymentStatus,
            string checkOutStart, string checkOutEnd, string bookingStart, string bookingEnd
            )
        {
            int? brokerId = null;
            int? agentId = null;
            int? clientId = null;
            int? campaignId = null;

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

                case "campaign":
                    campaignId = id;
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
                ClientId = clientId,
                CampaignId = campaignId
            };

            var model = await _reportService.GetProductionResultDetail(query);

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
