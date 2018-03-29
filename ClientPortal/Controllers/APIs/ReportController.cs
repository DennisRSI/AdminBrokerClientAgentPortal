﻿using ClientPortal.Models;
using Codes.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ClientPortal.Controllers.APIs
{
    [Authorize]
    [Route("api/[controller]")]
    public class ReportController : Controller
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public ReportController(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        [HttpGet("view/{id}")]
        public IActionResult View(int id)
        {
            return ViewComponent("Report");
        }
    }
}