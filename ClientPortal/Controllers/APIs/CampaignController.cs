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
        public DataTableViewModel<CampaignViewModel> Get(int id, int draw, int startRowIndex, int numberOfRows, string sortColumn, string sortDirection, string searchValue)
        {
            try
            {
                return _campaignService.GetByClient(id, draw, startRowIndex, numberOfRows, sortColumn, sortDirection, searchValue);
            }
            catch (Exception ex)
            {
                return new DataTableViewModel<CampaignViewModel>
                {
                    Message = $"Error: {ex.Message}"
                };
            }
        }
    }
}
