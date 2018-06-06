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

        public DashboardViewModel GetBroker(int brokerId)
        {
            var table = _dataAccess.ExecuteDataTable("DashboardBroker", "@BrokerId", brokerId);
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
