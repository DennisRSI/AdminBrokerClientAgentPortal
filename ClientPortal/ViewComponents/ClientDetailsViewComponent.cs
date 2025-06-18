using ClientPortal.Models;
using Codes1.Service.Interfaces;
using Codes1.Service.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ClientPortal.ViewComponents
{
    public class ClientDetailsViewComponent : ViewComponent
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IViewData1Service _viewDataService;

        public ClientDetailsViewComponent(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager, IViewData1Service viewDataService)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _viewDataService = viewDataService;
        }

        public async Task<IViewComponentResult> InvokeAsync(string applicationReference)
        {
            if (_signInManager.IsSignedIn(HttpContext.User))
            {
                var user = await _userManager.GetUserAsync(HttpContext.User);
                var client = await _userManager.FindByIdAsync(applicationReference);
                if (client != null)
                {
                    var model = _viewDataService.GetClientDetails(client.ClientId);

                    model.ShowAddCampaignButton = !user.Role.Contains("Agent") && !user.Role.Contains("Client");
                    model.ShowEditButton = !user.Role.Contains("Agent") && !user.Role.Contains("Client");

                    return await Task.FromResult(View(model));
                }
            }

            return Content("Client not found");
        }
    }
}
