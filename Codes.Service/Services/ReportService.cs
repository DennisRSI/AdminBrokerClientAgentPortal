using ClientPortal.Models;
using Codes.Service.Data;
using Codes.Service.Domain;
using Codes.Service.Interfaces;
using Codes.Service.ViewModels;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

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

        public DataTableViewModel<ActivationCardViewModel> GetDataActivation(ActivationReportViewModel model)
        {
            const string procedureName = "ReportActivations";
            var result = new DataTableViewModel<ActivationCardViewModel>();

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
                new SqlParameter("@BrokerId", model.BrokerId),
                new SqlParameter("@AgentId", model.AgentId),
                new SqlParameter("@ClientId", model.ClientId),
                new SqlParameter("@CampaignStatus", model.CampaignStatus),
                new SqlParameter("@IsCardUsed", model.IsCardUsed),
                totalCount
            };

            var table = _dataAccess.ExecuteDataTable(procedureName, parameters);
            var resultData = new List<ActivationCardViewModel>();

            foreach (DataRow row in table.Rows)
            {
                var activation = new ActivationCardViewModel()
                {
                    CardNumber = (string)row["CardNumber"],
                    ActivationDate = (DateTime)row["ActivationDate"],
                    FirstName = (string)row["FirstName"],
                    LastName = (string)row["LastName"],
                    Denomination = (Single)row["Denomination"],
                    CardType = (string)row["CardType"],
                    IsCardUsed = (string)row["CardUsed"],
                    CampaignName = (string)row["Campaign"],
                    CardStatus = (string)row["Status"],
                    Phone = (string)row["Phone"],
                    Email = (string)row["Email"],
                    PostalCode = (string)row["PostalCode"]
                };

                resultData.Add(activation);
            }

            result.Data = resultData.ToArray();

            return result;
        }

        public ProductionResultDetailViewModel GetProductionResultDetail(ProductionDetailQuery query)
        {
            const string procedureName = "ReportProductionDetails";

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
                new SqlParameter("@BrokerId", query.BrokerId),
                new SqlParameter("@AgentId", query.AgentId),
                new SqlParameter("@ClientId", query.ClientId),
                new SqlParameter("@Search", ""),
                totalCount
            };

            var table = _dataAccess.ExecuteDataTable(procedureName, parameters);
            var results = new List<ProductionDetailItemViewModel>();

            int totalNights = 0;
            decimal totalInternetPrice = 0;
            decimal totalYouPayPrice = 0;
            Single totalMemberSavings = 0;
            Single totalPointsBalance = 0;
            decimal totalCommission = 0;

            foreach (DataRow row in table.Rows)
            {
                var item = new ProductionDetailItemViewModel()
                {
                    ConfirmationNumber = (string)row["ConfirmationNumber"],
                    CardNumber = (string)row["CardNumber"],
                    MemberFullName = GetAbbreviatedName((string)row["MemberFirstName"], (string)row["MemberLastName"]),
                    GuestFullName = String.Empty,
                    BookingDate = (DateTime)row["BookingDate"],
                    CheckInDate = (DateTime)row["CheckInDate"],
                    CheckOutDate = (DateTime)row["CheckOutDate"],
                    Canceled = (string)row["Canceled"],
                    InternetPrice = (decimal)row["InternetPrice"],
                    YouPayPrice = (decimal)row["YouPayPrice"],
                    MemberSavings = (Single)row["MemberSavings"],
                    PointsBalance = (Single)row["PointsBalance"],
                };

                totalNights += (item.CheckOutDate - item.CheckInDate).Days;
                totalInternetPrice += item.InternetPrice;
                totalYouPayPrice += item.YouPayPrice;
                totalMemberSavings += item.MemberSavings;
                totalPointsBalance += item.PointsBalance;
                totalCommission += 0;

                results.Add(item);
            }

            var model = new ProductionResultDetailViewModel
            {
                CheckoutStartDate = query.CheckOutStartDate,
                CheckoutEndDate = query.CheckOutEndDate,
                BookingStartDate = query.BookingStartDate,
                BookingEndDate = query.BookingEndDate,
                DetailsTable = results,
                TotalNights = totalNights,
                TotalInternetPrice = totalInternetPrice,
                TotalYouPayPrice = totalYouPayPrice,
                TotalMemberSavings = totalMemberSavings,
                TotalPointsBalance = totalPointsBalance,
                TotalCommission = totalCommission
            };

            return model;
        }

        public ProductionResultSummaryViewModel GetProductionResultSummary(ProductionSummaryQuery query)
        {
            string procedureName = "ReportProductionBy" + query.QueryType;
            string reportGroupName = null;
            string accountNameColumn = "Client";

            switch (query.QueryType.ToLower())
            {
                case "broker":
                    reportGroupName = "Client";
                    break;

                case "agent":
                    reportGroupName = "Client";
                    break;

                case "client":
                    reportGroupName = "Campaign";
                    break;
            }

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
                    new SqlParameter("@" + query.QueryType + "Id", accountId),
                    new SqlParameter("@BookingStartDate", query.BookingStartDate),
                    new SqlParameter("@BookingEndDate", query.BookingEndDate),
                    new SqlParameter("@CheckInDate", query.CheckOutStartDate),
                    new SqlParameter("@CheckOutDate", query.CheckOutEndDate),
                    new SqlParameter("@StartRowIndex", Convert.ToInt32(0)),
                    new SqlParameter("@NumberOfRows", 30000),
                    totalCount
                };

                var table = _dataAccess.ExecuteDataTable(procedureName, parameters);

                var result = new ProductionSummaryTableViewModel
                {
                    ReportGroupName = reportGroupName,
                    AccountType = query.QueryType,
                    Items = new List<ProductionSummaryItemViewModel>()
                };

                model.Tables.Add(result);

                foreach (DataRow row in table.Rows)
                {
                    result.AccountName = (string)row[query.QueryType + "FirstName"] + " " + (string)row[query.QueryType + "LastName"];

                    var item = new ProductionSummaryItemViewModel()
                    {
                        AccountName = ReadColumn.GetString(row, accountNameColumn),
                        InternetPrice = ReadColumn.GetDecimal(row, "InternetPrice"),
                        YouPayPrice = ReadColumn.GetDecimal(row, "YouPayPrice"),
                        MemberSavings = ReadColumn.GetDouble(row, "MemberSavings"),
                        CommissionEarned = ReadColumn.GetDecimal(row, "CommissionEarned"),
                    };

                    result.TotalInternetPrice += item.InternetPrice;
                    result.TotalYouPayPrice += item.YouPayPrice;
                    result.TotalMemberSavings += item.MemberSavings;
                    result.TotalCommissionEarned += 0;

                    model.TotalInternetPrice += item.InternetPrice;
                    model.TotalYouPayPrice += item.YouPayPrice;
                    model.TotalMemberSavings += item.MemberSavings;
                    model.TotalCommissionEarned += 0;

                    result.Items.Add(item);
                }
            }

            return model;
        }

        private string GetAbbreviatedName(string firstName, string lastName)
        {
            var initial = firstName.Substring(0, 1).ToUpper();
            return $"{initial}. {lastName}";
        }
    }

    public class ProductionDetailQuery
    {
        public DateTime BookingStartDate { get; set; }
        public DateTime BookingEndDate { get; set; }
        public DateTime CheckOutStartDate { get; set; }
        public DateTime CheckOutEndDate { get; set; }
        public int? BrokerId { get; set; }
        public int? AgentId { get; set; }
        public int? ClientId { get; set; }
    }

    public class ProductionSummaryQuery
    {
        public DateTime BookingStartDate { get; set; }
        public DateTime BookingEndDate { get; set; }
        public DateTime CheckOutStartDate { get; set; }
        public DateTime CheckOutEndDate { get; set; }
        public string QueryType { get; set; }
        public IEnumerable<int> AccountIds { get; set; }
    }
}
