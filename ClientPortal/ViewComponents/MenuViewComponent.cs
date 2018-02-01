using ClientPortal.Models;
using ClientPortal.Models._ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClientPortal.ViewComponents
{
    public class MenuViewComponent : ViewComponent
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public MenuViewComponent(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            if (_signInManager.IsSignedIn(HttpContext.User))
            {
                MenuViewModel model = new MenuViewModel()
                {
                    RoleName = User.IsInRole("Super Administrator") ? "Super Admin" : User.IsInRole("Administrator") ? "Admin" : User.IsInRole("Broker") ? "Broker" : User.IsInRole("Agent") ? "Agent" : "Client",
                    AccountId = (await _userManager.GetUserAsync(HttpContext.User)).Id
                };
                return await Task.FromResult(View(model));
            }

            return null;
        }
    }
}
