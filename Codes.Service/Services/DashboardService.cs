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
            return _dashboardReports.GetAdmin();
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

            return model;
        }

        public DashboardSelectViewModel GetListBrokers()
        {
            var model = new DashboardSelectViewModel() { Role = "Broker" };

            model.Accounts = _context.Brokers.Where(b => b.IsActive)
                .Select(b => new DashboardAccountViewModel()
                {
                    Name = $"{b.BrokerFirstName} {b.BrokerLastName}",
                    AccountId = b.BrokerId
                });

            return model;
        }

        public DashboardSelectViewModel GetListAgents()
        {
            var model = new DashboardSelectViewModel() { Role = "Agent" };

            model.Accounts = _context.Agents.Where(a => a.IsActive && a.Broker.IsActive)
                .Select(a => new DashboardAccountViewModel()
                {
                    Name = $"{a.AgentFirstName} {a.AgentLastName}",
                    AccountId = a.AgentId
                });

            return model;
        }

        public DashboardSelectViewModel GetListClients()
        {
            var model = new DashboardSelectViewModel() { Role = "Client" };

            model.Accounts = _context.Clients.Where(c => c.IsActive && c.Broker.IsActive)
                .Select(c => new DashboardAccountViewModel()
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
