using ClientPortal.Models;
using Codes.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ClientPortal.Controllers.APIs
{
    [Authorize]
    [Route("api/[controller]")]
    public class MenuController : Controller
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ISearchService _searchService;

        public MenuController(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager, ISearchService searchService)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _searchService = searchService;
        }

        [HttpGet("{id}/{cmd?}/{msg?}")]
        public IActionResult Get(string id, string cmd = "", string msg = "")
        {
            switch (id)
            {
                case "admin-change":
                    switch (cmd)
                    {
                        case "broker":
                            return null;
                        case "agent":
                            return null;
                        default:
                            return null;
                    }
                case "user-list":
                    return ViewComponent("UserManagement", new { type = cmd });
                case "client-details":
                    return ViewComponent("ClientDetails", new { accountId = cmd });
                case "clients-campaigns":
                    return null;
                case "campaign-list":
                    return null;
                case "my-account":
                    return ViewComponent("MyAccount", new { accountId = cmd, message = msg });
                default:
                    return ViewComponent("Dashboard");
            }
        }

        [HttpGet("search/{query}")]
        public IActionResult Search(string query)
        {
            var model = _searchService.GetAdmin(query);
            return ViewComponent("Search", model);
        }
    }
}
