using AutoMapper;
using ClientPortal.Models;
using ClientPortal.Models._ViewModels;
using Codes.Service.Interfaces;
using Codes.Service.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClientPortal.ViewComponents
{
    public class ProfileViewComponent : ViewComponent
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ICodeService _context;
        private readonly IMapper _mapper;
        private readonly IAccountService _accountService;

        public ProfileViewComponent(ICodeService context, 
            SignInManager<ApplicationUser> signInManager, 
            UserManager<ApplicationUser> userManager,
            IMapper mapper,
            IAccountService accountService)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _context = context;
            _mapper = mapper;
            _accountService = accountService;
        }

        public async Task<IViewComponentResult> InvokeAsync(string profileId)
        {
            var user = await _userManager.FindByIdAsync(profileId);
            var model = _mapper.Map<ApplicationUser, ProfileViewModel>(user);

            if (user.Role == "Broker")
            {
                var broker = await _context.GetBrokerByAccountId(profileId);
                model.DocumentW9Id = broker.DocumentW9Id;
            }

            var account = _accountService.GetAccountCommon(profileId);
            model.CommissionRate = account.CommissionRate;

            return View(model);
        }
    }
}
