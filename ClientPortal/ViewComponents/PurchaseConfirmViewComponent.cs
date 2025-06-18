using ClientPortal.Models;
using Codes1.Service.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ClientPortal.ViewComponents
{
    public class PurchaseConfirmViewComponent : ViewComponent
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IPurchase1Service _purchaseService;

        public PurchaseConfirmViewComponent(SignInManager<ApplicationUser> signInManager, IPurchase1Service purchaseService)
        {
            _signInManager = signInManager;
            _purchaseService = purchaseService;
        }

        public async Task<IViewComponentResult> InvokeAsync(int purchaseId)
        {
            if (_signInManager.IsSignedIn(HttpContext.User))
            {
                var model = _purchaseService.GetDetails(purchaseId);

                return await Task.FromResult(View(model));
            }

            return null;
        }
    }
}
