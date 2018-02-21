﻿using AutoMapper;
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

        public void Create(int clientId, CampaignViewModel viewModel)
        {
           var model = new CampaignModel
           {
                BrokerId = 1, // Is the broker needed here because the client already is assigned to a broker?
                CampaignDescription = viewModel.CampaignDescription,
                CampaignName = viewModel.CampaignName,
                CampaignType = viewModel.CampaignType,
                CustomCSS = viewModel.CustomCSS, // TODO: Need to split this into two
                EndDateTime = viewModel.EndDateTime,
                GoogleAnalyticsCode = viewModel.GoogleAnalyticsCode,
                IsActive = true,
                StartDateTime = viewModel.StartDateTime,
                ClientId = clientId,
                PostLoginVideoId = viewModel.PostLoginVideoId,
                PreLoginVideoId = viewModel.PreLoginVideoId,
                BenefitCondo = viewModel.BenefitCondo,
                BenefitHotel = viewModel.BenefitHotel,
                BenefitShopping = viewModel.BenefitShopping,
                CardQuantity = viewModel.CardQuantity
            };

            _context.Campaigns.Add(model);
            _context.SaveChanges();
        }
    }
}
