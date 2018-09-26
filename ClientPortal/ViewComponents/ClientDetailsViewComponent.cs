using ClientPortal.Models;
using Codes.Service.Interfaces;
using Codes.Service.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ClientPortal.ViewComponents
{
    public class ClientDetailsViewComponent : ViewComponent
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IViewDataService _viewDataService;

        public ClientDetailsViewComponent(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager, IViewDataService viewDataService)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _viewDataService = viewDataService;
        }

        public async Task<IViewComponentResult> InvokeAsync(string applicationReference)
        {
            if (_signInManager.IsSignedIn(HttpContext.User))
            {
                var client = await _userManager.FindByIdAsync(applicationReference);
                var model = _viewDataService.GetClientDetails(client.ClientId);

                return await Task.FromResult(View(model));
            }

            return null;
        }
    }
}
