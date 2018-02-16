using AutoMapper;
using Codes.Service.Data;
using Codes.Service.Interfaces;
using Codes.Service.Models;
using Codes.Service.ViewModels;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;

namespace Codes.Service.Services
{
    public class CampaignService : ICampaignService
    {
        private readonly ILogger _logger;
        private readonly CodesDbContext _context;
        private readonly IMapper _mapper;

        public CampaignService(CodesDbContext context, ILoggerFactory loggerFactory, IMapper mapper)
        {
            _context = context;
            _logger = loggerFactory.CreateLogger<CodeService>();
            _mapper = mapper;
        }

        public DataTableViewModel<CampaignViewModel> GetByClient(int id, int draw, int startRowIndex, int numberOfRows, string sortColumn, string sortDirection, string searchValue)
        {
            var model = new DataTableViewModel<CampaignViewModel>();

            var queryResult = _context.Campaigns
                            .Where(c => c.ClientId == id && c.IsActive)
                            .OrderByDescending(c => c.CreationDate);

            var data = _mapper.Map<IEnumerable<CampaignModel>, IEnumerable<CampaignViewModel>>(queryResult);

            model.Draw = id;
            model.NumberOfRows = data.Count();
            model.RecordsFiltered = model.NumberOfRows;
            model.Data = data.ToArray();
            model.Message = "Success";

            return model;
        }
    }
}
