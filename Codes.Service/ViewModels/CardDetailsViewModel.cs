using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Codes.Service.ViewModels
{
    public class CardDetailsViewModel
    {
        public string CardNumber { get; set; }

        public bool BenefitHotel { get; set; }
        public bool BenefitCondo { get; set; }
        public bool BenefitShopping { get; set; }

        [DisplayFormat(DataFormatString = "{0:c}")]
        public decimal TotalHotelSavings { get; set; }

        [DisplayFormat(DataFormatString = "{0:c}")]
        public decimal TotalCondoSavings { get; set; }

        [DisplayFormat(DataFormatString = "{0:c}")]
        public decimal TotalShoppingSavings { get; set; }

        public string ClientName { get; set; }
        public string ClientCampaign { get; set; }

        [DisplayFormat(DataFormatString = "{0:MM-dd-yyyy}")]
        public DateTime ActivationDate { get; set; }

        [DisplayFormat(DataFormatString = "{0:p1}")]
        public decimal BrokerCommissionRate { get; set; }

        [DisplayFormat(DataFormatString = "{0:p1}")]
        public decimal ClientCommissionRate { get; set; }

        [DisplayFormat(DataFormatString = "{0:p1}")]
        public decimal AgentCommissionRate { get; set; }

        public string ActivatedBy { get; set; }

        [DisplayFormat(DataFormatString = "{0:c}")]
        public decimal TotalCardValue { get; set; }

        [DisplayFormat(DataFormatString = "{0:c}")]
        public decimal TotalCardRedeemed { get; set; }

        public string CardType { get; set; }

        [DisplayFormat(DataFormatString = "{0:c}")]
        public decimal TotalCommissionPaid { get; set; }

        [DisplayFormat(DataFormatString = "{0:c}")]
        public decimal TotalCommissionOwed { get; set; }

        public decimal BenefitsHotelUsage { get; set; }
        public decimal BenefitsCondoUsage { get; set; }
        public decimal BenefitsShoppingUsage { get; set; }

        public IEnumerable<CardBenefitDetailViewModel> BenefitDetails { get; set; }

        [DisplayFormat(DataFormatString = "{0:c}")]
        public decimal TotalCardAvailable
        {
            get { return TotalCardValue - TotalCardRedeemed;  }
        }

        [DisplayFormat(DataFormatString = "{0:0.00}")]
        public decimal BenefitsHotelPercent
        {
            get { return GetPercentage(BenefitsHotelUsage); }
        }

        [DisplayFormat(DataFormatString = "{0:0.00}")]
        public decimal BenefitsCondoPercent
        {
            get { return GetPercentage(BenefitsCondoUsage); }
        }

        [DisplayFormat(DataFormatString = "{0:0.00}")]
        public decimal BenefitsShoppingPercent
        {
            get { return GetPercentage(BenefitsShoppingUsage); }
        }

        private decimal GetPercentage(decimal usage)
        {
            if (TotalCardRedeemed == 0)
            {
                return 0;
            }

            return usage / TotalCardRedeemed;
        }
    }

    public class CardBenefitDetailViewModel
    {
        [DisplayFormat(DataFormatString = "{0:MM-dd-yyyy}")]
        public DateTime CheckInDate { get; set; }

        [DisplayFormat(DataFormatString = "{0:MM-dd-yyyy}")]
        public DateTime CheckOutDate { get; set; }

        [DisplayFormat(DataFormatString = "{0:c}")]
        public decimal MemberSavings { get; set; }

        [DisplayFormat(DataFormatString = "{0:c}")]
        public decimal CommissionTotal { get; set; }

        [DisplayFormat(DataFormatString = "{0:c}")]
        public decimal CommisionBroker { get; set; }

        [DisplayFormat(DataFormatString = "{0:c}")]
        public decimal CommisionClient { get; set; }

        [DisplayFormat(DataFormatString = "{0:c}")]
        public decimal CommisionAgent { get; set; }

        [DisplayFormat(DataFormatString = "{0:c}")]
        public decimal RemainingBalance { get; set; }

        [DisplayFormat(DataFormatString = "{0:MM-dd-yyyy}")]
        public DateTime? PaymentDate { get; set; }

        public string Confirmation { get; set; }
        public string Status { get; set; }
        public string Chargeback { get; set; }
    }
}
