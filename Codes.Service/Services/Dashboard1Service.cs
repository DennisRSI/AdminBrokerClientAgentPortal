using Codes1.Service.Data;
using Codes1.Service.Domain;
using Codes1.Service.Interfaces;
using Codes1.Service.Models;
using Codes1.Service.ViewModels;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Codes1.Service.Services
{
    public class Dashboard1Service : IDashboard1Service
    {
        private readonly ILogger _logger;
        private readonly Codes1DbContext _context;
        private readonly string _connectionString;
        private readonly DashboardReports _dashboardReports;

        public Dashboard1Service(Codes1DbContext context, ILoggerFactory loggerFactory, IConfiguration configuration)
        {
            _context = context;
            _logger = loggerFactory.CreateLogger<Code1Service>();
            _connectionString = configuration.GetConnectionString("CodeGeneratorConnection");
            _dashboardReports = new DashboardReports(_connectionString);
        }

        public DashboardViewModel GetAdmin()
        {
            return _dashboardReports.GetAdmin();
        }

        public DashboardViewModel GetBroker(int id)
        {
            var model = _dashboardReports.GetBroker(id);
            var broker = _context.Brokers.Single(b => b.BrokerId == id);
            model.AccountName = $"{broker.BrokerFirstName} {broker.BrokerLastName}";

            return model;
        }

        public DashboardViewModel GetAgent(int id)
        {
            /*var model = new DashboardViewModel() { DistributionDetailType = "Client" };
            var agent = _context.Agents.Single(a => a.AgentId == id);
            model.AccountName = $"{agent.AgentFirstName} {agent.AgentLastName}";

            //var campaigns = _context.CampaignAgents.Where(ca => ca.AgentId == id).Select(ca => ca.Campaign);
            var clientIds = _context.ClientAgents.Where(ca => ca.AgentId == id).Select(ca => ca.ClientId);
            var campaigns = (from c in _context.Campaigns
                             join a in _context.ClientAgents on c.ClientId equals a.ClientId
                             where a.AgentId == id
                             select c);

            //var campaigns = _context.ClientAgents.Where(ca => ca.AgentId == id).Select(ca => ca.Client.Campaigns);
            //var campaigns = _context.Campaigns.Where(c => campaignAgents.Contains(c.ClientId) )
            (int, int) totals = GetTotalsFromCampaigns(CodeType.Physical, campaigns);
            model.PhysicalCardsActivated = totals.Item2;
            model.PhysicalCardsPurchased = totals.Item1;
            //model.PhysicalCardsActivated = _context.UnusedCodes.Where(x => x.CampaignId)

            totals = GetTotalsFromCampaigns(CodeType.Virtual, campaigns);
            model.VirtualCardsActivated = totals.Item2;
            model.VirtualCardsGenerated = totals.Item1;
            */

            var model = _dashboardReports.GetAgent(id);
            var agent = _context.Agents.Single(b => b.AgentId == id);
            model.AccountName = $"{agent.AgentFirstName} {agent.AgentLastName}";
            return model;
        }

        public DashboardViewModel GetClient(int clientId)
        {
            /*var model = new DashboardViewModel() { DistributionDetailType = "Campaign" };
            var client = _context.Clients.Single(c => c.ClientId == clientId);
            model.AccountName = client.CompanyName;

            var campaigns = _context.Campaigns.Where(c => c.ClientId == clientId);

            var totals = GetTotalsFromCampaigns(CodeType.Physical, campaigns);
            model.PhysicalCardsActivated = totals.Item2;

            totals = GetTotalsFromCampaigns(CodeType.Virtual, campaigns);
            model.VirtualCardsActivated = totals.Item2;

            return model;*/

            var model = _dashboardReports.GetClient(clientId);
            var client = _context.Clients.Single(b => b.ClientId == clientId);
            model.AccountName = $"{client.ContactFirstName} {client.ContactLastName}";
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
                    Name = c.CompanyName,
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


        private (int, int) GetTotalsFromCampaigns(string physical, IQueryable<ICollection<CampaignModel>> campaigns)
        {
            throw new NotImplementedException();
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
