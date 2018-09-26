using ClientPortal.Models;
using Codes.Service.Services;
using System.Threading.Tasks;

namespace Codes.Service.Interfaces
{
    public interface IReportProductionService
    {
        Task<ProductionResultSummaryViewModel> GetProductionResultCampaignAsync(ProductionSummaryQuery query);
    }
}
