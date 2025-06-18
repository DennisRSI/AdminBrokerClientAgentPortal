using ClientPortal.Models;
using Codes1.Service.Interfaces;
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
        private readonly ISearch1Service _searchService;

        public MenuController(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager, ISearch1Service searchService)
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
                    return ViewComponent("ClientDetails", new { applicationReference = cmd });
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
            return ViewComponent("Search", query);
        }

        [HttpGet("card-details/{code}")]
        public IActionResult CardDetails(string code)
        {
            return ViewComponent("CardDetails", code);
        }

        [HttpGet("importexcel/")]
        public IActionResult ImportExcel()
        {
            return ViewComponent("ImportExcel");
        }

        

    }
}
