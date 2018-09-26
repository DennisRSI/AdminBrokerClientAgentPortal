using ClientPortal.Models;
using Codes.Service.ViewModels;
using Codes.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ClientPortal.Controllers.APIs
{
    [Authorize]
    [Route("api/[controller]")]
    public class PurchaseController : Controller
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IPurchaseService _purchaseService;

        public PurchaseController(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager, IPurchaseService purchaseService)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _purchaseService = purchaseService;
        }

        [HttpPost("purchase")]
        public IActionResult Purchase( [FromBody] PurchaseViewModel model)
        {
            var reference = _userManager.GetUserId(User);

            var result = _purchaseService.Purchase(reference, model);

            return Ok(result);
        }

        [HttpGet("viewconfirmation/{purchaseId}")]
        public IActionResult ViewConfirmation(int purchaseId)
        {
            return ViewComponent("PurchaseConfirm", purchaseId);
        }

        [HttpGet("list/{brokerId}")]
        public IActionResult List(int brokerId)
        {
            var model = _purchaseService.GetList(brokerId);
            return Ok(model);
        }
    }
}
