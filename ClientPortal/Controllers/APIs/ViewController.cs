using ClientPortal.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ClientPortal.Controllers.APIs
{
    [Authorize]
    [Route("api/[controller]")]
    public class ViewController : Controller
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public ViewController(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        [HttpGet("purchase")]
        public IActionResult Purchase()
        {
            return ViewComponent("Purchase");
        }

        [HttpGet("myclients")]
        public IActionResult MyClients()
        {
            return ViewComponent("MyClients");
        }

        [HttpGet("myagents")]
        public IActionResult MyAgents()
        {
            return ViewComponent("MyAgents");
        }

        [HttpGet("clientdetails/{applicationReference}")]
        public IActionResult ClientDetails(string applicationReference)
        {
            return ViewComponent("ClientDetails", applicationReference);
        }

        [HttpGet("clientedit/{clientId}")]
        public IActionResult ClientEdit(int clientId)
        {
            return ViewComponent("ClientEdit", clientId);
        }
    }
}
