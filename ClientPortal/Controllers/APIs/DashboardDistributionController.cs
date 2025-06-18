using ClientPortal.Models;
using Codes1.Service.Interfaces;
using Codes1.Service.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ClientPortal.Controllers.APIs
{
    [Authorize]
    [Route("api/[controller]")]
    public class DashboardDistributionController : Controller
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IDashboardDistribution1Service _dashboardDistributionService;

        public DashboardDistributionController(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager, IDashboardDistribution1Service dashboardDistributionService)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _dashboardDistributionService = dashboardDistributionService;
        }

        [HttpGet("admin/{adminId}")]
        public DataTableViewModel<CardDistributionViewModel> GetAdmin(int adminId)
        {
            return _dashboardDistributionService.GetAdmin();
        }

        [HttpGet("broker/{brokerId}")]
        public DataTableViewModel<CardDistributionViewModel> GetBroker(int brokerId)
        {
            return _dashboardDistributionService.GetBroker(brokerId);
        }

        [HttpGet("agent/{agentId}")]
        public DataTableViewModel<CardDistributionViewModel> GetAgent(int agentId)
        {
            return _dashboardDistributionService.GetAgent(agentId);
        }

        [HttpGet("client/{clientId}")]
        public DataTableViewModel<CardDistributionViewModel> GetClient(int clientId)
        {
            return _dashboardDistributionService.GetClient(clientId);
        }
    }
}
