using ClientPortal.Models;
using Codes.Service.Data;
using Codes.Service.Domain;
using Codes.Service.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Codes.Service.Services
{
    public class ReportProductionService : IReportProductionService
    {
        private readonly CodesDbContext _context;
        private readonly DataAccess _dataAccess;

        public ReportProductionService(CodesDbContext context, IConfiguration configuration)
        {
            _context = context;

            var connectionString = configuration.GetConnectionString("CodeGeneratorConnection");
            _dataAccess = new DataAccess(connectionString);
        }

        public async Task<ProductionResultSummaryViewModel> GetProductionResultCampaignAsync(ProductionSummaryQuery query)
        {
            var model = new ProductionResultSummaryViewModel
            {
                CheckoutStartDate = query.CheckOutStartDate,
                CheckoutEndDate = query.CheckOutEndDate,
                BookingStartDate = query.BookingStartDate,
                BookingEndDate = query.BookingEndDate,
                Tables = new List<ProductionSummaryTableViewModel>()
            };

            foreach (var accountId in query.AccountIds)
            {
                var totalCount = new SqlParameter()
                {
                    ParameterName = "@TotalCount",
                    Value = 0,
                    Direction = ParameterDirection.Output
                };

                var parameters = new[]
                {
                    new SqlParameter("@BookingStartDate", query.BookingStartDate),
                    new SqlParameter("@BookingEndDate", query.BookingEndDate),
                    new SqlParameter("@CheckOutStartDate", query.CheckOutStartDate),
                    new SqlParameter("@CheckOutEndDate", query.CheckOutEndDate),
                    new SqlParameter("@StartRowIndex", Convert.ToInt32(0)),
                    new SqlParameter("@NumberOfRows", 10000),
                    new SqlParameter("@SortColumn", "DEFAULT"),
                    new SqlParameter("@SortDirection", "ASC"),
                    new SqlParameter("@BrokerId", null),
                    new SqlParameter("@AgentId", null),
                    new SqlParameter("@ClientId", null),
                    new SqlParameter("@CampaignId", accountId),
                    new SqlParameter("@Search", String.Empty),
                    totalCount
                };

                var table = await _dataAccess.ExecuteDataTableAsync("ReportProductionDetails", parameters);

                if (table.Rows.Count == 0)
                {
                    continue;
                }

                var result = new ProductionSummaryTableViewModel
                {
                    IsCampaignReport = true,
                    ReportGroupName = "Member Name",
                    AccountType = query.QueryType,
                    Items = new List<ProductionSummaryItemViewModel>()
                };

                model.Tables.Add(result);

                foreach (DataRow row in table.Rows)
                {
                    result.AccountName = "Campaign Name Goes Here";

                    var item = new ProductionSummaryItemViewModel()
                    {
                        AccountName = row["MemberFirstName"] + " " + row["MemberLastName"],
                        InternetPrice = Convert.ToDecimal(row["InternetPrice"]),
                        YouPayPrice = Convert.ToDecimal(row["YouPayPrice"]),
                        MemberSavings = Convert.ToDecimal(row["MemberSavings"]),
                        CommissionEarned = Convert.ToDecimal(row["ClubCommissionDue"]),
                        TransactionDate = (DateTime?)row["BookingDate"]
                    };

                    result.TotalInternetPrice += item.InternetPrice;
                    result.TotalYouPayPrice += item.YouPayPrice;
                    result.TotalMemberSavings += item.MemberSavings;
                    result.TotalCommissionEarned += item.CommissionEarned;

                    model.TotalInternetPrice += item.InternetPrice;
                    model.TotalYouPayPrice += item.YouPayPrice;
                    model.TotalMemberSavings += item.MemberSavings;
                    model.TotalCommissionEarned += item.CommissionEarned;

                    result.Items.Add(item);
                }
            }

            return model;
        }
    }
}
