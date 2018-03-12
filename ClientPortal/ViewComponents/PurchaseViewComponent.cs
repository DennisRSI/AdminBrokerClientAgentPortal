using ClientPortal.Models;
using Codes.Service.Interfaces;
using Codes.Service.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ClientPortal.ViewComponents
{
    public class PurchaseViewComponent : ViewComponent
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IPurchaseService _purchaseService;

        public PurchaseViewComponent(SignInManager<ApplicationUser> signInManager, IPurchaseService purchaseService)
        {
            _signInManager = signInManager;
            _purchaseService = purchaseService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            if (_signInManager.IsSignedIn(HttpContext.User))
            {
                var model = new PurchaseViewModel();
                return await Task.FromResult(View(model));
            }

            return null;
        }
    }
}
