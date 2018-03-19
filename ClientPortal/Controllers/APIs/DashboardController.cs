using ClientPortal.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ClientPortal.Controllers.APIs
{
    [Authorize]
    [Route("api/[controller]")]
    public class DashboardController : Controller
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public DashboardController(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        [HttpGet("select/{role}")]
        public IActionResult SelectRole(string role)
        {
            return ViewComponent("DashboardSelect", new { role });
        }

        [HttpGet("view/{role}/{id}")]
        public IActionResult GetSimulatedView(string role, int id)
        {
            return ViewComponent("Dashboard", new { role, id });
        }
    }
}
