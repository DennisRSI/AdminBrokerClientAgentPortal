using Codes.Service.Data;
using Codes.Service.Domain;
using Codes.Service.Interfaces;
using Codes.Service.Models;
using Codes.Service.ViewModels;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Codes.Service.Services
{
    public class DashboardDistributionService : IDashboardDistributionService
    {
        private readonly ILogger _logger;
        private readonly CodesDbContext _context;
        private readonly DashboardReports _dashboardReports;
        private readonly DataAccess _dataAccess;

        public DashboardDistributionService(CodesDbContext context, ILoggerFactory loggerFactory, IConfiguration configuration)
        {
            _context = context;
            _logger = loggerFactory.CreateLogger<CodeService>();

            var connectionString = configuration.GetConnectionString("CodeGeneratorConnection");
            _dataAccess = new DataAccess(connectionString);
        }

        public DataTableViewModel<CardDistributionViewModel> GetAdmin()
        {
            var model = new DataTableViewModel<CardDistributionViewModel>();
            var table = _dataAccess.ExecuteDataTable("DashboardDistributionAdmin");
            var results = new List<CardDistributionViewModel>();

            foreach (DataRow row in table.Rows)
            {
                var result = new CardDistributionViewModel()
                {
                    Name = (string)row["BrokerFirstName"] + " " + (string)row["BrokerLastName"],
                    PhysicalTotal = (int)row["PhysicalTotal"],
                    PhysicalActivated = (int)row["PhysicalActivated"],
                    VirtualTotal = (int)row["VirtualTotal"],
                    VirtualActivated = (int)row["VirtualActivated"],
                };

                results.Add(result);
            }

            model.Data = results.ToArray();
            return model;
        }

        public DataTableViewModel<CardDistributionViewModel> GetAgent(int agentId)
        {
            throw new System.NotImplementedException();
        }

        public DataTableViewModel<CardDistributionViewModel> GetBroker(int brokerId)
        {
            throw new System.NotImplementedException();
        }

        public DataTableViewModel<CardDistributionViewModel> GetClient(int clientId)
        {
            throw new System.NotImplementedException();
        }
    }
}
