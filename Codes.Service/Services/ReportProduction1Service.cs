using ClientPortal.Models;
using Codes1.Service.Data;
using Codes1.Service.Domain;
using Codes1.Service.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Codes1.Service.Services
{
    public class ReportProduction1Service : IReportProduction1Service
    {
        private readonly Codes1DbContext _context;
        private readonly DataAccess _dataAccess;
        

        public ReportProduction1Service(Codes1DbContext context, IConfiguration configuration)
        {
            _context = context;

            var connectionString = configuration.GetConnectionString("CodeGeneratorConnection");
            _dataAccess = new DataAccess(connectionString);
        }

        public async Task<ProductionResultSummaryViewModel> V2_GetProductionResultCampaignAsync(int startRowIndex = 0, int numberOfRows = 9000000, 
            int? campaignId = null, int? brokerId = null, int?  clientId = null, int? agentId = null,
            DateTime? reservationStartDate = null, DateTime? reservationEndDate = null, DateTime? checkInDate = null, DateTime? checkOutDate = null, bool? showOnlyPaid = null)
        {
            ProductionResultSummaryViewModel model = new ProductionResultSummaryViewModel();

            try
            {
                string paidStatus = null;

                if (showOnlyPaid != null)
                {
                    paidStatus = showOnlyPaid.GetValueOrDefault() ? "Paid" : "Unpaid";
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
                    Direction = ParameterDirection.Output
                };

                var parameters = new[]
                {
                    new SqlParameter("@CampaignId", campaignId),
                    new SqlParameter("@AgentId", agentId),
                    new SqlParameter("@ClientId", clientId),
                    new SqlParameter("@BrokerId", brokerId),
                    new SqlParameter("@BookingStartDate", null),//reservationStartDate),
                    new SqlParameter("@BookingEndDate", null), //reservationEndDate),
                    new SqlParameter("@CheckInDate", null), //checkInDate),
                    new SqlParameter("@CheckOutDate", null), //checkOutDate),
                    new SqlParameter("@PaidStatus", paidStatus),
                    new SqlParameter("@StartRowIndex", Convert.ToInt32(0)),
                    new SqlParameter("@NumberOfRows", 90000000),
                    totalCount,
                    totalCommission
                };

                DataTable table = await _dataAccess.ExecuteDataTableAsync("V2_ReportProductionByCampaign", parameters);

                if (table.Rows.Count > 0)
                {
                    int currentCampaignId = 0;
                    ProductionSummaryTableViewModel result = null;
                    ProductionSummaryItemViewModel item1 = null;

                    foreach (DataRow row in table.Rows)
                    {
                        if ((int)row["CampaignId"] != currentCampaignId)
                        {
                            currentCampaignId = (int)row["CampaignId"];

                            result = new ProductionSummaryTableViewModel
                            {
                                IsCampaignReport = true,
                                ReportGroupName = "Member Name",
                                AccountType = "ProductionReportByCampaign",
                                Items = new List<ProductionSummaryItemViewModel>()
                            };

                            model.Tables.Add(result);

                            result.AccountName = (string)row["CampaignName"];

                            
                        }

                        item1 = new ProductionSummaryItemViewModel()
                        {
                            AccountName = row["MemberFirstName"] + " " + row["MemberLastName"],
                            InternetPrice = Convert.ToDecimal(row["InternetPrice"]),
                            YouPayPrice = Convert.ToDecimal(row["YouPayPrice"]),
                            CommissionEarned = Convert.ToDecimal(row["CommissionEarned"]),
                            TransactionDate = (DateTime?)row["ReservationDate"]
                        };

                        result.TotalInternetPrice += item1.InternetPrice;
                        result.TotalYouPayPrice += item1.YouPayPrice;
                        result.TotalCommissionEarned = decimal.Parse(totalCommission.Value.ToString()); // item1.CommissionEarned;

                        model.TotalInternetPrice += item1.InternetPrice;
                        model.TotalYouPayPrice += item1.YouPayPrice;
                        //model.TotalCommissionEarned += item1.CommissionEarned;

                        result.Items.Add(item1);
                    }

                    model.TotalCommissionEarned = decimal.Parse(totalCommission.Value.ToString());
                }
            }
            catch (Exception ex)
            {
                model.Message = ex.Message;
                model.IsSuccess = false;
            }

            return model;
        }

        public async Task<ProductionResultSummaryViewModel> V2_getProductionResultClientAsync(int startRowIndex = 0, int numberOfRows = 9000000,
            int? campaignId = null, int? brokerId = null, int? clientId = null, int? agentId = null,
            DateTime? reservationStartDate = null, DateTime? reservationEndDate = null, DateTime? checkInDate = null, DateTime? checkOutDate = null, bool? showOnlyPaid = null)
        {
            ProductionResultSummaryViewModel model = new ProductionResultSummaryViewModel();

            try
            {
                string paidStatus = null;

                if (showOnlyPaid != null)
                {
                    paidStatus = showOnlyPaid.GetValueOrDefault() ? "Paid" : "Unpaid";
                }

                var totalCount = new SqlParameter()
                {
                    ParameterName = "@TotalCount",
                    Value = 0,
                    Direction = ParameterDirection.Output
                };

                var parameters = new[]
                {
                   
                    new SqlParameter("@AgentId", agentId),
                    new SqlParameter("@ClientId", clientId),
                    new SqlParameter("@BrokerId", brokerId),
                    new SqlParameter("@BookingStartDate", null),//reservationStartDate),
                    new SqlParameter("@BookingEndDate", null), //reservationEndDate),
                    new SqlParameter("@CheckInDate", checkInDate),
                    new SqlParameter("@CheckOutDate", checkOutDate),
                    new SqlParameter("@PaidStatus", paidStatus),
                    new SqlParameter("@StartRowIndex", Convert.ToInt32(0)),
                    new SqlParameter("@NumberOfRows", 90000000),
                    totalCount
                };

                DataTable table = await _dataAccess.ExecuteDataTableAsync("V2_ReportProductionByClient", parameters);

                if (table.Rows.Count > 0)
                {
                    int currentClientId = 0;
                    ProductionSummaryTableViewModel result = null;
                    ProductionSummaryItemViewModel item1 = null;

                    foreach (DataRow row in table.Rows)
                    {
                        if ((int)row["ClientId"] != currentClientId)
                        {
                            currentClientId = (int)row["ClientId"];

                            result = new ProductionSummaryTableViewModel
                            {
                                IsCampaignReport = true,
                                ReportGroupName = "Member Name",
                                AccountType = "ProductionReportByClient",
                                Items = new List<ProductionSummaryItemViewModel>()
                            };

                            model.Tables.Add(result);

                            result.AccountName = (string)row["Client"];


                        }

                        item1 = new ProductionSummaryItemViewModel()
                        {
                            AccountName = row["MemberFirstName"] + " " + row["MemberLastName"],
                            InternetPrice = Convert.ToDecimal(row["InternetPrice"]),
                            YouPayPrice = Convert.ToDecimal(row["YouPayPrice"]),
                            CommissionEarned = Convert.ToDecimal(row["CommissionEarned"]),
                            TransactionDate = (DateTime?)row["ReservationDate"]
                        };

                        result.TotalInternetPrice += item1.InternetPrice;
                        result.TotalYouPayPrice += item1.YouPayPrice;
                        result.TotalCommissionEarned += item1.CommissionEarned;

                        model.TotalInternetPrice += item1.InternetPrice;
                        model.TotalYouPayPrice += item1.YouPayPrice;
                        model.TotalCommissionEarned += item1.CommissionEarned;

                        result.Items.Add(item1);
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

        public async Task<ProductionResultSummaryViewModel> GetProductionResultCampaignAsync(ProductionSummaryQuery query)
        {
            int? brokerId = query.BrokerId > 0 ? query.BrokerId : null, clientId = query.ClientId > 0 ? query.ClientId : null;
            int? agentId = query.AgentId > 0 ? query.AgentId : null;

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
                    new SqlParameter("@NumberOfRows", 90000000),
                    new SqlParameter("@SortColumn", "DEFAULT"),
                    new SqlParameter("@SortDirection", "ASC"),
                    new SqlParameter("@BrokerId", brokerId),
                    new SqlParameter("@AgentId", agentId),
                    new SqlParameter("@ClientId", clientId),
                    new SqlParameter("@CampaignId", accountId),
                    new SqlParameter("@Search", String.Empty),
                    totalCount
                };

                var table = await _dataAccess.ExecuteDataTableAsync("V2_ReportProductionDetails", parameters);

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
                    result.AccountName = (string)row["CampaignName"];

                    var item = new ProductionSummaryItemViewModel()
                    {
                        AccountName = row["MemberFirstName"] + " " + row["MemberLastName"],
                        InternetPrice = Convert.ToDecimal(row["InternetPrice"]),
                        YouPayPrice = Convert.ToDecimal(row["YouPayPrice"]),
                        CommissionEarned = Convert.ToDecimal(row["ClubCommissionDue"]),
                        TransactionDate = (DateTime?)row["BookingDate"]
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
    }
}
