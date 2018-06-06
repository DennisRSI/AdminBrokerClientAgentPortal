using ClientPortal.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ClientPortal.ViewComponents
{
    public class ReportViewComponent : ViewComponent
    {
        private readonly SignInManager<ApplicationUser> _signInManager;

        public ReportViewComponent(SignInManager<ApplicationUser> signInManager)
        {
            _signInManager = signInManager;
        }

        public async Task<IViewComponentResult> InvokeAsync(string name)
        {
            if (_signInManager.IsSignedIn(HttpContext.User))
            {
                return await Task.FromResult(View(name));
            }

            return null;
        }
    }
}
