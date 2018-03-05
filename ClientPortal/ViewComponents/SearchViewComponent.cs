using ClientPortal.Models;
using Codes.Service.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ClientPortal.ViewComponents
{
    public class SearchViewComponent : ViewComponent
    {
        private readonly SignInManager<ApplicationUser> _signInManager;

        public SearchViewComponent(SignInManager<ApplicationUser> signInManager)
        {
            _signInManager = signInManager;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            if (_signInManager.IsSignedIn(HttpContext.User))
            {
                return await Task.FromResult(View());
            }

            return null;
        }
    }
}
