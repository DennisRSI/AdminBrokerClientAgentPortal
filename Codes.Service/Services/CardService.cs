using Codes.Service.Data;
using Codes.Service.Interfaces;
using Codes.Service.Models;
using Codes.Service.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Codes.Service.Services
{
    public class CardService : ICardService
    {
        private readonly ILogger _logger;
        private readonly CodesDbContext _context;

        public CardService(CodesDbContext context, ILoggerFactory loggerFactory)
        {
            _context = context;
            _logger = loggerFactory.CreateLogger<CodeService>();
        }

        public CardDetailsViewModel GetDetails(int id)
        {
            // TODO: How to determine which table to query?

            //var card = _context.UsedCodes.Single(c => c.UsedCodeId == id);

            var card = new UsedCodeModel();

            var model = new CardDetailsViewModel
            {
                CardNumber = "abc123", // card.Code,
                BenefitCondo = false,
                BenefitHotel = true,
                BenefitShopping = false,
                TotalHotelSavings = 0,
                TotalCondoSavings = 0,
                TotalShoppingSavings = 0,
                ClientName = "ClientName", // card.Campaign.Client.CompanyName,
                ClientCampaign = "CampaignName", //card.Campaign.CampaignName,
                ActivationDate = card.CreationDate,
                BrokerCommissionRate = 0, // (decimal) card.Broker.BrokerCommissionPercentage,
                ClientCommissionRate = 0, // (decimal) card.Campaign.Client.CommissionRate,
                AgentCommissionRate = 0, // (decimal)card.Campaign.Ag.CommissionRate,
                ActivatedBy = "Activated By",
                TotalCardValue = 200,
                TotalCardRedeemed = 150,
                CardType = "Physical", // card.CodeType,

                TotalCommissionPaid = 0,
                TotalCommissionOwed = 0,

                BenefitsHotelUsage = 75,
                BenefitsCondoUsage = 25,
                BenefitsShoppingUsage = 50,
            };

            var benefitDetail1 = new CardBenefitDetailViewModel
            {
                CheckInDate = new DateTime(2018, 2, 20),
                CheckOutDate = new DateTime(2018, 2, 23),
                MemberSavings = 20.19M,
                CommissionTotal = 5.50M,
                CommisionBroker = 5.50M,
                CommisionClient = 5.50M,
                CommisionAgent = 5.50M,
                RemainingBalance = 5.50M,
                PaymentDate = new DateTime(2018, 2, 23),
                Confirmation = "T123456",
                Status = "Pending",
                Chargeback = "Chargeback",
            };

            var benefitDetail2 = new CardBenefitDetailViewModel
            {
                CheckInDate = new DateTime(2017, 2, 20),
                CheckOutDate = new DateTime(2017, 2, 23),
                MemberSavings = 120.19M,
                CommissionTotal = 75.50M,
                CommisionBroker = 5.50M,
                CommisionClient = 5.50M,
                CommisionAgent = 5.50M,
                RemainingBalance = 5.50M,
                PaymentDate = new DateTime(2018, 2, 23),
                Confirmation = "R123456",
                Status = "Pending",
                Chargeback = "Chargeback",
            };

            var benefitDetail3 = new CardBenefitDetailViewModel
            {
                CheckInDate = new DateTime(2016, 2, 20),
                CheckOutDate = new DateTime(2016, 2, 23),
                MemberSavings = 220.19M,
                CommissionTotal = 15.50M,
                CommisionBroker = 5.50M,
                CommisionClient = 5.50M,
                CommisionAgent = 5.50M,
                RemainingBalance = 5.50M,
                PaymentDate = new DateTime(2018, 2, 23),
                Confirmation = "S123456",
                Status = "Pending",
                Chargeback = "Chargeback",
            };

            model.BenefitDetails = new List<CardBenefitDetailViewModel>
            {
                benefitDetail1,
                benefitDetail2,
                benefitDetail3
            };

            return model;
        }
    }
}
