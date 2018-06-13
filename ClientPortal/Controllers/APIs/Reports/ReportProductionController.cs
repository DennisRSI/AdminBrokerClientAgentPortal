using ClientPortal.Models;
using Codes.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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

        public ReportProductionController(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager,
                                            IReportService reportService, IAccountService accountService, ICodeService context)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _reportService = reportService;
            _accountService = accountService;
            _context = context;
        }

        [HttpGet("load/{type}")]
        public async Task<IActionResult> LoadAsync(string type)
        {
            ProductionLoadViewModel model = null;
            var user = await _userManager.GetUserAsync(HttpContext.User);

            switch (type.ToLower())
            {
                case "admin":
                    model = LoadAdmin();
                    break;

                case "broker":
                    model = LoadBroker(user.BrokerId);
                    break;

                case "agent":
                    model = LoadAgent(user.AgentId);
                    break;

                case "client":
                    model = LoadClient(user.ClientId);
                    break;
            }

            return PartialView("Load", model);
        }

        private ProductionLoadViewModel LoadAdmin()
        {
            return new ProductionLoadViewModel
            {
                ReportType = new List<SelectListItem>
                {
                    new SelectListItem() { Text = "By Source", Value = "source" },
                    new SelectListItem() { Text = "By Broker", Value = "broker" },
                    new SelectListItem() { Text = "By Client", Value = "client" },
                    new SelectListItem() { Text = "By Agent", Value = "agent" }
                },

                Brokers = _accountService.GetAllBrokers().Select(b => new SelectListItem() { Value = b.Id.ToString(), Text = b.FullName }),
                Clients = _accountService.GetAllClients().Select(c => new SelectListItem() { Value = c.Id.ToString(), Text = c.CompanyName }),
                Agents = _accountService.GetAllAgents().Select(a => new SelectListItem() { Value = a.Id.ToString(), Text = a.FullName })
            };
        }

        private ProductionLoadViewModel LoadBroker(int brokerId)
        {
            return new ProductionLoadViewModel
            {
                ReportType = new List<SelectListItem>
                {
                    new SelectListItem() { Text = "By Client", Value = "client" },
                    new SelectListItem() { Text = "By Agent", Value = "agent" },
                    new SelectListItem() { Text = "By Campaign", Value = "campaign" }
                },

                // TODO: Update these
                Clients = _accountService.GetAllClients().Select(c => new SelectListItem() { Value = c.Id.ToString(), Text = c.CompanyName }),
                Agents = _accountService.GetAllAgents().Select(a => new SelectListItem() { Value = a.Id.ToString(), Text = a.FullName }),
                Campaigns = _accountService.GetAllBrokers().Select(b => new SelectListItem() { Value = b.Id.ToString(), Text = b.FullName })
            };
        }

        private ProductionLoadViewModel LoadAgent(int agentId)
        {
            return new ProductionLoadViewModel
            {
                ReportType = new List<SelectListItem>
                {
                    new SelectListItem() { Text = "By Client", Value = "client" },
                    new SelectListItem() { Text = "By Campaign", Value = "campaign" }
                },

                // TODO: Update these
                Clients = _accountService.GetAllClients().Select(c => new SelectListItem() { Value = c.Id.ToString(), Text = c.CompanyName }),
                Campaigns = _accountService.GetAllAgents().Select(a => new SelectListItem() { Value = a.Id.ToString(), Text = a.FullName })
            };
        }

        private ProductionLoadViewModel LoadClient(int clientId)
        {
            return new ProductionLoadViewModel
            {
                ReportType = new List<SelectListItem>
                {
                    new SelectListItem() { Text = "By Campaign", Value = "campaign" }
                },

                // TODO: Update this
                Campaigns = _accountService.GetAllAgents().Select(a => new SelectListItem() { Value = a.Id.ToString(), Text = a.FullName })
            };
        }
    }
}
