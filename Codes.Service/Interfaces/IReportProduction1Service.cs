using ClientPortal.Models;
using Codes1.Service.Services;
using System;
using System.Threading.Tasks;

namespace Codes1.Service.Interfaces
{
    public interface IReportProduction1Service
    {
        Task<ProductionResultSummaryViewModel> GetProductionResultCampaignAsync(ProductionSummaryQuery query);

        Task<ProductionResultSummaryViewModel> V2_GetProductionResultCampaignAsync(int startRowIndex = 0, int numberOfRows = 9000000,
            int? campaignId = null, int? brokerId = null, int? clientId = null, int? agentId = null,
            DateTime? reservationStartDate = null, DateTime? reservationEndDate = null, DateTime? checkInDate = null, DateTime? checkOutDate = null, bool? showOnlyPaid = null);

        Task<ProductionResultSummaryViewModel> V2_getProductionResultClientAsync(int startRowIndex = 0, int numberOfRows = 9000000,
            int? campaignId = null, int? brokerId = null, int? clientId = null, int? agentId = null,
            DateTime? reservationStartDate = null, DateTime? reservationEndDate = null, DateTime? checkInDate = null, DateTime? checkOutDate = null, bool? showOnlyPaid = null);
    }

}
