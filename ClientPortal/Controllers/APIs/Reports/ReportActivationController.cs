﻿using ClientPortal.Models;
using Codes1.Service.Domain;
using Codes1.Service.Interfaces;
using Codes1.Service.ViewModels;
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
    public class ReportActivationController : Controller
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IReport1Service _reportService;
        private readonly IAccount1Service _accountService;
        private readonly ICode1Service _context;
        private readonly IAccountQueryFactory _accountQueryFactory;

        public ReportActivationController(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager,
                                            IReport1Service reportService, IAccount1Service accountService, ICode1Service context, IAccountQueryFactory accountQueryFactory)
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
            ActivationLoadViewModel model = null;
            var user = await _userManager.GetUserAsync(HttpContext.User);

            switch (type.ToLower())
            {
                case "super admin":
                case "admin":
                    model = LoadAdmin();
                    break;

                case "broker":
                    model = LoadBroker(user.BrokerId);
                    break;

                case "client":
                    model = await LoadClient();
                    break;

                case "agent":
                    model = await LoadAgent();
                    break;
            };

            return PartialView("Load", model);
        }

        private ActivationLoadViewModel LoadAdmin()
        {
            ActivationLoadViewModel model = new ActivationLoadViewModel
            {
                ReportType = new List<SelectListItem>
                {
                    new SelectListItem() { Text = "By Broker", Value = "broker" },
                    new SelectListItem() { Text = "By Client", Value = "client" },
                    new SelectListItem() { Text = "By Agent", Value = "agent" }
                },
                Brokers = _accountService.GetAllBrokers().Select(a => new SelectListItem() { Value = a.Id.ToString(), Text = a.FullName }).OrderBy(o => o.Text),
                Agents = _accountService.GetAllAgents().Select(a => new SelectListItem() { Value = a.Id.ToString(), Text = a.FullName }).OrderBy(o => o.Text),
                Clients = _accountService.GetAllClients().Select(a => new SelectListItem() { Value = a.Id.ToString(), Text = a.CompanyName }).OrderBy(o => o.Text)
            };

            return model;
        }

        private ActivationLoadViewModel LoadBroker(int brokerId)
        {
            ActivationLoadViewModel model = new ActivationLoadViewModel
            {
                ReportType = new List<SelectListItem>
                {
                    new SelectListItem() { Text = "By Client", Value = "client" },
                    new SelectListItem() { Text = "By Agent", Value = "agent" }
                },
                Agents = _accountService.GetAgentsOfBroker(brokerId).OrderBy(o=> o.CompanyName).Select(a => new SelectListItem() { Value = a.Id.ToString(), Text = a.CompanyName }),
                Clients = _accountService.GetClientsOfBroker(brokerId).OrderBy(o => o.CompanyName).Select(a => new SelectListItem() { Value = a.Id.ToString(), Text = a.CompanyName })
            };

            return model;
        }

        private async Task<ActivationLoadViewModel> LoadClient()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var accountQuery = _accountQueryFactory.GetAccountQuery(user.BrokerId, user.AgentId, user.ClientId);

            var model = new ActivationLoadViewModel
            {
                ReportType = new List<SelectListItem>
                {
                    new SelectListItem() { Text = "By Campaign", Value = "campaign" }
                },
                Campaigns = accountQuery.GetCampaigns().OrderBy(o => o.CompanyName).Select(a => new SelectListItem() { Value = a.Id.ToString(), Text = a.CompanyName }),
            };

            return model;
        }

        private async Task<ActivationLoadViewModel> LoadAgent()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var accountQuery = _accountQueryFactory.GetAccountQuery(user.BrokerId, user.AgentId, user.ClientId);

            var model = new ActivationLoadViewModel
            {
                ReportType = new List<SelectListItem>
                {
                    new SelectListItem() { Text = "By Client", Value = "client" },
                },
                Clients = accountQuery.GetClients().OrderBy(o => o.CompanyName).Select(c => new SelectListItem() { Value = c.Id.ToString(), Text = c.CompanyName }),
            };

            return model;
        }

        [HttpGet("gethtml/{type}/{id}/{name}/{start}/{end}")]
        public async Task<IActionResult> GetHtml(string type, int id, string name, string start, string end)
        {
            try
            {
                var user = await _userManager.GetUserAsync(HttpContext.User);
                var accountQuery = _accountQueryFactory.GetAccountQuery(user.BrokerId, user.AgentId, user.ClientId);

                var startSplit = start.Split('-');
                var endSplit = end.Split('-');

                var model = new ActivationResultViewModel()
                {
                    Type = type.First().ToString().ToUpper() + type.Substring(1),
                    AccountName = name,
                    StartDate = $"{startSplit[1]}/{startSplit[2]}/{startSplit[0]}",
                    EndDate = $"{endSplit[1]}/{endSplit[2]}/{endSplit[0]}",
                };

                switch (type)
                {
                    case "broker":
                        var brokers = accountQuery.GetBrokers()
                            .Where(c => c.Id == id || id == 0)
                            .Select(c => new ActivationTableViewModel() { Id = c.Id, Type = type, CompanyName = c.FullName });

                        model.Tables.AddRange(brokers);
                        break;

                    case "client":
                        var clients = accountQuery.GetClients().Where(c => c.Id == id || id == 0)
                            .Select(c => new ActivationTableViewModel() { Id = c.Id, Type = type, CompanyName = c.CompanyName });

                        model.Tables.AddRange(clients);
                        break;

                    case "agent":
                        var agents = accountQuery.GetAgents().Where(a => a.Id == id || id == 0)
                            .Select(a => new ActivationTableViewModel() { Id = a.Id, Type = type, CompanyName = a.CompanyName });

                        model.Tables.AddRange(agents);
                        break;

                    case "campaign":
                        var campaigns = accountQuery.GetCampaigns().Where(c => c.Id == id || id == 0)
                            .Select(c => new ActivationTableViewModel() { Id = c.Id, Type = type, CompanyName = c.CompanyName });

                        model.Tables.AddRange(campaigns);
                        break;
                }
                return PartialView("Html", model);
            }
            catch(Exception ex)
            {
                return null;
            }

            
        }

        [HttpGet("getjson/{type}/{id}/{status}/{used}/{start}/{end}")]
        public async Task<DataTableViewModel<ActivationCardViewModel>> GetJson(string type, int id, string status, string used, string start, string end)
        {
            DataTableViewModel<ActivationCardViewModel> result = null;

            try
            {
                var user = await _userManager.GetUserAsync(HttpContext.User);

                int? agentId = null;
                int? brokerId = null;
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

                var startDate = DateTime.ParseExact(start, "yyyy-MM-dd", null);
                var endDate = DateTime.ParseExact(end, "yyyy-MM-dd", null);

                bool? isCardUsed = null;

                if (used == "Y")
                {
                    isCardUsed = true;
                }

                if (used == "N")
                {
                    isCardUsed = false;
                }

                var query = new ActivationReportViewModel()
                {
                    AgentId = agentId,
                    BrokerId = brokerId,
                    ClientId = clientId,
                    CampaignId = campaignId,
                    CampaignStatus = status,
                    IsCardUsed = isCardUsed,
                    StartDate = startDate,
                    EndDate = endDate,
                };

                result = _reportService.GetDataActivation(query);

                if (!user.IsAdmin)
                {
                    result.Data.ToList().ForEach(a => { a.Phone = String.Empty; a.Email = String.Empty; });
                }
            }
            catch (Exception ex)
            {
                string message = ex.Message;
            }

            return result;
        }
    }
}
