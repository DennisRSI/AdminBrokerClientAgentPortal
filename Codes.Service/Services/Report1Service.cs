using ClientPortal.Models;
using Codes1.Service.Data;
using Codes1.Service.Domain;
using Codes1.Service.Interfaces;
using Codes1.Service.ViewModels;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Codes1.Service.Services
{
    public class Report1Service : IReport1Service
    {
        private readonly Codes1DbContext _context;
        private readonly DataAccess _dataAccess;

        public Report1Service(Codes1DbContext context, IConfiguration configuration)
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
            const string procedureName = "V2_ReportProductionDetails";
            ProductionResultDetailViewModel model = new ProductionResultDetailViewModel();
            try
            {

                var totalCount = new SqlParameter()
                {
                    ParameterName = "@TotalCount",
                    Value = 0,
                    Direction = ParameterDirection.Output
                };

                SqlParameter totalCommission = new SqlParameter()
                {
                    ParameterName = "@TotalCommissionEarned",
                    Value = 0.00,
                    Direction = ParameterDirection.Output
                };

                var parameters = new[]
                {
                new SqlParameter("@BookingStartDate", null),//query.BookingStartDate),
                new SqlParameter("@BookingEndDate", null),//, query.BookingEndDate),
                new SqlParameter("@CheckOutStartDate", query.CheckOutStartDate),//, ),
                new SqlParameter("@CheckOutEndDate", query.CheckOutEndDate),//, ),
                new SqlParameter("@StartRowIndex", Convert.ToInt32(0)),
                new SqlParameter("@NumberOfRows", 90000000),
                new SqlParameter("@SortColumn", "DEFAULT"),
                new SqlParameter("@SortDirection", "ASC"),
                new SqlParameter("@BrokerId", query.BrokerId),
                new SqlParameter("@AgentId", query.AgentId),
                new SqlParameter("@ClientId", query.ClientId),
                new SqlParameter("@CampaignId", query.CampaignId),
                new SqlParameter("@Search", String.Empty),
                totalCount,
                totalCommission
            };

                var table = await _dataAccess.ExecuteDataTableAsync(procedureName, parameters);

                var results = new List<ProductionDetailItemViewModel>();

                model = new ProductionResultDetailViewModel
                {
                    CheckoutStartDate = query.CheckOutStartDate,
                    CheckoutEndDate = query.CheckOutEndDate,
                    BookingStartDate = query.BookingStartDate,
                    BookingEndDate = query.BookingEndDate,
                    DetailsTable = results
                };

                model.TotalCommission = decimal.Parse(totalCommission.Value.ToString());

                foreach (DataRow row in table.Rows)
                {
                    if (row["PointsBalance"] != null)
                    {
                        var item = new ProductionDetailItemViewModel()
                        {
                            ConfirmationNumber = (string)row["Confirmation"],
                            CardNumber = (string)row["CardNumber"],
                            MemberFullName = GetAbbreviatedName((string)row["MemberFirstName"], (string)row["MemberLastName"]),
                            GuestFullName = String.Empty,
                            BookingDate = (DateTime)row["BookingDate"],
                            CheckInDate = (DateTime)row["CheckInDate"],
                            CheckOutDate = (DateTime)row["CheckOutDate"],
                            Canceled = (string)row["Cancelled"],
                            InternetPrice = Convert.ToDecimal(row["InternetPrice"]),
                            YouPayPrice = Convert.ToDecimal(row["YouPayPrice"]),
                            PointsBalance = Convert.ToSingle(row["PointsBalance"]),
                            Commission = Convert.ToDecimal(row["ClubCommissionDue"])
                        };

                        model.TotalNights += (item.CheckOutDate - item.CheckInDate).Days;
                        model.TotalInternetPrice += item.InternetPrice;
                        model.TotalYouPayPrice += item.YouPayPrice;
                        model.TotalPointsBalance += item.PointsBalance;
                        //model.TotalCommission += item.Commission;
                       

                        results.Add(item);
                    }
                }

            }
            catch (Exception ex)
            {
                model.Message = ex.Message;
                model.IsSuccess = false;
            }

            return model;
        }

        public async Task<ProductionResultSummaryViewModel> GetProductionResultSummaryAsync(ProductionSummaryQuery query)
        {
            string procedureName = "V2_ReportProductionBy" + query.QueryType;
            string reportGroupName = null;
            string accountNameColumn = "Client";
            string columnNamePrefix = query.QueryType;

            switch (query.QueryType.ToLower())
            {
                case "broker":
                    reportGroupName = "Client";
                    break;

                case "agent":
                    reportGroupName = "Agent";
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

            var totalCount = new SqlParameter()
            {
                ParameterName = "@TotalCount",
                Value = 0,
                Direction = ParameterDirection.Output
            };

            var totalCommission = new SqlParameter()
            {
                ParameterName = "@TotalCommission",
                Value = 0.00,
                DbType = DbType.Decimal,
                Direction = ParameterDirection.Output
            };

            SqlParameter[] parameters = null;

            DataTable table = null;
            ProductionSummaryTableViewModel result = null;
            ProductionResultSummaryViewModel model = new ProductionResultSummaryViewModel
            {
                CheckoutStartDate = query.CheckOutStartDate,
                CheckoutEndDate = query.CheckOutEndDate,
                BookingStartDate = query.BookingStartDate,
                BookingEndDate = query.BookingEndDate,
                Tables = new List<ProductionSummaryTableViewModel>()
            };

            var accountName = String.Empty;


            if (query.QueryType == "source")
            {
                
                parameters = null;
                foreach (var accountId in query.AccountIds)
                {

                    var idParam = new SqlParameter("@" + query.QueryType + "Id", accountId);

                    if (query.QueryType == "source")
                    {
                        idParam = new SqlParameter("@" + query.QueryType, null);



                        parameters = new[]
                        {
                        idParam,
                        new SqlParameter("@PaidStatus", query.PaymentStatus),
                        new SqlParameter("@BookingStartDate", null),//query.BookingStartDate),
                        new SqlParameter("@BookingEndDate", null),//query.BookingEndDate),
                        new SqlParameter("@CheckInDate", null),//query.CheckOutStartDate),
                        new SqlParameter("@CheckOutDate", null),//query.CheckOutEndDate),
                        new SqlParameter("@StartRowIndex", Convert.ToInt32(0)),
                        new SqlParameter("@NumberOfRows", 90000000),
                        totalCount,
                        //totalCommission
                    };


                    }
                    else
                    {
                        //var agentParm = new SqlParameter("@AgentId", query.AgentId);
                        //var brokerParm = new SqlParameter("@BrokerId", query.BrokerId);

                        parameters = new[]
                        {
                        idParam,
                        //agentParm,
                        //brokerParm,
                        new SqlParameter("@PaidStatus", query.PaymentStatus),
                        new SqlParameter("@BookingStartDate",  null),//query.BookingStartDate),
                        new SqlParameter("@BookingEndDate",  null),//query.BookingEndDate),
                        new SqlParameter("@CheckInDate",  null),//query.CheckOutStartDate),
                        new SqlParameter("@CheckOutDate",  null),//query.CheckOutEndDate),
                        new SqlParameter("@StartRowIndex", Convert.ToInt32(0)),
                        new SqlParameter("@NumberOfRows", 90000000),
                        totalCount
                    };
                    }



                    table = await _dataAccess.ExecuteDataTableAsync(procedureName, parameters);

                    if (table.Rows.Count == 0)
                    {
                        continue;
                    }

                     result = new ProductionSummaryTableViewModel
                    {
                        ReportGroupName = reportGroupName,
                        AccountType = query.QueryType,
                        Items = new List<ProductionSummaryItemViewModel>()
                    };

                    model.Tables.Add(result);

                    foreach (DataRow row in table.Rows)
                    {
                        accountName = String.Empty;

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
            }
            else
            {
                var agentParm = new SqlParameter("@AgentId", query.AgentId);
                var brokerParm = new SqlParameter("@BrokerId", query.BrokerId); 
                var clientParm = new SqlParameter("@ClientId", query.ClientId);

                if (procedureName.ToLower() == "v2_reportproductionbybroker")
                {
                    parameters = new[]
                    {
                        brokerParm,
                        new SqlParameter("@PaidStatus", query.PaymentStatus),
                        new SqlParameter("@BookingStartDate", query.BookingStartDate),
                        new SqlParameter("@BookingEndDate", query.BookingEndDate),
                        //new SqlParameter("@CheckInDate", query.CheckOutStartDate),
                        new SqlParameter("@CheckOutDate", null), //query.CheckOutEndDate),
                        new SqlParameter("@StartRowIndex", Convert.ToInt32(0)),
                        new SqlParameter("@NumberOfRows", 90000000),
                        totalCount
                    };
                }
                else
                {
                    parameters = new[]
                    {
                        clientParm,
                        agentParm,
                        brokerParm,
                        new SqlParameter("@PaidStatus", query.PaymentStatus),
                        new SqlParameter("@BookingStartDate", query.BookingStartDate),
                        new SqlParameter("@BookingEndDate", query.BookingEndDate),
                        //new SqlParameter("@CheckInDate", query.CheckOutStartDate),
                        new SqlParameter("@CheckOutDate",null), //query.CheckOutEndDate),
                        new SqlParameter("@StartRowIndex", Convert.ToInt32(0)),
                        new SqlParameter("@NumberOfRows", 90000000),
                        totalCount,
                        totalCommission
                };
                }

                table = await _dataAccess.ExecuteDataTableAsync(procedureName, parameters);

                if (table.Rows.Count > 0)
                {
                    model.TotalCommissionEarned = decimal.Parse(totalCommission.Value.ToString());
                    string currentClientId = "";

                    foreach (DataRow row in table.Rows)
                    {
                        if (currentClientId != row["ClientId"].ToString())
                        {

                            currentClientId = row["ClientId"].ToString();
                            accountName = String.Empty;

                            result = new ProductionSummaryTableViewModel
                            {
                                ReportGroupName = reportGroupName,
                                AccountType = query.QueryType,
                                Items = new List<ProductionSummaryItemViewModel>()
                            };

                            model.Tables.Add(result);

                            

                        }

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
                        //model.TotalCommissionEarned += item.CommissionEarned;

                        result.Items.Add(item);
                    }
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
        public int? BrokerId { get; set; } = null;
        public int? AgentId { get; set; } = null;
        public int? ClientId { get; set; } = null;
        public string Role { get; set; }
    }
}
