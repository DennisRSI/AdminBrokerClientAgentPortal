using ClientPortal.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClientPortal.ViewComponents
{
    public class MyAccountViewComponent : ViewComponent
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public MyAccountViewComponent(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        public async Task<IViewComponentResult> InvokeAsync(string accountId)
        {
            if (_signInManager.IsSignedIn(HttpContext.User))
            {
                //var user = await _userManager.GetUserAsync(HttpContext.User);
                var user = await _userManager.FindByIdAsync(accountId);
                return View(user);
            }

            return null;
        }
    }
}
