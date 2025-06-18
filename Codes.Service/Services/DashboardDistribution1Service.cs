using Codes1.Service.Data;
using Codes1.Service.Domain;
using Codes1.Service.Interfaces;
using Codes1.Service.Models;
using Codes1.Service.ViewModels;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Codes1.Service.Services
{
    public class DashboardDistribution1Service : IDashboardDistribution1Service
    {
        private readonly ILogger _logger;
        private readonly Codes1DbContext _context;
        private readonly DataAccess _dataAccess;

        public DashboardDistribution1Service(Codes1DbContext context, ILoggerFactory loggerFactory, IConfiguration configuration)
        {
            _context = context;
            _logger = loggerFactory.CreateLogger<Code1Service>();

            var connectionString = configuration.GetConnectionString("CodeGeneratorConnection");
            _dataAccess = new DataAccess(connectionString);
        }

        public DataTableViewModel<CardDistributionViewModel> GetAdmin()
        {
            return GetModel("DashboardDistributionAdmin", "@AdminId", 0);
        }

        public DataTableViewModel<CardDistributionViewModel> GetBroker(int brokerId)
        {
            return GetModel("DashboardDistributionBrokerNew", "@BrokerId", brokerId);
        }

        public DataTableViewModel<CardDistributionViewModel> GetAgent(int agentId)
        {
            return GetModel("DashboardDistributionAgent", "@AgentId", agentId);
        }

        public DataTableViewModel<CardDistributionViewModel> GetClient(int clientId)
        {
            return GetModel("DashboardDistributionClient", "@ClientId", clientId);
        }

        private DataTableViewModel<CardDistributionViewModel> GetModel(string procedureName, string parameterName, int id)
        {
            var parameters = new[]
            {
                new SqlParameter(parameterName,  Convert.ToInt32(id)),
            };

            var model = new DataTableViewModel<CardDistributionViewModel>();
            var table = _dataAccess.ExecuteDataTable(procedureName, parameters);
            var results = new List<CardDistributionViewModel>();

            foreach (DataRow row in table.Rows)
            {
                var result = new CardDistributionViewModel()
                {
                    Name = (string)row["FullName"],
                    PhysicalTotal = (int)row["PhysicalTotal"],
                    PhysicalAvailableActivations = (int)row["PhysicalAvailableActivations"],
                    PhysicalActivated = (int)row["PhysicalActivated"],
                    VirtualTotal = (int)row["VirtualTotal"],
                    VirtualAvailableActivations = (int)row["VirtualAvailableActivations"],
                    VirtualActivated = (int)row["VirtualActivated"],
                };

                results.Add(result);
            }

            model.Data = results.ToArray();
            return model;
        }
    }
}
