using Codes.Service.Data;
using Codes.Service.Domain;
using Codes.Service.Interfaces;
using Codes.Service.Models;
using Codes.Service.ViewModels;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;

namespace Codes.Service.Services
{
    public class DashboardService : IDashboardService
    {
        private readonly ILogger _logger;
        private readonly CodesDbContext _context;
        private readonly string _connectionString;
        private readonly DashboardReports _dashboardReports;

        public DashboardService(CodesDbContext context, ILoggerFactory loggerFactory, IConfiguration configuration)
        {
            _context = context;
            _logger = loggerFactory.CreateLogger<CodeService>();
            _connectionString = configuration.GetConnectionString("CodeGeneratorConnection");
            _dashboardReports = new DashboardReports(_connectionString);
        }

        public DashboardViewModel GetAdmin()
        {
            var model = new DashboardViewModel() { DistributionDetailType = "Broker" };

            var totals = GetTotals(CodeType.Physical);
            model.PhysicalCardsPurchased = totals.Item1;
            model.PhysicalCardsActivated = totals.Item3;

            totals = GetTotals(CodeType.Virtual);
            model.VirtualCardsGenerated = totals.Item1;
            model.VirtualCardsActivated = totals.Item3;

            model.CardDistributions = GetCardDistributionAdmin();

            return model;
        }

        public DashboardViewModel GetBroker(int id)
        {
            return _dashboardReports.GetBroker(id);
        }

        public DashboardViewModel GetAgent(int id)
        {
            var model = new DashboardViewModel() { DistributionDetailType = "Client" };

            var campaigns = _context.CampaignAgents.Where(ca => ca.AgentId == id).Select(ca => ca.Campaign);

            var totals = GetTotalsFromCampaigns(CodeType.Physical, campaigns);
            model.PhysicalCardsActivated = totals.Item2;

            totals = GetTotalsFromCampaigns(CodeType.Virtual, campaigns);
            model.VirtualCardsActivated = totals.Item2;

            model.CardDistributions = GetCardDistribution(campaigns);

            return model;
        }

        public DashboardViewModel GetClient(int clientId)
        {
            var model = new DashboardViewModel() { DistributionDetailType = "Campaign" };

            var campaigns = _context.Campaigns.Where(c => c.ClientId == clientId);

            var totals = GetTotalsFromCampaigns(CodeType.Physical, campaigns);
            model.PhysicalCardsActivated = totals.Item2;

            totals = GetTotalsFromCampaigns(CodeType.Virtual, campaigns);
            model.VirtualCardsActivated = totals.Item2;

            model.CardDistributions = GetCardDistribution(campaigns);

            return model;
        }

        public DashboardSelectViewModel GetListBrokers()
        {
            var model = new DashboardSelectViewModel() { Role = "Broker" };

            model.Accounts = _context.Brokers.Select(b => new DashboardAccountViewModel()
            {
                Name = $"{b.BrokerFirstName} {b.BrokerLastName}",
                AccountId = b.BrokerId
            });

            return model;
        }

        public DashboardSelectViewModel GetListAgents()
        {
            var model = new DashboardSelectViewModel() { Role = "Agent" };

            model.Accounts = _context.Agents.Select(a => new DashboardAccountViewModel()
            {
                Name = $"{a.AgentFirstName} {a.AgentLastName}",
                AccountId = a.AgentId
            });

            return model;
        }

        public DashboardSelectViewModel GetListClients()
        {
            var model = new DashboardSelectViewModel() { Role = "Client" };

            model.Accounts = _context.Clients.Select(c => new DashboardAccountViewModel()
            {
                Name = $"{c.ContactFirstName} {c.ContactLastName}",
                AccountId = c.ClientId
            });

            return model;
        }

