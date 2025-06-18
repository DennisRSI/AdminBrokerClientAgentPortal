using Codes1.Service.Interfaces;
using Codes1.Service.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace ClientPortal.Controllers.APIs
{
    [Authorize]
    [Route("api/[controller]")]
    public class CampaignController : Controller
    {
        private readonly ICampaign1Service _campaignService;

        public CampaignController(ICampaign1Service campaignService)
        {
            _campaignService = campaignService;
        }

        [HttpGet("getbyclient/{id}")]
        public DataTableViewModel<CampaignViewModel> Get(int id)
        {
            try
            {
                return _campaignService.GetByClient(id);
            }
            catch (Exception ex)
            {
                return new DataTableViewModel<CampaignViewModel>
                {
                    Message = $"Error: {ex.Message}"
                };
            }
        }

        [HttpPost("clone/{campaignId}")]
        public IActionResult Clone(int campaignId)
        {
            _campaignService.Clone(campaignId);
            return Ok();
        }

        [HttpPost("create/{id}")]
        public async Task<IActionResult> Create(int id, [FromBody] CampaignViewModel model)
        {
            string message = await _campaignService.Create(id, model);
            if(message == "Success")
                return Ok("Campaign Created Successfully");
            else
                return Ok(message);
        }

        [HttpPost("deactivate/{campaignId}/{reason}")]
        public IActionResult Deactivate(int campaignId, string reason)
        {
            _campaignService.Deactivate(campaignId, reason);
            return Ok();
        }
    }
}
