using ClientPortal.Models;
using Codes1.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ClientPortal.Controllers.APIs
{
    [Authorize]
    [Route("api/[controller]")]
    public class DocumentController : Controller
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IDocument1Service _documentService;

        public DocumentController(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager, IDocument1Service documentService)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _documentService = documentService;
        }

        [HttpGet("get/{id}")]
        public async Task<FileResult> Get(int id)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);

            if (!user.IsAdmin)
            {
                Unauthorized();
            }

            var model = _documentService.Get(id);
            return File(model.Data, model.ContentType);
        }
    }
}