        private (int, int, int) GetTotals(string codeType, int brokerId = 0)
        {
            int totalPurchases = 0;
            int totalInCampaigns = 0;
            int totalActivated = 0;

            var purchases = GetCodeRanges(codeType, brokerId);
            totalPurchases = purchases.Select(p => p.GetTotalCodes()).Sum();
            
            totalInCampaigns += GetUnusedCodes(codeType, brokerId).Count();
            totalInCampaigns += GetPendingCodes(codeType, brokerId).Count();

            totalActivated += GetUsedCodes(codeType, brokerId).Count();

            return (totalPurchases, totalInCampaigns, totalActivated);
        }

        private (int, int) GetTotalsFromCampaigns(string codeType, IQueryable<CampaignModel> campaigns)
        {
            var campaignIds = campaigns.Select(c => c.CampaignId);

            var unused = _context.UnusedCodes.Where(c => c.CodeType == codeType && campaignIds.Contains(c.CampaignId.Value)).Count();
            var pending = _context.PendingCodes.Where(c => c.CodeType == codeType && campaignIds.Contains(c.CampaignId.Value)).Count();
            var used = _context.UsedCodes.Where(c => c.CodeType == codeType && campaignIds.Contains(c.CampaignId.Value)).Count();

            var totalInCampaigns = unused + pending + used;
            int totalActivated = used;

            return (totalInCampaigns, totalActivated);
        }

        private IEnumerable<CardDistributionViewModel> GetCardDistributionAdmin()
        {
            foreach (var broker in _context.Brokers)
            {
                var distribution = new CardDistributionViewModel
                {
                    Name = $"{broker.BrokerFirstName} {broker.BrokerLastName}"
                };

                distribution.PhysicalCardsTotal = _context.CodeRanges.Where(cr => cr.BrokerId == broker.BrokerId && cr.CodeType == CodeType.Physical)
                                    .Select(cr => cr.GetTotalCodes())
                                    .Sum();

                distribution.PhysicalCardsActivated = _context.UsedCodes.Where(c => c.BrokerId == broker.BrokerId && c.CodeType == CodeType.Physical).Count();

                distribution.VirtualCardsTotal = _context.CodeRanges.Where(cr => cr.BrokerId == broker.BrokerId && cr.CodeType == CodeType.Virtual)
                    .Select(cr => cr.GetTotalCodes())
                    .Sum();

                distribution.VirtualCardsActivated = _context.UsedCodes.Where(c => c.BrokerId == broker.BrokerId && c.CodeType == CodeType.Virtual).Count();

                yield return distribution;
            }
        }

        private IEnumerable<CardDistributionViewModel> GetCardDistributionBroker(int brokerId)
        {
            foreach (var client in _context.Clients.Where(c => c.BrokerId == brokerId))
            {
                var distribution = new CardDistributionViewModel()
                {
                    Name = client.CompanyName
                };

                var campaignsIds = _context.Campaigns.Where(c => c.ClientId == client.ClientId).Select(c => c.CampaignId).ToList();

                var unused = GetUnusedCodes(CodeType.Physical, brokerId).Count(c => campaignsIds.Contains(c.CampaignId.Value));
                var pending = GetPendingCodes(CodeType.Physical, brokerId).Count(c => campaignsIds.Contains(c.CampaignId.Value));
                var used = GetUsedCodes(CodeType.Physical, brokerId).Count(c => campaignsIds.Contains(c.CampaignId.Value));

                distribution.PhysicalCardsTotal = unused + pending + used;
                distribution.PhysicalCardsActivated = used;

                unused = GetUnusedCodes(CodeType.Virtual, brokerId).Count(c => campaignsIds.Contains(c.CampaignId.Value));
                pending = GetPendingCodes(CodeType.Virtual, brokerId).Count(c => campaignsIds.Contains(c.CampaignId.Value));
                used = GetUsedCodes(CodeType.Virtual, brokerId).Count(c => campaignsIds.Contains(c.CampaignId.Value));

                distribution.VirtualCardsTotal = unused + pending + used;
                distribution.VirtualCardsActivated = used;

                yield return distribution;
            }
        }

