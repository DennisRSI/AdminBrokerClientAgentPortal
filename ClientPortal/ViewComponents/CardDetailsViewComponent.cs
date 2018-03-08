﻿using ClientPortal.Models;
using Codes.Service.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ClientPortal.ViewComponents
{
    public class CardDetailsViewComponent : ViewComponent
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ICardService _cardService;


        public CardDetailsViewComponent(SignInManager<ApplicationUser> signInManager, ICardService cardService)
        {
            _signInManager = signInManager;
            _cardService = cardService;
        }

        public async Task<IViewComponentResult> InvokeAsync(int id)
        {
            if (_signInManager.IsSignedIn(HttpContext.User))
            {
                var model = _cardService.GetDetails(id);
                return await Task.FromResult(View(model));
            }

            return null;
        }
    }
}
