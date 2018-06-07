using ClientPortal.Models;
using Codes.Service.Interfaces;
using Codes.Service.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;

namespace ClientPortal.Controllers.APIs
{
    [Authorize]
    [Route("api/[controller]")]
    public class ReportController : Controller
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IReportService _reportService;

        public ReportController(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager, IReportService reportService)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _reportService = reportService;
        }

        [HttpGet("load/{name}")]
        public IActionResult Load(string name)
        {
            return ViewComponent("Report", name);
        }

        [HttpGet("getjson/activation")]
        public DataTableViewModel<ActivationResultViewModel> GetJsonActivation() // ActivationReportViewModel model)
        {
            // Test code
            var model = new ActivationReportViewModel()
            {
                StartDate = DateTime.Parse("1/1/2000"),
                EndDate = DateTime.Parse("1/1/2020"),
                BrokerId = 4,
                AgentId = null,
                ClientId = 8,
                CampaignStatus = "Active",
                IsCardUsed = false
            };

            return _reportService.GetDataActivation(model);
        }
    }
}
