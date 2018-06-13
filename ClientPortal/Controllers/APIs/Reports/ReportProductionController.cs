using ClientPortal.Models;
using Codes.Service.Interfaces;
using Codes.Service.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ClientPortal.Controllers.APIs
{
    [Authorize]
    [Route("api/[controller]")]
    public class ReportProductionController : Controller
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IReportService _reportService;
        private readonly IAccountService _accountService;
        private readonly ICodeService _context;

        public ReportProductionController(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager,
                                            IReportService reportService, IAccountService accountService, ICodeService context)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _reportService = reportService;
            _accountService = accountService;
            _context = context;
        }

        [HttpGet("load/{type}")]
        public async Task<IActionResult> LoadAsync(string type)
        {
            return PartialView("Load");
        }
    }
}
