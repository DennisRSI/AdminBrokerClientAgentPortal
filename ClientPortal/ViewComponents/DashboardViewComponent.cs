using ClientPortal.Models;
using ClientPortal.Extensions;
using Codes1.Service.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Codes1.Service.ViewModels;
using Microsoft.AspNetCore.Hosting;

namespace ClientPortal.ViewComponents
{
    public class DashboardViewComponent : ViewComponent
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IDashboard1Service _dashboardService;
        private readonly IAccount1Service _accountService;
        private readonly IWebHostEnvironment _hostingEnvironment;
        private Codes1.Service.Interfaces.V2.ICodeServices _codeService;

        public DashboardViewComponent(SignInManager<ApplicationUser> signInManager,
                                        UserManager<ApplicationUser> userManager,
                                        IDashboard1Service dashboardService,
                                        IAccount1Service accountService,
                                        IWebHostEnvironment hostingEnvironment,
                                        Codes1.Service.Interfaces.V2.ICodeServices codeService)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _dashboardService = dashboardService;
            _accountService = accountService;
            _hostingEnvironment = hostingEnvironment;
            _codeService = codeService;
        }

        public async Task<IViewComponentResult> InvokeAsync(string role, int id)
        {
            if (_signInManager.IsSignedIn(HttpContext.User))
            {
                bool simulating = true;

                if (role == string.Empty)
                {
                    role = HttpContext.User.GetRole().GetName();

                    var reference = _userManager.GetUserId(HttpContext.User);
                    id = _accountService.GetIdFromReference(reference);
                    simulating = false;
                }

                //var model = new Codes.Service.ViewModels.V2.DashboardViewModel();

                var model = new DashboardViewModel();

                if (_hostingEnvironment.EnvironmentName != "Local")
                {
                    switch (role.ToLower())
                    {
                        case "super administrator":
                        case "administrator":
                            model = _dashboardService.GetAdmin(); //await _codeService.GetDashboardAsync(0, 0, 0);
                            break;

                        case "broker":
                            model = _dashboardService.GetBroker(id);
                            //model = await _codeService.GetDashboardAsync(id, 0 , 0); //_dashboardService.GetBroker(id);
                            break;

                        case "agent":
                            model = _dashboardService.GetAgent(id);
                            //model = await _codeService.GetDashboardAsync(0, 0, id); //_dashboardService.GetAgent(id);
                            break;

                        case "client":
                            model = _dashboardService.GetClient(id);
                            //model = await _codeService.GetDashboardAsync(0, id, 0); //_dashboardService.GetClient(id);
                            break;

                        default:
                            model = _dashboardService.GetAdmin();
                            //model = await _codeService.GetDashboardAsync(0, 0, 0); //_dashboardService.GetAdmin();
                            break;
                    }
                }

                model.Role = role;
                model.Id = id;
                model.IsSimulating = simulating;

                ViewData["Role"] = role;
                return await Task.FromResult(View(model));
            }

            return null;
        }
    }
}
