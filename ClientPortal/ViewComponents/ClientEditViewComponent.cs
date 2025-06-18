using ClientPortal.Models;
using Codes1.Service.Interfaces;
using Codes1.Service.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ClientPortal.ViewComponents
{
    public class ClientEditViewComponent : ViewComponent
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IAccount1Service _accountService;

        public ClientEditViewComponent(SignInManager<ApplicationUser> signInManager, IAccount1Service accountService)
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
