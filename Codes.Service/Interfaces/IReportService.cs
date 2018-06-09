using Codes.Service.ViewModels;

namespace Codes.Service.Interfaces
{
    public interface IReportService
    {
        DataTableViewModel<ActivationCardViewModel> GetDataActivation(ActivationReportViewModel model);
    }
}
