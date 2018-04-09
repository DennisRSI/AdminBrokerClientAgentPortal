﻿using Codes.Service.Data;
using Codes.Service.Interfaces;
using Codes.Service.Models;
using Codes.Service.ViewModels;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;

namespace Codes.Service.Services
{
    public class ViewDataService : IViewDataService
    {
        private readonly ILogger _logger;
        private readonly CodesDbContext _context;

        public ViewDataService(CodesDbContext context, ILoggerFactory loggerFactory)
        {
            _context = context;
            _logger = loggerFactory.CreateLogger<CodeService>();
        }

        public ClientDetailsViewModel GetClientDetails(int clientId)
        {
            var client = _context.Clients.Single(c => c.ClientId == clientId);

            var model = new ClientDetailsViewModel
            {
                ApplicationReference = client.ApplicationReference,
                ClientId = client.ClientId,
                PhysicalTotal = 0,
                PhysicalInCampaigns = 0,
                PhysicalAvailable = 0,
                VirtualTotal = 0,
                VirtualInCampaigns = 0,
                VirtualAvailable = 0,
            };

            model.Campaigns = _context.Campaigns
                .Where(c => c.ClientId == client.ClientId)
                .Select(c =>
                            new ClientDetailsCampaignViewModel()
                            {
                                CampaignId = c.CampaignId,
                                CampaignName = c.CampaignName,
                                CardQuantity = c.CardQuantity,
                                CardType = c.CampaignType,
                                Benefits = "?",
                                Status = "?"
                            }
                        );

            return model;
        }
    }
}