using ClientPortal.Models;
using Codes.Service.Interfaces;
using Codes.Service.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
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
        private readonly IReportService _reportService;
        private readonly IAccountService _accountService;
        private readonly ICodeService _context;

        public ReportActivationController(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager,
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
            var view = "Load" + type;
            var model = new ActivationLoadViewModel();
            var user = await _userManager.GetUserAsync(HttpContext.User);

            switch (type.ToLower())
            {
                case "broker":
                    model.Agents = _accountService.GetAgentsOfBroker(user.BrokerId).Select(a => new SelectListItem() { Value = a.Id.ToString(), Text = a.Text });
                    model.Clients = _accountService.GetClientsOfBroker(user.BrokerId).Select(a => new SelectListItem() { Value = a.Id.ToString(), Text = a.Text });
                    break;
            };

            return PartialView(view, model);
        }

        [HttpGet("gethtml/{type}/{id}/{name}/{start}/{end}")]
        public async Task<IActionResult> GetHtml(string type, int id, string name, string start, string end)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);

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
                case "client":
                    var clients = _accountService.GetClientsOfBroker(user.BrokerId)
                        .Where(c => c.Id == id || id == 0)
                        .Select(c => new ActivationTableViewModel() { Id = c.Id, Type = type });

                    model.Tables.AddRange(clients);
                    break;

                case "agent":
                    var agents = _accountService.GetAgentsOfBroker(user.BrokerId)
                        .Where(a => a.Id == id || id == 0)
                        .Select(a => new ActivationTableViewModel() { Id = a.Id, Type = type });

                    model.Tables.AddRange(agents);
                    break;
            }

            return PartialView("Html", model);
        }

        [HttpGet("getjson/{type}/{id}/{status}/{used}/{start}/{end}")]
        public async Task<DataTableViewModel<ActivationCardViewModel>> GetJson(string type, int id, string status, string used, string start, string end)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);

            int? agentId = null;
            int? brokerId = user.BrokerId;
            int? clientId = null;
            DateTime startDate = DateTime.Now.AddYears(-1);
            DateTime endDate = DateTime.Now;

            switch (type)
            {
                case "agent":
                    agentId = id;
                    break;

                case "client":
                    clientId = id;
                    break;
            }

            startDate = DateTime.ParseExact(start, "yyyy-MM-dd", null);
            endDate = DateTime.ParseExact(end, "yyyy-MM-dd", null);

            var query = new ActivationReportViewModel()
            {
                AgentId = agentId,
                BrokerId = brokerId,
                ClientId = clientId,
                CampaignStatus = status,
                IsCardUsed = used == "Y",
                StartDate = startDate,
                EndDate = endDate,
            };

            return _reportService.GetDataActivation(query);
        }
    }
}
