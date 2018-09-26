using ClientPortal.Models;
using ClientPortal.Models._ViewModels;
using Codes.Service.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ClientPortal.ViewComponents
{
    public class AddUserViewComponent : ViewComponent
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ICodeService _codeService;

        public AddUserViewComponent(
            SignInManager<ApplicationUser> signInManager,
            UserManager<ApplicationUser> userManager,
            ICodeService codeService)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _codeService = codeService;
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

                if (brokerId > 0)
                {
                    var broker = await _codeService.GetBrokerById(brokerId);

                    switch (userType)
                    {
                        case "Agent":
                            model.CommissionRate = (decimal)broker.AgentCommissionPercentage;
                            break;

                        case "Client":
                            model.CommissionRate = (decimal)broker.ClientCommissionPercentage;
                            break;
                    }
                }

                return await Task.FromResult(View(model));
            }

            return null;
        }
    }
}
