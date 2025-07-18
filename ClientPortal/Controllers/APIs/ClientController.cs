﻿using System;
using ClientPortal.Models;
using ClientPortal.Services._Interfaces;
using Codes1.Service.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ClientPortal.ViewComponents;
using System.Threading.Tasks;

namespace ClientPortal.Controllers.APIs
{
    [Route("api/[controller]")]
    public class ClientController : Controller
    {
        private readonly IUserService _context;
        private readonly ICode1Service _codeService;
        private readonly IDocument1Service _documentService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IAccount1Service _accountService;

        public ClientController(IUserService context, ICode1Service codeService, IDocument1Service documentService, UserManager<ApplicationUser> userManager,
            IAccount1Service accountService)
        {
            _context = context;
            _codeService = codeService;
            _userManager = userManager;
            _documentService = documentService;
            _accountService = accountService;
        }

        [HttpPost("addagent/{clientId}/{agentId}")]
        public IActionResult AddAgent(int clientId, int agentId)
        {
            _accountService.AddAgentToClient(clientId, agentId);

            string errorMessage = String.Empty;
            return ViewComponent(typeof(AssignedAgentsViewComponent), new { clientId, errorMessage });
        }

        [HttpPost("removeagent/{clientId}/{agentId}")]
        public IActionResult RemoveAgent(int clientId, int agentId)
        {
            _accountService.RemoveAgentFromClient(clientId, agentId);

            string errorMessage = String.Empty;
            return ViewComponent(typeof(AssignedAgentsViewComponent), new { clientId, errorMessage });
        }

        [HttpPost("updatecommission/{clientId}/{agentId}/{commissionRate}")]
        public async Task<IActionResult> UpdateCommission(int clientId, int agentId, decimal commissionRate)
        {
            var errorMessage = String.Empty;
            var result = await _accountService.UpdateClientCommissionRate(clientId, agentId, commissionRate);

            if (!result)
            {
                errorMessage = "Error: The commission rate for this agent would be above the total commission you are getting for your company.";
            }

            return ViewComponent(typeof(AssignedAgentsViewComponent), new { clientId, errorMessage });
        }
    }
}
