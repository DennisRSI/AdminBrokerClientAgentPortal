using Codes.Service.Data;
using Codes.Service.Domain;
using Codes.Service.Interfaces;
using Codes.Service.Models;
using Codes.Service.ViewModels;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace Codes.Service.Services
{
    public class ReportService : IReportService
    {
        private readonly CodesDbContext _context;
        private readonly DashboardReports _dashboardReports;
        private readonly DataAccess _dataAccess;

        public ReportService(CodesDbContext context, IConfiguration configuration)
        {
            _context = context;

            var connectionString = configuration.GetConnectionString("CodeGeneratorConnection");
            _dataAccess = new DataAccess(connectionString);
        }

        public DataTableViewModel<ActivationResultViewModel> GetDataActivation(ActivationReportViewModel model)
        {
            const string procedureName = "ReportActivations";
            var result = new DataTableViewModel<ActivationResultViewModel>();

            var totalCount = new SqlParameter()
            {
                ParameterName = "@TotalCount",
                Value = 0,
                Direction = ParameterDirection.Output
            };

            var parameters = new []
            {
                new SqlParameter("@StartDate", model.StartDate),
                new SqlParameter("@EndDate", model.EndDate),
                new SqlParameter("@StartRowIndex", Convert.ToInt32(0)),
                new SqlParameter("@NumberOfRows", 10000),
                new SqlParameter("@SortColumn", "ActivationDate"),
                new SqlParameter("@SortDirection", "DESC"),
                new SqlParameter("@BrokerId", Convert.ToInt32(model.BrokerId)),
                new SqlParameter("@AgentId", Convert.ToInt32(model.AgentId)),
                new SqlParameter("@ClientId", Convert.ToInt32(model.ClientId)),
                new SqlParameter("@CampaignStatus", model.CampaignStatus),
                new SqlParameter("@IsCardUsed", model.IsCardUsed),
                totalCount
            };

            var table = _dataAccess.ExecuteDataTable(procedureName, parameters);
            var resultData = new List<ActivationResultViewModel>();

            foreach (DataRow row in table.Rows)
            {
                var activation = new ActivationResultViewModel()
                {
                    CardNumber = (string)row["CardNumber"],
                    ActivationDate = (DateTime)row["ActivationDate"],
                    MemberName = (string)row["Member"],
                    Denomination = (Single)row["Denomination"],
                    CardType = (string)row["CardType"],
                    IsCardUsed = (string)row["CardUsed"],
                    CampaignName = (string)row["Campaign"],
                    CardStatus = (string)row["Status"]
                };

                resultData.Add(activation);
            }

            result.Data = resultData.ToArray();

            return result;
        }
    }
}
