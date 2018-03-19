using ClientPortal.Models;
using ClientPortal.Extensions;
using Codes.Service.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Codes.Service.ViewModels;

namespace ClientPortal.ViewComponents
{
    public class DashboardViewComponent : ViewComponent
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IDashboardService _dashboardService;

        public DashboardViewComponent(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager, IDashboardService dashboardService)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _dashboardService = dashboardService;
        }

        public async Task<IViewComponentResult> InvokeAsync(string role, int id)
        {
            if (_signInManager.IsSignedIn(HttpContext.User))
            {
                if (role == string.Empty)
                {
                    role = HttpContext.User.GetRole().GetName();
                }

                DashboardViewModel model;

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

                ViewData["Role"] = role;
                return await Task.FromResult(View(model));
            }

            return null;
        }
    }
}
