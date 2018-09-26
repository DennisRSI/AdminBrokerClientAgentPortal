using AutoMapper;
using ClientPortal.Models;
using ClientPortal.Models._ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ClientPortal.ViewComponents
{
    public class MyAccountViewComponent : ViewComponent
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;

        public MyAccountViewComponent(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager, IMapper mapper)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<IViewComponentResult> InvokeAsync(string accountId)
        {
            if (_signInManager.IsSignedIn(HttpContext.User))
            {
                var loggedInUser = await _userManager.GetUserAsync(HttpContext.User);
                var viewUser = await _userManager.FindByIdAsync(accountId);
                var model = _mapper.Map<ApplicationUser, MyAccountViewModel>(viewUser);

                model.ShowProfileTab = loggedInUser.Role.Contains("Administrator");

                return View(model);
            }

            return null;
        }
    }
}
