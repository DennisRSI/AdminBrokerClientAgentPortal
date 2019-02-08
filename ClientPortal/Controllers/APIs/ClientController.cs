using System;
using System.IO;
using System.Threading.Tasks;
using ClientPortal.Models;
using ClientPortal.Services._Interfaces;
using Codes.Service.Interfaces;
using Codes.Service.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Codes.Service.Models;
using ClientPortal.Models._ViewModels;

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

        [HttpPost("addagent/{clientId}/{agentId}")]
        public IActionResult AddAgent(int clientId, int agentId)
        {
            _accountService.AddAgentToClient(clientId, agentId);
            return Ok();
        }

        [HttpPost("removeagent/{clientId}/{agentId}")]
        public IActionResult RemoveAgent(int clientAgentId)
        {
            _accountService.RemoveAgentFromClient(clientAgentId);
            return Ok();
        }
    }
}
