using ClientPortal.Models;
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

        public ClientDetailsViewComponent(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        public async Task<IViewComponentResult> InvokeAsync(string applicationReference)
        {
            if (_signInManager.IsSignedIn(HttpContext.User))
            {
                var model = new ClientDetailsViewModel()
                {
                    ApplicationReference = applicationReference
                };


                return await Task.FromResult(View(model));
            }

            return null;
        }
    }
}
