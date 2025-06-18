using Codes1.Service.ViewModels;
using System.Data;
using System.Data.SqlClient;

namespace Codes1.Service.Domain
{
    public class DashboardReports
    {
        private readonly DataAccess _dataAccess;

        public DashboardReports(string connectionString)
        {
            _dataAccess = new DataAccess(connectionString);
        }

        public DashboardViewModel GetAdmin()
        {
            // Broker and Admin share the same proc
            var model = CallDashboardBroker(null, null, null);
            model.DistributionDetailType = "Broker";

            return model;
        }

        public DashboardViewModel GetBroker(int brokerId)
        {
            return CallDashboardBroker(brokerId, null, null);
        }

        public DashboardViewModel GetAgent(int agentId)
        {
            return CallDashboardBroker(null, null, agentId);
        }

        public DashboardViewModel GetClient(int clientId)
        {
            return CallDashboardBroker(null, clientId, null);
        }

        private DashboardViewModel CallDashboardBroker(int? brokerId = null, int? clientId = null, int? agentId = null)
        {
            var parameters = new[]
            {
                new SqlParameter("@BrokerId", brokerId),
                new SqlParameter("@ClientId", clientId),
                new SqlParameter("@AgentId", agentId),
            };

            DataTable table = null;
            if(brokerId == null && clientId == null && agentId == null)
                table = _dataAccess.ExecuteDataTable("DashboardBroker", parameters);
            else
                table = _dataAccess.ExecuteDataTable("V2_DashboardBroker", parameters);

            var row = table.Rows[0];

            var model = new DashboardViewModel()
            {
                DistributionDetailType = "Client",
                PhysicalCardsPurchased = (int)row["PhysicalTotal"],
                PhysicalCardsActivated = (int)row["PhysicalActivated"],
                VirtualCardsGenerated = (int)row["VirtualTotal"],
                VirtualCardsActivated = (int)row["VirtualActivated"],

                OverviewSavings = (decimal)row["MemberSavings"],
                OverviewCommisionsPaid = (decimal)row["CommissionsPaid"],
                OverviewCommisionsOwed = (decimal)row["CommissionsOwed"],

                HotelSavings = (decimal)row["HotelSavings"],
                HotelCommissionsPaid = (decimal)row["HotelCommissionsPaid"],
                HotelCommissionsOwed = (decimal)row["HotelCommissionsOwed"],
                TotalCommissionOwed = (decimal)row["HotelOrigionalCommissionsOwed"] + (decimal)row["CondoOrigionalCommissionsOwed"],
                CondoCompanyCommissionsOwed = (decimal)row["CondoOrigionalCommissionsOwed"],
                HotelCompanyCommissionsOwed = (decimal)row["HotelOrigionalCommissionsOwed"],

                CondoSavings =  (decimal)row["CondoSavings"],
                CondoCommissionsPaid = (decimal)row["CondoCommissionsPaid"],
                CondoCommissionsOwed = (decimal)row["CondoCommissionsOwed"],
            };

            return model;
        }
    }
}
