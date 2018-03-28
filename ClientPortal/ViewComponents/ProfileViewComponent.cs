﻿using AutoMapper;
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

        public ProfileViewComponent(ICodeService context, 
            SignInManager<ApplicationUser> signInManager, 
            UserManager<ApplicationUser> userManager,
            IMapper mapper)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _context = context;
            _mapper = mapper;
        }

        public async Task<IViewComponentResult> InvokeAsync(string profileId)
        {
            var user = await _userManager.FindByIdAsync(profileId);
            var model = _mapper.Map<ApplicationUser, ProfileViewModel>(user);

            return View(model);
        }
    }
}
