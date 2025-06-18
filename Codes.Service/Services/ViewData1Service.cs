using Codes1.Service.Data;
using Codes1.Service.Interfaces;
using Codes1.Service.ViewModels;
using Microsoft.Extensions.Logging;
using System.Linq;

namespace Codes1.Service.Services
{
    public class ViewData1Service : IViewData1Service
    {
        private readonly ILogger _logger;
        private readonly Codes1DbContext _context;
        private readonly IAccount1Service _accountService;

        public ViewData1Service(Codes1DbContext context, ILoggerFactory loggerFactory, IAccount1Service accountService)
        {
            _context = context;
            _logger = loggerFactory.CreateLogger<Code1Service>();
            _accountService = accountService;
        }

        public ClientDetailsViewModel GetClientDetails(int clientId)
        {
            var client = _context.Clients.Single(c => c.ClientId == clientId);

            var model = new ClientDetailsViewModel
            {
                ApplicationReference = client.ApplicationReference,
                ClientId = client.ClientId,
                CompanyName = client.CompanyName,
                PhysicalInCampaigns = _accountService.GetCardQuantityByClient(clientId, "Physical"),
                VirtualInCampaigns = _accountService.GetCardQuantityByClient(clientId, "Virtual")
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
