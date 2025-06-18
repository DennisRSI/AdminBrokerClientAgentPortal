using ClientPortal.Extensions;
using ClientPortal.Models;
using Codes1.Service.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ClientPortal.ViewComponents
{
    public class SearchViewComponent : ViewComponent
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ISearch1Service _searchService;

        public SearchViewComponent(SignInManager<ApplicationUser> signInManager,
                                    UserManager<ApplicationUser> userManager,
                                    ISearch1Service searchService)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _searchService = searchService;
        }

        public async Task<IViewComponentResult> InvokeAsync(string query)
        {
            if (_signInManager.IsSignedIn(HttpContext.User))
            {
                var user = await _userManager.GetUserAsync(HttpContext.User);
                var role = HttpContext.User.GetRole().GetName();
                var model = _searchService.Search(query, role, user.AccountId);

                return await Task.FromResult(View(model));
            }

            return null;
        }
    }
}
