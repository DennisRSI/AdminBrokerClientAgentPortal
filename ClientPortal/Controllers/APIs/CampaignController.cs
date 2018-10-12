using Codes.Service.Interfaces;
using Codes.Service.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

namespace ClientPortal.Controllers.APIs
{
    [Authorize]
    [Route("api/[controller]")]
    public class CampaignController : Controller
    {
        private readonly ICampaignService _campaignService;

        public CampaignController(ICampaignService campaignService)
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
        public IActionResult Create(int id, [FromBody] CampaignViewModel model)
        {
            _campaignService.Create(id, model);
            return Ok("Campaign Created Successfully");
        }

        [HttpPost("deactivate/{campaignId}/{reason}")]
        public IActionResult Deactivate(int campaignId, string reason)
        {
            _campaignService.Deactivate(campaignId, reason);
            return Ok();
        }
    }
}
