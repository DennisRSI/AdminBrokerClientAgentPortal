using ClientPortal.Models;
using ClientPortal.Models._ViewModels;
using Codes1.Service.Interfaces;
using Codes1.Service.Models;
using Codes1.Service.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ClientPortal.Controllers.APIs
{
    [Authorize]
    [Route("api/[controller]")]
    public class ViewDataController : Controller
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IAccount1Service _accountService;

        public ViewDataController(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager, IAccount1Service accountService)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _accountService = accountService;
        }

        [HttpGet("clients")]
        public async Task<IEnumerable<MyClientViewModel>> GetClients()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            if(user.Role.Contains("Agent"))
                return _accountService.GetClientsByAgent(user.AgentId);
            else
                return _accountService.GetClientsByBroker(user.BrokerId);
        }

        [HttpGet("agents")]
        public async Task<IEnumerable<MyAgentViewModel>> GetAgents()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);

            if (user.AgentId > 0)
            {
                return _accountService.GetAgentsByAgent(user.AgentId);
            }

            if (user.BrokerId > 0)
            {
                return _accountService.GetAgentsByBroker(user.BrokerId);
            }

            return null;
        }
    }
}
