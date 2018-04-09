using ClientPortal.Models;
using Codes.Service.Interfaces;
using Codes.Service.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ClientPortal.ViewComponents
{
    public class ClientEditViewComponent : ViewComponent
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IAccountService _accountService;

        public ClientEditViewComponent(SignInManager<ApplicationUser> signInManager, IAccountService accountService)
        {
            _signInManager = signInManager;
            _accountService = accountService;
        }

        public async Task<IViewComponentResult> InvokeAsync(int clientId)
        {
            if (_signInManager.IsSignedIn(HttpContext.User))
            {
                var model = _accountService.GetClientEdit(clientId);

                return await Task.FromResult(View(model));
            }

            return null;
        }
    }
}
