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

        public DataTableViewModel<CampaignViewModel> GetByClient(int id)
        {
            var queryResult = _context.Campaigns
                                .Where(c => c.ClientId == id && c.IsActive)
                                .OrderByDescending(c => c.CreationDate);

            var data = _mapper.Map<IEnumerable<CampaignModel>, IEnumerable<CampaignViewModel>>(queryResult);

            return new DataTableViewModel<CampaignViewModel>
            {
                NumberOfRows = queryResult.Count(),
                RecordsFiltered = queryResult.Count(),
                Data = data.ToArray(),
                Message = "Success"
            };
        }
    }
}
