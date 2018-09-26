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
        public bool BenefitDining { get; set; }

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

        [DisplayFormat(DataFormatString = "{0:c}")]
        public decimal BrokerCommission { get; set; }

        [DisplayFormat(DataFormatString = "{0:c}")]
        public decimal ClientCommission { get; set; }

        [DisplayFormat(DataFormatString = "{0:c}")]
        public decimal AgentCommission { get; set; }

        public string ActivatedBy { get; set; }

        [DisplayFormat(DataFormatString = "{0:c}")]
        public decimal FaceValue { get; set; }

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
        public Dictionary<string, CardMonthlyUsage> MonthlyUsage { get; set; } = new Dictionary<string, CardMonthlyUsage>();

        [DisplayFormat(DataFormatString = "{0:c}")]
        public decimal TotalCommission
        {
            get { return BrokerCommission + ClientCommission + AgentCommission; }
        }

        [DisplayFormat(DataFormatString = "{0:c}")]
        public decimal TotalCardAvailable
        {
            get { return FaceValue - TotalCardRedeemed;  }
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
        public decimal CommissionBroker { get; set; }

        [DisplayFormat(DataFormatString = "{0:c}")]
        public decimal CommissionClient { get; set; }

        [DisplayFormat(DataFormatString = "{0:c}")]
        public decimal CommissionAgent { get; set; }

        [DisplayFormat(DataFormatString = "{0:c}")]
        public decimal RemainingBalance { get; set; }

        [DisplayFormat(DataFormatString = "{0:MM-dd-yyyy}")]
        public DateTime? PaymentDate { get; set; }

        public string Confirmation { get; set; }
        public string Status { get; set; }
        public string Chargeback { get; set; }
    }

    public class CardMonthlyUsage
    {
        public decimal Hotel { get; set; }
        public decimal Condo { get; set; }
        public decimal Shopping { get; set; }
        public decimal Dining { get; set; }
    }
}
