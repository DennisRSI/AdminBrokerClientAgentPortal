using Codes.Service.Data;
using Codes.Service.Interfaces;
using Codes.Service.ViewModels;
using Microsoft.Extensions.Logging;
using System.Linq;

namespace Codes.Service.Services
{
    public class ViewDataService : IViewDataService
    {
        private readonly ILogger _logger;
        private readonly CodesDbContext _context;
        private readonly IAccountService _accountService;

        public ViewDataService(CodesDbContext context, ILoggerFactory loggerFactory, IAccountService accountService)
        {
            _context = context;
            _logger = loggerFactory.CreateLogger<CodeService>();
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
