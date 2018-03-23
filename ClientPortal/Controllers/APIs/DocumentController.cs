using ClientPortal.Models;
using Codes.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ClientPortal.Controllers.APIs
{
    [Authorize]
    [Route("api/[controller]")]
    public class DocumentController : Controller
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IDocumentService _documentService;

        public DocumentController(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager, IDocumentService documentService)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _documentService = documentService;
        }

        [HttpGet("get/{id}")]
        public FileResult Get(int id)
        {
            var model = _documentService.Get(id);
            return File(model.Data, model.ContentType);
        }
    }
}
