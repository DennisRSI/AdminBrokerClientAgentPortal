using Codes.Service.Data;
using Codes.Service.Domain;
using Codes.Service.Interfaces;
using Codes.Service.Models;
using Codes.Service.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Codes.Service.Services
{
    public class CardService : ICardService
    {
        private readonly CodesDbContext _context;
        private readonly DataAccess _dataAccess;

        public CardService(CodesDbContext context, IConfiguration configuration)
        {
            _context = context;

            var connectionString = configuration.GetConnectionString("CodeGeneratorConnection");
            _dataAccess = new DataAccess(connectionString);
        }

        public async Task<CardDetailsViewModel> GetDetails(string code)
        {
            var parameters = new[]
            {
                new SqlParameter("@Code", code),
            };

            var table = await _dataAccess.ExecuteDataTableAsync("GetCardDetails", parameters);
            var row = table.Rows[0];

            var packageId = (int)row["PackageId"];
            var benefits = PackageCode.GetBenefits(packageId);

            var model = new CardDetailsViewModel
            {
                CardNumber = code,
                BenefitHotel = true,
                BenefitCondo = benefits.CondoBenefit,
                BenefitShopping = benefits.ShoppingBenefit,
                BenefitDining = benefits.DiningBenefit,
                ClientName = (string)row["ClientName"],
                ClientCompanyName = (string)row["ClientCompanyName"],
                ClientCampaign = (string)row["CampaignName"],
                ActivationDate = (DateTime)row["ActivationDate"],
                BrokerCommission = Convert.ToDecimal(row["BrokerCommission"]),
                ClientCommission = Convert.ToDecimal(row["ClientCommission"]),
                AgentCommission = Convert.ToDecimal(row["AgentCommission"]),
                ActivatedBy = (string)row["ActivatedBy"],
                FaceValue = Convert.ToDecimal(row["FaceValue"]),
                CardType = (string)row["CardType"],
                BenefitDetails = GetCardUsage(code)
            };

            var remainingBalance = model.FaceValue;

            foreach (var benefit in model.BenefitDetails)
            {
                switch (benefit.UsageType.ToLower())
                {
                    case "hotel":
                        model.TotalHotelSavings += benefit.MemberSavings;
                        break;

                    case "condo":
                        model.TotalCondoSavings += benefit.MemberSavings;
                        break;

                    case "shopping":
                        model.TotalShoppingSavings += benefit.MemberSavings;
                        break;

                    case "dining":
                        model.TotalDiningSavings += benefit.MemberSavings;
                        break;
                }

                if (benefit.RemainingBalance < remainingBalance)
                {
                    remainingBalance = benefit.RemainingBalance;
                }
            }

            model.TotalCardAvailable = remainingBalance;

            return model;
        }

        private List<CardBenefitDetailViewModel> GetCardUsage(string code)
        {
            var usageList = new List<CardBenefitDetailViewModel>();

            var parameters = new[]
            {
                new SqlParameter("@Code", code),
            };

            var table = _dataAccess.ExecuteDataTable("GetCardUsage", parameters);

            foreach (DataRow row in table.Rows)
            {
                var item = new CardBenefitDetailViewModel
                {
                    CheckInDate = (DateTime)row["CheckInDate"],
                    CheckOutDate = (DateTime)row["CheckOutDate"],
                    MemberSavings = Convert.ToDecimal(row["MemberSavings"]),
                    CommissionTotal = Convert.ToDecimal(row["CommissionTotal"]),
                    CommissionBroker = Convert.ToDecimal(row["CommissionBroker"]),
                    CommissionClient = Convert.ToDecimal(row["CommissionClient"]),
                    CommissionAgent = Convert.ToDecimal(row["CommissionAgent"]),
                    RemainingBalance = Convert.ToDecimal(row["RemainingBalance"]),
                    PaymentDate = Convert.IsDBNull(row["PaymentDate"]) ? null : (DateTime?)row["PaymentDate"],
                    Confirmation = (string)row["Confirmation"],
                    Status = (string)row["Status"],
                    UsageType = (string)row["UsageType"]
                };

                usageList.Add(item);
            }

            return usageList;
        }
    }
}
