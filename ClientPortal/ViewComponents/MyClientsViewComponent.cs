using AutoMapper;
using ClientPortal.Models;
using ClientPortal.Models._ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ClientPortal.ViewComponents
{
    public class MyClientsViewComponent : ViewComponent
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;

        public MyClientsViewComponent(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager, IMapper mapper)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            if (_signInManager.IsSignedIn(HttpContext.User))
            {
                var user = await _userManager.GetUserAsync(HttpContext.User);
                var model = _mapper.Map<ApplicationUser, MyClientsViewModel>(user);

                if (user.Role.Contains("Client") || user.Role.Contains("Agent"))
                {
                    model.ShowAddNewClientButton = false;
                }
                else
                {
                    model.ShowAddNewClientButton = true;
                }

                return View(model);
            }

            return null;
        }
    }
}
