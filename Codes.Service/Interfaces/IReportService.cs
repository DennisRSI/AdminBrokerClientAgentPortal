using Codes.Service.ViewModels;

namespace Codes.Service.Interfaces
{
    public interface IReportService
    {
        DataTableViewModel<ActivationResultViewModel> GetDataActivation(ActivationReportViewModel model);
    }
}
