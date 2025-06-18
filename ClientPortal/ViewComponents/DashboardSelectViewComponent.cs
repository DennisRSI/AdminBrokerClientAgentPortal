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
    public class DashboardSelectViewComponent : ViewComponent
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IDashboard1Service _dashboardService;

        public DashboardSelectViewComponent(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager, IDashboard1Service dashboardService)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _dashboardService = dashboardService;
        }

        public async Task<IViewComponentResult> InvokeAsync(string role)
        {
            if (_signInManager.IsSignedIn(HttpContext.User))
            {
                DashboardSelectViewModel model;

                switch (role)
                {
                    case "broker":
                        model = _dashboardService.GetListBrokers();
                        break;

                    case "agent":
                        model = _dashboardService.GetListAgents();
                        break;

                    case "client":
                        model = _dashboardService.GetListClients();
                        break;

                    default:
                        model = null;
                        break;
                }

                return await Task.FromResult(View(model));
            }

            return null;
        }
    }
}
