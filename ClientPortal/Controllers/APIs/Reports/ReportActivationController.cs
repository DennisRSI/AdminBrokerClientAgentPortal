using ClientPortal.Models;
using Codes.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
            var model = new ActivationReportLoadViewModel();
            var user = await _userManager.GetUserAsync(HttpContext.User);
            
            switch (type.ToLower())
            {
                case "broker":
                    model.Agents = _accountService.GetAgentsOfBroker(user.BrokerId).Select(a => new SelectListItem() { Value = a.Id.ToString(), Text = a.Text });
                    model.Clients = _accountService.GetClientsOfBroker(user.BrokerId).Select(a => new SelectListItem() { Value = a.Id.ToString(), Text = a.Text });
                    break;
            };

            return View(view, model);
        }

        [HttpGet("gethtml/{type}/{id}")]
        public async Task<IActionResult> GetHtml(string type, int id)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var model = new ActivationResultViewModel();

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

            return View("Html", model);
        }
    }
}
