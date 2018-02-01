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
    public class UserManagementViewComponent : ViewComponent
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public UserManagementViewComponent(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        public async Task<IViewComponentResult> InvokeAsync(string type)
        {
            if (_signInManager.IsSignedIn(HttpContext.User))
            {
                PageViewModel model = new PageViewModel()
                {
                    Role = type
                };
                return await Task.FromResult(View(model));

            }

            return null;
        }
    }
}
