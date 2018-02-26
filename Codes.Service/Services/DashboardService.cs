using Codes.Service.Data;
using Codes.Service.Interfaces;
using Codes.Service.Models;
using Codes.Service.ViewModels;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;

namespace Codes.Service.Services
{
    public class DashboardService : IDashboardService
    {
        private readonly ILogger _logger;
        private readonly CodesDbContext _context;

        public DashboardService(CodesDbContext context, ILoggerFactory loggerFactory)
        {
            _context = context;
            _logger = loggerFactory.CreateLogger<CodeService>();
        }

        public DashboardViewModel GetAdmin()
        {
            var model = new DashboardViewModel();

            var totals = GetTotals("Physical");
            model.PhysicalCardsPurchased = totals.Item1;
            model.PhysicalCardsInCampaigns = totals.Item2;
            model.PhysicalCardsActivated = totals.Item3;

            totals = GetTotals("Virtual");
            model.VirtualCardsGenerated = totals.Item1;
            model.VirtualCardsInCampaigns = totals.Item2;
            model.VirtualCardsActivated = totals.Item3;

            var distribution1 = new CardDistributionViewModel
            {
                Name = "Test 1",
                PhysicalCardsTotal = 1000000,
                PhysicalCardsActivated = 884292,
                VirtualCardsTotal = 1000000,
                VirtualCardsActivated = 884294,
            };

            var distribution2 = new CardDistributionViewModel
            {
                Name = "Test 2",
                PhysicalCardsTotal = 1000000,
                PhysicalCardsActivated = 884294,
                VirtualCardsTotal = 1000000,
                VirtualCardsActivated = 884294,
            };

            var distributions = new List<CardDistributionViewModel>();
            distributions.Add(distribution1);
            distributions.Add(distribution2);

            model.CardDistributions = distributions;

            return model;
        }

        private (int, int, int) GetTotals(string codeType)
        {
            int totalPurchases = 0;
            int totalInCampaigns = 0;
            int totalActivated = 0;

            var purchases = _context.CodeRanges.Where(cr => cr.CodeType == codeType);

            if (codeType == "Physical")
            {
                totalPurchases = purchases.Select(cr => (cr.EndNumber - cr.StartNumber) / cr.IncrementByNumber).Sum();
            }
            else
            {
                totalPurchases = purchases.Select(cr => cr.NumberOfUses).Sum();
            }

            totalInCampaigns = _context.UnusedCodes.Count(cr => cr.CodeType == codeType);
            totalInCampaigns += _context.PendingCodes.Count(cr => cr.CodeType == codeType);

            totalActivated += _context.UsedCodes.Count(cr => cr.CodeType == codeType);

            return (totalPurchases, totalInCampaigns, totalActivated);
        }
    }
}
