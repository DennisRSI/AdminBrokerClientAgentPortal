using Codes.Service.Interfaces;
using Codes.Service.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

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

        [HttpPost("create/{id}")]
        public IActionResult Create(int id, [FromBody] CampaignViewModel model)
        {
            _campaignService.Create(id, model);
            return Ok();
        }
    }
}
