using ClientPortal.Models;
using Codes1.Service.Interfaces;
using Codes1.Service.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClientPortal.ViewComponents
{
    public class CardsTotalViewComponent : ViewComponent
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ICode1Service _context;

        public CardsTotalViewComponent(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager, ICode1Service context)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _context = context;
        }

#pragma warning disable 1998
        public async Task<IViewComponentResult> InvokeAsync(string accountId)
        {
            CardsTotalViewModel model = new CardsTotalViewModel();

            if (_signInManager.IsSignedIn(HttpContext.User))
            {
                return View(model);
            }

            return null;

        }
#pragma warning restore 1998

    }
}
