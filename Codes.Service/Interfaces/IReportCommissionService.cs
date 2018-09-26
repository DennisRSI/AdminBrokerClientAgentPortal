using ClientPortal.Models;
using Codes.Service.Services;
using Codes.Service.ViewModels;
using System.Threading.Tasks;

namespace Codes.Service.Interfaces
{
    public interface IReportCommissionService
    {
        Task<CommissionResultViewModel> GetCommissionResultBrokerAsync(CommissionQuery query);
        Task<CommissionResultViewModel> GetCommissionResultSourceAsync(CommissionQuery query);
        Task<CommissionResultViewModel> GetCommissionResultClientAsync(CommissionQuery query);
    }
}
