using ClientPortal.Models;
using Codes.Service.Interfaces;
using Codes.Service.ViewModels;
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
        private readonly ICodeService _context;

        public CardsTotalViewComponent(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager, ICodeService context)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync(string accountId)
        {
            CardsTotalViewModel model = new CardsTotalViewModel();

            if (_signInManager.IsSignedIn(HttpContext.User))
            {
                return View(model);
            }

            return null;

        }
    }
}
