using ClientPortal.Models;
using Codes1.Service.Services;
using Codes1.Service.ViewModels;
using System;
using System.Threading.Tasks;

namespace Codes1.Service.Interfaces
{
    public interface IReportCommission1Service
    {
        Task<CommissionResultViewModel> GetCommissionResultBrokerAsync(CommissionQuery query);
        Task<CommissionResultViewModel> GetCommissionResultSourceAsync(CommissionQuery query);
        Task<CommissionResultViewModel> GetCommissionResultClientAsync(CommissionQuery query);
        Task<CommissionResultViewModel> GetCommissionResultsBrokerNewAsync(DateTime checkoutStartDate, DateTime checkoutEndDate,
            int? brokerId = null, int? agentId = null, int? clientId = null, string paymentStatus = null);
    }
}
