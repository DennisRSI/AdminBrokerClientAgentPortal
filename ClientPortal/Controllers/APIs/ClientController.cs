﻿using ClientPortal.Models;
using ClientPortal.Services._Interfaces;
using Codes.Service.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ClientPortal.ViewComponents;

namespace ClientPortal.Controllers.APIs
{
    [Route("api/[controller]")]
    public class ClientController : Controller
    {
        private readonly IUserService _context;
        private readonly ICodeService _codeService;
        private readonly IDocumentService _documentService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IAccountService _accountService;

        public ClientController(IUserService context, ICodeService codeService, IDocumentService documentService, UserManager<ApplicationUser> userManager,
            IAccountService accountService)
        {
            _context = context;
            _codeService = codeService;
            _userManager = userManager;
            _documentService = documentService;
            _accountService = accountService;
        }

        [HttpPost("addagent/{clientId}/{agentId}/{commissionRate}")]
        public IActionResult AddAgent(int clientId, int agentId, decimal commissionRate)
        {
            _accountService.AddAgentToClient(clientId, agentId, commissionRate);
            return ViewComponent(typeof(AssignedAgentsViewComponent), clientId);
        }

        [HttpPost("removeagent/{clientId}/{agentId}")]
        public IActionResult RemoveAgent(int clientId, int agentId)
        {
            _accountService.RemoveAgentFromClient(clientId, agentId);
            return ViewComponent(typeof(AssignedAgentsViewComponent), clientId);
        }

        [HttpPost("updatecommission/{clientId}/{agentId}/{commissionRate}")]
        public IActionResult UpdateCommission(int clientId, int agentId, decimal commissionRate)
        {
            _accountService.UpdateClientCommissionRate(clientId, agentId, commissionRate);
            return ViewComponent(typeof(AssignedAgentsViewComponent), clientId);
        }
    }
}