        private IEnumerable<CardDistributionViewModel> GetCardDistribution(IQueryable<CampaignModel> campaigns)
        {
            foreach (var campaign in campaigns)
            {
                var distribution = new CardDistributionViewModel()
                {
                    Name = campaign.CampaignName
                };

                var unused = GetUnusedCodes(CodeType.Physical).Count(c => c.CampaignId == campaign.CampaignId);
                var pending = GetPendingCodes(CodeType.Physical).Count(c => c.CampaignId == campaign.CampaignId);
                var used = GetUsedCodes(CodeType.Physical).Count(c => c.CampaignId == campaign.CampaignId);

                distribution.PhysicalCardsTotal = unused + pending + used;
                distribution.PhysicalCardsActivated = used;

                unused = GetUnusedCodes(CodeType.Virtual).Count(c => c.CampaignId == campaign.CampaignId);
                pending = GetPendingCodes(CodeType.Virtual).Count(c => c.CampaignId == campaign.CampaignId);
                used = GetUsedCodes(CodeType.Virtual).Count(c => c.CampaignId == campaign.CampaignId);

                distribution.VirtualCardsTotal = unused + pending + used;
                distribution.VirtualCardsActivated = used;

                yield return distribution;
            }
        }

        private IQueryable<CodeRangeModel> GetCodeRanges(string type, int brokerId = 0)
        {
            return _context.CodeRanges.Where(cr => cr.CodeType == type && (brokerId == 0 || cr.BrokerId == brokerId));
        }

        private IQueryable<PendingCodeModel> GetPendingCodes(string type, int brokerId = 0)
        {
            return _context.PendingCodes.Where(cr => cr.CodeType == type && (brokerId == 0 || cr.BrokerId == brokerId));
        }

        private IQueryable<UsedCodeModel> GetUsedCodes(string type, int brokerId = 0)
        {
            return _context.UsedCodes.Where(cr => cr.CodeType == type && (brokerId == 0 || cr.BrokerId == brokerId));
        }

        private IQueryable<UnusedCodeModel> GetUnusedCodes(string type, int brokerId = 0)
        {
            return _context.UnusedCodes.Where(cr => cr.CodeType == type && (brokerId == 0 || cr.BrokerId == brokerId));
        }

        private IQueryable<CodeRangeModel> GetCodeRangesByCampaign(string type, IQueryable<CampaignModel> campaigns)
        {
            var result1 = GetPendingCodesByCampaign(type, campaigns).Select(c => c.CodeRange);
            var result2 = GetUsedCodesByCampaign(type, campaigns).Select(c => c.CodeRange);
            var result3 = GetUnusedCodesByCampaign(type, campaigns).Select(c => c.CodeRange);

            return result1.Union(result2).Union(result3);
        }

        private IQueryable<PendingCodeModel> GetPendingCodesByCampaign(string type, IQueryable<CampaignModel> campaigns)
        {
            var campaignIds = campaigns.Select(c => c.CampaignId);
            return _context.PendingCodes.Where(cr => cr.CodeType == type && campaignIds.Contains(cr.CampaignId.Value));
        }

        private IQueryable<UsedCodeModel> GetUsedCodesByCampaign(string type, IQueryable<CampaignModel> campaigns)
        {
            var campaignIds = campaigns.Select(c => c.CampaignId);
            return _context.UsedCodes.Where(cr => cr.CodeType == type && campaignIds.Contains(cr.CampaignId.Value));
        }

        private IQueryable<UnusedCodeModel> GetUnusedCodesByCampaign(string type, IQueryable<CampaignModel> campaigns)
        {
            var campaignIds = campaigns.Select(c => c.CampaignId);
            return _context.UnusedCodes.Where(cr => cr.CodeType == type && campaignIds.Contains(cr.CampaignId.Value));
        }
    }
}
