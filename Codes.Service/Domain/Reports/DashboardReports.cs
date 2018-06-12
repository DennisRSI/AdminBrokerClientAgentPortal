using Codes.Service.ViewModels;
using System.Data.SqlClient;

namespace Codes.Service.Domain
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
            var model = CallDashboardBroker(null);
            model.DistributionDetailType = "Broker";

            return model;
        }

        public DashboardViewModel GetBroker(int brokerId)
        {
            return CallDashboardBroker(brokerId);
        }

        private DashboardViewModel CallDashboardBroker(int? brokerId)
        {
            var parameters = new[]
            {
                new SqlParameter("@BrokerId", brokerId),
            };

            var table = _dataAccess.ExecuteDataTable("DashboardBroker", parameters);
            var row = table.Rows[0];

            var model = new DashboardViewModel()
            {
                DistributionDetailType = "Client",
                PhysicalCardsPurchased = (int)row["PhysicalTotal"],
                PhysicalCardsActivated = (int)row["PhysicalActivated"],
                VirtualCardsGenerated = (int)row["VirtualTotal"],
                VirtualCardsActivated = (int)row["VirtualActivated"]
            };

            return model;
        }
    }
}
