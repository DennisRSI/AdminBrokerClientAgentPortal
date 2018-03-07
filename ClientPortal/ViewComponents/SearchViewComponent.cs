using ClientPortal.Models;
using Codes.Service.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ClientPortal.ViewComponents
{
    public class SearchViewComponent : ViewComponent
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ISearchService _searchService;

        public SearchViewComponent(SignInManager<ApplicationUser> signInManager, ISearchService searchService)
        {
            _signInManager = signInManager;
            _searchService = searchService;
        }

        public async Task<IViewComponentResult> InvokeAsync(string query)
        {
            if (_signInManager.IsSignedIn(HttpContext.User))
            {
                var model = _searchService.GetAdmin(query);
                return await Task.FromResult(View(model));
            }

            return null;
        }
    }
}
