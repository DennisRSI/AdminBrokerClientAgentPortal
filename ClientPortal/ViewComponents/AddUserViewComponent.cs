using ClientPortal.Models;
using ClientPortal.Models._ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ClientPortal.ViewComponents
{
    public class AddUserViewComponent : ViewComponent
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public AddUserViewComponent(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        public async Task<IViewComponentResult> InvokeAsync(int brokerId, string userType)
        {
            if (_signInManager.IsSignedIn(HttpContext.User))
            {
                var model = new AddUserViewModel()
                {
                    BrokerId = brokerId,
                    UserType = userType
                };

                return await Task.FromResult(View(model));
            }

            return null;
        }
    }
}
