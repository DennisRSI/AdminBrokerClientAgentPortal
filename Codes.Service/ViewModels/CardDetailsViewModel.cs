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
        public decimal TotalHotelAndCondoSavings
        {
            get { return TotalHotelSavings + TotalCondoSavings; }
        }

        [DisplayFormat(DataFormatString = "{0:c}")]
        public decimal TotalShoppingSavings { get; set; }

        [DisplayFormat(DataFormatString = "{0:c}")]
        public decimal TotalDiningSavings { get; set; }

        public string ClientName { get; set; }
        public string ClientCompanyName { get; set; }
        public string ClientCampaign { get; set; }

        [DisplayFormat(DataFormatString = "{0:MM-dd-yyyy}")]
        public DateTime ActivationDate { get; set; }

        [DisplayFormat(DataFormatString = "{0:f2}")]
        public decimal BrokerCommission { get; set; }

        [DisplayFormat(DataFormatString = "{0:f2}")]
        public decimal ClientCommission { get; set; }

        [DisplayFormat(DataFormatString = "{0:f2}")]
        public decimal AgentCommission { get; set; }

        public string ActivatedBy { get; set; }

        [DisplayFormat(DataFormatString = "{0:c}")]
        public decimal FaceValue { get; set; }

        [DisplayFormat(DataFormatString = "{0:c}")]
        public decimal TotalCardRedeemed
        {
            get { return FaceValue - TotalCardAvailable; }
        }

        public string CardType { get; set; }

        [DisplayFormat(DataFormatString = "{0:c}")]
        public decimal TotalCommissionPaid { get; set; }

        [DisplayFormat(DataFormatString = "{0:c}")]
        public decimal TotalCommissionOwed { get; set; }

        public IEnumerable<CardBenefitDetailViewModel> BenefitDetails { get; set; }

        [DisplayFormat(DataFormatString = "{0:c}")]
        public decimal TotalCommission
        {
            get { return BrokerCommission + ClientCommission + AgentCommission; }
        }

        [DisplayFormat(DataFormatString = "{0:c}")]
        public decimal TotalCardAvailable { get; set; }

        [DisplayFormat(DataFormatString = "{0:0.0}")]
        public decimal BenefitsHotelPercent
        {
            get { return GetPercentage(TotalHotelSavings); }
        }

        [DisplayFormat(DataFormatString = "{0:0.0}")]
        public decimal BenefitsCondoPercent
        {
            get { return GetPercentage(TotalCondoSavings); }
        }

        [DisplayFormat(DataFormatString = "{0:0.0}")]
        public decimal BenefitsShoppingPercent
        {
            get { return GetPercentage(TotalShoppingSavings); }
        }

        public string ChartMonthlyLabel { get; set; }
        public string ChartMonthlyHotel { get; set; }
        public string ChartMonthlyCondo { get; set; }
        public string ChartMonthlyShopping { get; set; }
        public string ChartMonthlyDining { get; set; }

        private decimal GetPercentage(decimal usage)
        {
            if (TotalCardRedeemed == 0)
            {
                return 0;
            }

            return usage / TotalCardRedeemed * 100;
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
        public string UsageType { get; set; }
    }
}
