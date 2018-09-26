using ClientPortal.Models;
using Codes.Service.Services;
using Codes.Service.ViewModels;
using System.Threading.Tasks;

namespace Codes.Service.Interfaces
{
    public interface IReportService
    {
        DataTableViewModel<ActivationCardViewModel> GetDataActivation(ActivationReportViewModel model);
        Task<ProductionResultDetailViewModel> GetProductionResultDetail(ProductionDetailQuery query);
        Task<ProductionResultSummaryViewModel> GetProductionResultSummaryAsync(ProductionSummaryQuery query);
    }
}
