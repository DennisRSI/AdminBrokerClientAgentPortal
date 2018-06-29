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
    public class ReportCommissionService : IReportCommissionService
    {
        private readonly CodesDbContext _context;
        private readonly DataAccess _dataAccess;

        public ReportCommissionService(CodesDbContext context, IConfiguration configuration)
        {
            _context = context;

            var connectionString = configuration.GetConnectionString("CodeGeneratorConnection");
            _dataAccess = new DataAccess(connectionString);
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

                var parameters = new[]
                {
                    new SqlParameter("@BrokerId", accountId),
                    new SqlParameter("@CheckInDate", query.CheckOutStartDate),
                    new SqlParameter("@CheckOutDate", query.CheckOutEndDate),
                    new SqlParameter("@StartRowIndex", Convert.ToInt32(0)),
                    new SqlParameter("@NumberOfRows", 30000),
                    totalCount
                };

                var table = await _dataAccess.ExecuteDataTableAsync("ReportCommissionByBroker", parameters);

                if (table.Rows.Count == 0)
                {
                    continue;
                }

                var result = new CommissionResultTableViewModel
                {
                    ReportGroupName = "Client",
                    AccountType = "Broker",
                    Items = new List<CommissionResultItemViewModel>()
                };

                model.Tables.Add(result);

                foreach (DataRow row in table.Rows)
                {
                    result.AccountName = row["BrokerFirstName"] + " " + row["BrokerLastName"];

                    var item = new CommissionResultItemViewModel
                    {
                        AccountName = (string)row["FullName"],
                        NumberCards = (int)row["NumberOfCards"],
                        NumberTransaction = (int)row["Transactions"],
                        InternetPrice = (decimal)row["InternetPrice"],
                        YouPayPrice = (decimal)row["YouPayPrice"],
                        MemberSavings = (double)row["MemberSavings"],
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
                new SqlParameter("@PaidDate", null),
                new SqlParameter("@StartRowIndex", Convert.ToInt32(0)),
                new SqlParameter("@NumberOfRows", 30000),
                totalCount
            };

            var table = await _dataAccess.ExecuteDataTableAsync("ReportCommissionBySource", parameters);

            var result = new CommissionResultTableViewModel
            {
                ReportGroupName = "Client",
                AccountType = "Broker",
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
                    MemberSavings = (double)row["MemberSavings"],
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
            return null;
        }

        private string GetAbbreviatedName(string firstName, string lastName)
        {
            var initial = firstName.Substring(0, 1).ToUpper();
            return $"{initial}. {lastName}";
        }
    }

    public class CommissionQuery
    {
        public DateTime CheckOutStartDate { get; set; }
        public DateTime CheckOutEndDate { get; set; }
        public string QueryType { get; set; }
        public IEnumerable<int> AccountIds { get; set; }
    }
}
