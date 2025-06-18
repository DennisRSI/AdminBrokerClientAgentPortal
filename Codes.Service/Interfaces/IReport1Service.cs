using ClientPortal.Models;
using Codes1.Service.Services;
using Codes1.Service.ViewModels;
using System.Threading.Tasks;

namespace Codes1.Service.Interfaces
{
    public interface IReport1Service
    {
        DataTableViewModel<ActivationCardViewModel> GetDataActivation(ActivationReportViewModel model);
        Task<ProductionResultDetailViewModel> GetProductionResultDetail(ProductionDetailQuery query);
        Task<ProductionResultSummaryViewModel> GetProductionResultSummaryAsync(ProductionSummaryQuery query);
    }
}
