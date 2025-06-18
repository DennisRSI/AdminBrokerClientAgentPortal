using ClientPortal.Models;
using Codes1.Service.Interfaces;
using Codes1.Service.ViewModels;
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
    public class ReportChargebackController : Controller
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IReport1Service _reportService;
        private readonly IAccount1Service _accountService;
        private readonly ICode1Service _context;

        public ReportChargebackController(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager,
                                            IReport1Service reportService, IAccount1Service accountService, ICode1Service context)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _reportService = reportService;
            _accountService = accountService;
            _context = context;
        }
    }
}
