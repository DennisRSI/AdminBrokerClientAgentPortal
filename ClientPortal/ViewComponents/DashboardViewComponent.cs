using ClientPortal.Models;
using ClientPortal.Extensions;
using Codes.Service.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Codes.Service.ViewModels;
using Microsoft.AspNetCore.Hosting;

namespace ClientPortal.ViewComponents
{
    public class DashboardViewComponent : ViewComponent
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IDashboardService _dashboardService;
        private readonly IAccountService _accountService;
        private readonly IHostingEnvironment _hostingEnvironment;

        public DashboardViewComponent(SignInManager<ApplicationUser> signInManager,
                                        UserManager<ApplicationUser> userManager,
                                        IDashboardService dashboardService,
                                        IAccountService accountService,
                                        IHostingEnvironment hostingEnvironment)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _dashboardService = dashboardService;
            _accountService = accountService;
            _hostingEnvironment = hostingEnvironment;
        }

        public async Task<IViewComponentResult> InvokeAsync(string role, int id)
        {
            if (_signInManager.IsSignedIn(HttpContext.User))
            {
                bool simulating = true;

                if (role == string.Empty)
                {
                    role = HttpContext.User.GetRole().GetName();

                    var reference = _userManager.GetUserId(HttpContext.User);
                    id = _accountService.GetIdFromReference(reference);
                    simulating = false;
                }

                var model = new DashboardViewModel();

                if (_hostingEnvironment.EnvironmentName != "Local")
                {
                    switch (role.ToLower())
                    {
                        case "super administrator":
                        case "administrator":
                            model = _dashboardService.GetAdmin();
                            break;

                        case "broker":
                            model = _dashboardService.GetBroker(id);
                            break;

                        case "agent":
                            model = _dashboardService.GetAgent(id);
                            break;

                        case "client":
                            model = _dashboardService.GetClient(id);
                            break;

                        default:
                            model = _dashboardService.GetAdmin();
                            break;
                    }
                }

                model.Role = role;
                model.Id = id;
                model.IsSimulating = simulating;

                ViewData["Role"] = role;
                return await Task.FromResult(View(model));
            }

            return null;
        }
    }
}
