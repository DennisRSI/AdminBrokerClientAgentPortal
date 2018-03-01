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

            var totals = GetTotals(CodeType.Physical);
            model.PhysicalCardsPurchased = totals.Item1;
            model.PhysicalCardsInCampaigns = totals.Item2;
            model.PhysicalCardsActivated = totals.Item3;

            totals = GetTotals(CodeType.Virtual);
            model.VirtualCardsGenerated = totals.Item1;
            model.VirtualCardsInCampaigns = totals.Item2;
            model.VirtualCardsActivated = totals.Item3;

            model.CardDistributions = GetCardDistribution();

            return model;
        }

        private (int, int, int) GetTotals(string codeType)
        {
            int totalPurchases = 0;
            int totalInCampaigns = 0;
            int totalActivated = 0;

            var purchases = _context.CodeRanges.Where(cr => cr.CodeType == codeType);

            totalPurchases = purchases.Select(cr => cr.GetTotalCodes()).Sum();
            totalInCampaigns = _context.UnusedCodes.Count(cr => cr.CodeType == codeType);
            totalInCampaigns += _context.PendingCodes.Count(cr => cr.CodeType == codeType);
            totalActivated += _context.UsedCodes.Count(cr => cr.CodeType == codeType);

            return (totalPurchases, totalInCampaigns, totalActivated);
        }

        private IEnumerable<CardDistributionViewModel> GetCardDistribution()
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
    }
}
