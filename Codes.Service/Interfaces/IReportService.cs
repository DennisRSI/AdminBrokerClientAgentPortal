using ClientPortal.Models;
using Codes.Service.Services;
using Codes.Service.ViewModels;

namespace Codes.Service.Interfaces
{
    public interface IReportService
    {
        DataTableViewModel<ActivationCardViewModel> GetDataActivation(ActivationReportViewModel model);
        ProductionResultDetailViewModel GetProductionResultDetail(ProductionDetailQuery query);
        ProductionResultSummaryViewModel GetProductionResultSummary(ProductionSummaryQuery query);
    }
}
