using ClientPortal.Models;
using ClientPortal.Models._ViewModels;
using Codes.Service.Domain;
using Codes.Service.Interfaces;
using Codes.Service.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;
using System.Threading.Tasks;

namespace ClientPortal.ViewComponents
{
    public class AssignedAgentsViewComponent : ViewComponent
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ICodeService _codeService;
        private readonly IAccountService _accountService;

        public AssignedAgentsViewComponent(
            SignInManager<ApplicationUser> signInManager,
            UserManager<ApplicationUser> userManager,
            ICodeService codeService,
            IAccountService accountService)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _codeService = codeService;
            _accountService = accountService;
        }

        public async Task<IViewComponentResult> InvokeAsync(int clientId, string errorMessage)
        {
            var model = _accountService.GetClientEdit(clientId);
            model.ErrorMessage = errorMessage;

            return await Task.FromResult(View(model));
        }
    }
}
