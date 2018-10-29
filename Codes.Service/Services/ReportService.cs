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
using System.Threading.Tasks;

namespace Codes.Service.Services
{
    public class ReportService : IReportService
    {
        private readonly CodesDbContext _context;
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
                new SqlParameter("@CampaignId", model.CampaignId),
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
                    PostalCode = (string)row["PostalCode"],
                    LoginCount = (int)row["LoginCount"]
                };

                resultData.Add(activation);
            }

            result.Data = resultData.ToArray();

            return result;
        }

        public async Task<ProductionResultDetailViewModel> GetProductionResultDetail(ProductionDetailQuery query)
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
                new SqlParameter("@CampaignId", query.CampaignId),
                new SqlParameter("@Search", String.Empty),
                totalCount
            };

            var table = await _dataAccess.ExecuteDataTableAsync(procedureName, parameters);

            var results = new List<ProductionDetailItemViewModel>();

            var model = new ProductionResultDetailViewModel
            {
                CheckoutStartDate = query.CheckOutStartDate,
                CheckoutEndDate = query.CheckOutEndDate,
                BookingStartDate = query.BookingStartDate,
                BookingEndDate = query.BookingEndDate,
                DetailsTable = results
            };

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
                    InternetPrice = Convert.ToDecimal(row["InternetPrice"]),
                    YouPayPrice = Convert.ToDecimal(row["YouPayPrice"]),
                    PointsBalance = Convert.ToSingle(row["PointsBalance"]),
                    Commission = Convert.ToDecimal(row["ClubCommissionDue"])
                };

                model.TotalNights += (item.CheckOutDate - item.CheckInDate).Days;
                model.TotalInternetPrice += item.InternetPrice;
                model.TotalYouPayPrice += item.YouPayPrice;
                model.TotalPointsBalance += item.PointsBalance;
                model.TotalCommission += item.Commission;

                results.Add(item);
            }

            return model;
        }

        public async Task<ProductionResultSummaryViewModel> GetProductionResultSummaryAsync(ProductionSummaryQuery query)
        {
            string procedureName = "ReportProductionBy" + query.QueryType;
            string reportGroupName = null;
            string accountNameColumn = "Client";
            string columnNamePrefix = query.QueryType;

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

                case "source":
                    reportGroupName = "Broker";
                    columnNamePrefix = "Broker";
                    break;

                case "campaign":
                    reportGroupName = "Member Name";
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

                var idParam = new SqlParameter("@" + query.QueryType + "Id", accountId);

                if (query.QueryType == "source")
                {
                    idParam = new SqlParameter("@" + query.QueryType, null);
                }

                var parameters = new[]
                {
                    idParam,
                    new SqlParameter("@PaidStatus", query.PaymentStatus),
                    new SqlParameter("@BookingStartDate", query.BookingStartDate),
                    new SqlParameter("@BookingEndDate", query.BookingEndDate),
                    new SqlParameter("@CheckInDate", query.CheckOutStartDate),
                    new SqlParameter("@CheckOutDate", query.CheckOutEndDate),
                    new SqlParameter("@StartRowIndex", Convert.ToInt32(0)),
                    new SqlParameter("@NumberOfRows", 30000),
                    totalCount
                };

                var table = await _dataAccess.ExecuteDataTableAsync(procedureName, parameters);

                if (table.Rows.Count == 0)
                {
                    continue;
                }

                var result = new ProductionSummaryTableViewModel
                {
                    ReportGroupName = reportGroupName,
                    AccountType = query.QueryType,
                    Items = new List<ProductionSummaryItemViewModel>()
                };

                model.Tables.Add(result);

                foreach (DataRow row in table.Rows)
                {
                    var accountName = String.Empty;

                    switch (query.QueryType)
                    {
                        case "source":
                            accountName = (string)row[columnNamePrefix + "FirstName"] + " " + (string)row[columnNamePrefix + "LastName"];
                            result.AccountName = (string)row["Source"];
                            break;

                        case "client":
                            accountName = ReadColumn.GetString(row, "CampaignName");
                            result.AccountName = ReadColumn.GetString(row, "Client");
                            break;

                        case "broker":
                            accountName = (string)row["CompanyName"];
                            result.AccountName = row["BrokerFirstName"] + " " + row["BrokerLastName"];
                            break;

                        case "campaign":
                            accountName = row["MemberFirstName"] + " " + row["MemberLastName"];
                            result.AccountName = (string)row["CampaignName"];
                            break;

                        default:
                            accountName = ReadColumn.GetString(row, accountNameColumn);
                            result.AccountName = (string)row[columnNamePrefix + "FirstName"] + " " + (string)row[columnNamePrefix + "LastName"];
                            break;
                    }

                    var item = new ProductionSummaryItemViewModel()
                    {
                        AccountName = accountName,
                        InternetPrice = ReadColumn.GetDecimal(row, "InternetPrice"),
                        YouPayPrice = ReadColumn.GetDecimal(row, "YouPayPrice"),
                        CommissionEarned = ReadColumn.GetDecimal(row, "CommissionEarned"),
                    };

                    result.TotalInternetPrice += item.InternetPrice;
                    result.TotalYouPayPrice += item.YouPayPrice;
                    result.TotalCommissionEarned += item.CommissionEarned;

                    model.TotalInternetPrice += item.InternetPrice;
                    model.TotalYouPayPrice += item.YouPayPrice;
                    model.TotalCommissionEarned += item.CommissionEarned;

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
        public int? CampaignId { get; set; }
    }

    public class ProductionSummaryQuery
    {
        public string QueryType { get; set; }
        public string PaymentStatus { get; set; }
        public DateTime BookingStartDate { get; set; }
        public DateTime BookingEndDate { get; set; }
        public DateTime CheckOutStartDate { get; set; }
        public DateTime CheckOutEndDate { get; set; }
        public IEnumerable<int> AccountIds { get; set; }
    }
}
