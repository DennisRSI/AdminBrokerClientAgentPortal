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
    public class ReportCommission1Service : IReportCommission1Service
    {
        private readonly Codes1DbContext _context;
        private readonly DataAccess _dataAccess;

        public ReportCommission1Service(Codes1DbContext context, IConfiguration configuration)
        {
            _context = context;

            var connectionString = configuration.GetConnectionString("CodeGeneratorConnection");
            _dataAccess = new DataAccess(connectionString);
        }

        public async Task<CommissionResultViewModel> GetCommissionResultsBrokerNewAsync(DateTime checkoutStartDate, DateTime checkoutEndDate, 
            int ? brokerId = null, int? agentId = null, int? clientId = null, string paymentStatus = null)
        {

            if (brokerId == 0)
                brokerId = null;

            if (agentId == 0)
                agentId = null;

            if (clientId == 0)
                clientId = null;

            CommissionResultViewModel model = new CommissionResultViewModel
            {
                QueryType = "broker",
                CheckoutStartDate = checkoutStartDate,
                CheckoutEndDate = checkoutEndDate,
                Tables = new List<CommissionResultTableViewModel>()
            };

            try
            {
                var totalCount = new SqlParameter()
                {
                    ParameterName = "@TotalCount",
                    Value = 0,
                    Direction = ParameterDirection.Output
                };

                var totalInternet = new SqlParameter()
                {
                    ParameterName = "@TotalInternetPrice",
                    SqlDbType = SqlDbType.Money,
                    Direction = ParameterDirection.Output
                };

                var totalYouPay = new SqlParameter()
                {
                    ParameterName = "@TotalYouPayPrice",
                    SqlDbType = SqlDbType.Money,
                    Direction = ParameterDirection.Output
                };

                var totalMemberSavings = new SqlParameter()
                {
                    ParameterName = "@TotalMemberSavings",
                    SqlDbType = SqlDbType.Money,
                    Direction = ParameterDirection.Output
                };

                var totalCommission = new SqlParameter()
                {
                    ParameterName = "@TotalCommission",
                    SqlDbType = SqlDbType.Money,
                    Direction = ParameterDirection.Output
                };

                var parameters = new[]
                {
                    new SqlParameter("@BrokerId", brokerId),
                    new SqlParameter("@ClientId", clientId),
                    new SqlParameter("@AgentId", agentId),
                    new SqlParameter("@CheckInDate", checkoutStartDate),
                    new SqlParameter("@CheckOutDate", checkoutEndDate),
                    new SqlParameter("@PaidStatus", paymentStatus),
                    new SqlParameter("@StartRowIndex", Convert.ToInt32(0)),
                    new SqlParameter("@NumberOfRows", 90000000),
                    totalCount,
                    totalInternet,
                    totalYouPay,
                    totalMemberSavings,
                    totalCommission
                };

                var table = await _dataAccess.ExecuteDataTableAsync("V2_ReportCommissionByBroker", parameters);

                /*var tbl = await (
                    from bc in _context.BookingCommissions
                    select bc
                    ).ToListAsync();*/

                if(table.Rows.Count > 0)
                {
                    var result = new CommissionResultTableViewModel
                    {
                        ReportGroupName = "Client",
                        AccountType = "Broker",
                        Items = new List<CommissionResultItemViewModel>(),
                        TotalInternetPrice = (decimal)totalInternet.Value,
                        TotalYouPayPrice = (decimal)totalYouPay.Value,
                        TotalMemberSavings = (decimal)totalMemberSavings.Value,
                        TotalCommissionEarned = (decimal)totalCommission.Value
                    };

                    model.Tables.Add(result);

                    CommissionResultItemViewModel company = null;

                    foreach (DataRow row in table.Rows)
                    {
                        var commissionType = (string)row["CommissionType"];

                        if (commissionType == "Broker")
                        {
                            result.AccountName = row["FirstName"] + " " + row["LastName"];
                        }

                        var item = new CommissionResultItemViewModel
                        {
                            AccountName = (string)row["CompanyName"],
                            FirstName = (string)row["FirstName"],
                            LastName = (string)row["LastName"],
                            CommissionType = (string)row["CommissionType"],
                            NumberCards = (int)row["NumberOfCards"],
                            NumberTransaction = (int)row["NumberOfTransactions"],
                            InternetPrice = (decimal)row["InternetPrice"],
                            YouPayPrice = (decimal)row["YouPayPrice"],
                            MemberSavings = (decimal)row["MemberSavings"],
                            CommissionEarned = (decimal)row["CommissionEarned"],
                        };

                        if (commissionType == "Broker Bucket")
                        {
                            company = item;
                            company.CommissionEarned = company.CommissionEarned;

                            result.Items.Add(item);

                            result.TotalCards += item.NumberCards;
                            result.TotalTransactions += item.NumberTransaction;
                            //result.TotalInternetPrice += item.InternetPrice;
                            //result.TotalYouPayPrice += item.YouPayPrice;
                            ///result.TotalMemberSavings += item.MemberSavings;
                           // result.TotalCommissionEarned += item.CommissionEarned;

                            model.TotalCards += item.NumberCards;
                            model.TotalTransactions += item.NumberTransaction;
                            //model.TotalInternetPrice += item.InternetPrice;
                            //model.TotalYouPayPrice += item.YouPayPrice;
                            //model.TotalMemberSavings += item.MemberSavings;
                            //model.TotalCommissionEarned += item.CommissionEarned;
                        }
                        else
                        {
                            var name = $"({item.FirstName} {item.LastName})";

                            if (item.CommissionType == "Client" || item.CommissionType == "Agent Bucket")
                            {
                                name = String.Empty;
                            }

                            var child = new CommissionResultChildViewModel
                            {
                                CommissionEarned = item.CommissionEarned,
                                Name = $"{item.CommissionType} {name}"
                            };

                            company.Children.Add(child);
                        }
                    }
                }

                model.Message = "Success";
                model.IsSuccess = true;
            }
            catch (Exception ex)
            {
                if (model == null)
                    model = new CommissionResultViewModel();

                model.Message = ex.Message;
                model.IsSuccess = false;
            }

            return model;
        }

        public async Task<CommissionResultViewModel> GetCommissionResultBrokerAsync(CommissionQuery query)
        {
            var model = new CommissionResultViewModel
            {
                QueryType = query.QueryType,
                CheckoutStartDate = query.CheckOutStartDate,
                CheckoutEndDate = query.CheckOutEndDate,
                Tables = new List<CommissionResultTableViewModel>()
            };

            foreach (var accountId in query.AccountIds)
            {
                var totalCount = new SqlParameter()
                {
                    ParameterName = "@TotalCount",
                    Value = 0,
                    Direction = ParameterDirection.Output
                };

                var totalInternet = new SqlParameter()
                {
                    ParameterName = "@TotalInternetPrice",
                    SqlDbType = SqlDbType.Money,
                    Direction = ParameterDirection.Output
                };

                var totalYouPay = new SqlParameter()
                {
                    ParameterName = "@TotalYouPayPrice",
                    SqlDbType = SqlDbType.Money,
                    Direction = ParameterDirection.Output
                };

                var totalMemberSavings = new SqlParameter()
                {
                    ParameterName = "@TotalMemberSavings",
                    SqlDbType = SqlDbType.Money,
                    Direction = ParameterDirection.Output
                };

                var totalCommission = new SqlParameter()
                {
                    ParameterName = "@TotalCommission",
                    SqlDbType = SqlDbType.Money,
                    Direction = ParameterDirection.Output
                };

                var parameters = new[]
                {
                    new SqlParameter("@BrokerId", accountId),
                    new SqlParameter("@CheckInDate", query.CheckOutStartDate),
                    new SqlParameter("@CheckOutDate", query.CheckOutEndDate),
                    new SqlParameter("@PaidStatus", query.PaymentStatus),
                    new SqlParameter("@StartRowIndex", Convert.ToInt32(0)),
                    new SqlParameter("@NumberOfRows", 9000000),
                    totalCount,
                    totalInternet,
                    totalYouPay,
                    totalMemberSavings
                };

                var table = await _dataAccess.ExecuteDataTableAsync("V2_ReportCommissionByBroker", parameters);

                if (table.Rows.Count == 0)
                {
                    continue;
                }

                var result = new CommissionResultTableViewModel
                {
                    ReportGroupName = "Client",
                    AccountType = "Broker",
                    Items = new List<CommissionResultItemViewModel>(),
                    TotalInternetPrice = (decimal)totalInternet.Value,
                    TotalYouPayPrice = (decimal)totalYouPay.Value,
                    TotalMemberSavings = (decimal)totalMemberSavings.Value,
                    TotalCommissionEarned = (decimal)totalCommission.Value
                };

                model.Tables.Add(result);

                CommissionResultItemViewModel company = null;

                foreach (DataRow row in table.Rows)
                {
                    var commissionType = (string)row["CommissionType"];

                    if (commissionType == "Broker")
                    {
                        result.AccountName = row["FirstName"] + " " + row["LastName"];
                    }

                    var item = new CommissionResultItemViewModel
                    {
                        AccountName = (string)row["CompanyName"],
                        FirstName = (string)row["FirstName"],
                        LastName = (string)row["LastName"],
                        CommissionType = (string)row["CommissionType"],
                        NumberCards = (int)row["NumberOfCards"],
                        NumberTransaction = (int)row["NumberOfTransactions"],
                        InternetPrice = (decimal)row["InternetPrice"],
                        YouPayPrice = (decimal)row["YouPayPrice"],
                        MemberSavings = (decimal)row["MemberSavings"],
                        CommissionEarned = (decimal)row["CommissionEarned"],
                    };

                    if (commissionType == "Company")
                    {
                        company = item;
                        company.CommissionEarned = company.CommissionEarned;

                        result.Items.Add(item);

                        result.TotalCards += item.NumberCards;
                        result.TotalTransactions += item.NumberTransaction;
                        //result.TotalInternetPrice += item.InternetPrice;
                        //result.TotalYouPayPrice += item.YouPayPrice;
                        //result.TotalMemberSavings += item.MemberSavings;
                        //result.TotalCommissionEarned += item.CommissionEarned;

                        model.TotalCards += item.NumberCards;
                        model.TotalTransactions += item.NumberTransaction;
                        //model.TotalInternetPrice += item.InternetPrice;
                        //model.TotalYouPayPrice += item.YouPayPrice;
                        //model.TotalMemberSavings += item.MemberSavings;
                        //model.TotalCommissionEarned += item.CommissionEarned;
                    }
                    else
                    {
                        var name = $"({item.FirstName} {item.LastName})";

                        if (item.CommissionType == "Client")
                        {
                            name = String.Empty;
                        }

                        var child = new CommissionResultChildViewModel
                        {
                            CommissionEarned = item.CommissionEarned,
                            Name = $"{item.CommissionType} {name}"
                        };

                        company.Children.Add(child);
                    }
                }
            }

            return model;
        }

        public async Task<CommissionResultViewModel> GetCommissionResultSourceAsync(CommissionQuery query)
        {
            var model = new CommissionResultViewModel
            {
                QueryType = query.QueryType,
                CheckoutStartDate = query.CheckOutStartDate,
                CheckoutEndDate = query.CheckOutEndDate,
                Tables = new List<CommissionResultTableViewModel>()
            };

            var totalCount = new SqlParameter()
            {
                ParameterName = "@TotalCount",
                Value = 0,
                Direction = ParameterDirection.Output
            };

            var parameters = new[]
            {
                new SqlParameter("@Source", null),
                new SqlParameter("@BrokerId", null),
                new SqlParameter("@CheckInDate", query.CheckOutStartDate),
                new SqlParameter("@CheckOutDate", query.CheckOutEndDate),
                new SqlParameter("@PaidStatus", query.PaymentStatus),
                new SqlParameter("@StartRowIndex", Convert.ToInt32(0)),
                new SqlParameter("@NumberOfRows", 90000000),
                totalCount
            };

            var table = await _dataAccess.ExecuteDataTableAsync("V2_ReportCommissionBySource", parameters);

            var result = new CommissionResultTableViewModel
            {
                ReportGroupName = "Broker",
                AccountType = "Source",
                Items = new List<CommissionResultItemViewModel>()
            };

            model.Tables.Add(result);

            foreach (DataRow row in table.Rows)
            {
                result.AccountName = (string)row["Source"];

                var item = new CommissionResultItemViewModel
                {
                    AccountName = row["BrokerFirstName"] + " " + row["BrokerLastName"],
                    // Need to add Client here row["FullName"]
                    NumberCards = (int)row["NumberOfCards"],
                    NumberTransaction = (int)row["Transactions"],
                    InternetPrice = (decimal)row["InternetPrice"],
                    YouPayPrice = (decimal)row["YouPayPrice"],
                    MemberSavings = (decimal)row["MemberSavings"],
                    CommissionEarned = (decimal)row["CommissionEarned"]
                };

                result.Items.Add(item);

                result.TotalCards += item.NumberCards;
                result.TotalTransactions += item.NumberTransaction;
                result.TotalInternetPrice += item.InternetPrice;
                result.TotalYouPayPrice += item.YouPayPrice;
                result.TotalMemberSavings += item.MemberSavings;
                result.TotalCommissionEarned += item.CommissionEarned;
            }

            return model;
        }

        public async Task<CommissionResultViewModel> GetCommissionResultClientAsync(CommissionQuery query)
        {
            var model = new CommissionResultViewModel
            {
                QueryType = query.QueryType,
                CheckoutStartDate = query.CheckOutStartDate,
                CheckoutEndDate = query.CheckOutEndDate,
                Tables = new List<CommissionResultTableViewModel>()
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
                    new SqlParameter("@ClientId", accountId),
                    new SqlParameter("@CheckInDate", query.CheckOutStartDate),
                    new SqlParameter("@CheckOutDate", query.CheckOutEndDate),
                    new SqlParameter("@PaidStatus", query.PaymentStatus),
                    new SqlParameter("@StartRowIndex", Convert.ToInt32(0)),
                    new SqlParameter("@NumberOfRows", 90000000),
                    totalCount
                };

                var table = await _dataAccess.ExecuteDataTableAsync("V2_ReportCommissionByClient", parameters);

                if (table.Rows.Count == 0)
                {
                    continue;
                }

                var result = new CommissionResultTableViewModel
                {
                    ReportGroupName = "Campaign",
                    AccountType = "Client",
                    Items = new List<CommissionResultItemViewModel>()
                };

                model.Tables.Add(result);

                foreach (DataRow row in table.Rows)
                {
                    result.AccountName = (string)row["ClientCompanyName"];

                    var item = new CommissionResultItemViewModel
                    {
                        AccountName = (string)row["CampaignName"],
                        NumberCards = (int)row["NumberOfCards"],
                        NumberTransaction = (int)row["Transactions"],
                        InternetPrice = (decimal)row["InternetPrice"],
                        YouPayPrice = (decimal)row["YouPayPrice"],
                        MemberSavings = Convert.ToDecimal(row["MemberSavings"]),
                        CommissionEarned = (decimal)row["CommissionEarned"]
                    };

                    result.Items.Add(item);

                    result.TotalCards += item.NumberCards;
                    result.TotalTransactions += item.NumberTransaction;
                    result.TotalInternetPrice += item.InternetPrice;
                    result.TotalYouPayPrice += item.YouPayPrice;
                    result.TotalMemberSavings += item.MemberSavings;
                    result.TotalCommissionEarned += item.CommissionEarned;

                    model.TotalCards += item.NumberCards;
                    model.TotalTransactions += item.NumberTransaction;
                    model.TotalInternetPrice += item.InternetPrice;
                    model.TotalYouPayPrice += item.YouPayPrice;
                    model.TotalMemberSavings += item.MemberSavings;
                    model.TotalCommissionEarned += item.CommissionEarned;
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

    public class CommissionQuery
    {
        public string QueryType { get; set; }
        public string PaymentStatus { get; set; }
        public DateTime CheckOutStartDate { get; set; }
        public DateTime CheckOutEndDate { get; set; }
        public IEnumerable<int> AccountIds { get; set; }
    }
}
