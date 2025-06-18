using Codes.Service.ViewModels.V2;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Codes1.Service.Interfaces.V2
{
    public interface ICodeServices
    {
        Task<DashboardViewModel> GetDashboardAsync(int brokerId = 0, int clientId = 0, int agentId = 0);
        Task<Codes1.Service.ViewModels.CommissionResultViewModel> ReportCommissionAsync(DateTime checkoutStartDate, DateTime checkoutEndDate, int startRowIndex = 0, int numberOfRows = 0, 
            int? brokerId = null, int? agentId = null, int? clientId = null, string paymentStatus = null);

    }
}
