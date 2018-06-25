using ClientPortal.Models;
using Codes.Service.Services;
using Codes.Service.ViewModels;
using System.Collections.Generic;

namespace Codes.Service.Interfaces
{
    public interface IReportService
    {
        DataTableViewModel<ActivationCardViewModel> GetDataActivation(ActivationReportViewModel model);
        ProductionResultDetailViewModel GetProductionResultDetail(ProductionDetailQuery query);
    }
}
