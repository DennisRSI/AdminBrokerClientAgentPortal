using ClientPortal.Models;
using ClientPortal.Extensions;
using Codes.Service.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

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

        public async Task<IViewComponentResult> InvokeAsync()
        {
            if (_signInManager.IsSignedIn(HttpContext.User))
            {
                ViewData["Role"] = HttpContext.User.GetRole().GetName();
                var model = _dashboardService.GetAdmin();
                return await Task.FromResult(View(model));
            }

            return null;
        }
    }
}
