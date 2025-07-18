using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ClientPortal.Models
{
    public class ProductionResultSummaryViewModel
    {
        public string Type { get; set; }
        public string AccountName { get; set; }

        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime CheckoutStartDate { get; set; }

        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime CheckoutEndDate { get; set; }

        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime BookingStartDate { get; set; }

        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime BookingEndDate { get; set; }
        
        [DisplayFormat(DataFormatString = "{0:c}")]
        public decimal TotalInternetPrice { get; set; }

        [DisplayFormat(DataFormatString = "{0:c}")]
        public decimal TotalYouPayPrice { get; set; }

        [DisplayFormat(DataFormatString = "{0:c}")]
        public decimal TotalCommissionEarned { get; set; }

        public List<ProductionSummaryTableViewModel> Tables { get; set; } = new List<ProductionSummaryTableViewModel>();

        [DisplayFormat(DataFormatString = "{0:c}")]
        public decimal TotalMemberSavings
        {
            get { return TotalInternetPrice - TotalYouPayPrice; }
        }

        public string ReportTime
        {
            get { return DateTime.Now.ToString(); }
        }

        public string Message { get; set; } = "Success";
        public bool IsSuccess { get; set; } = true;
    }

    public class ProductionSummaryTableViewModel
    {
        public string AccountType { get; set; }
        public string AccountName { get; set; }
        public string ReportGroupName { get; set; }
        public bool IsCampaignReport { get; set; }

        public List<ProductionSummaryItemViewModel> Items { get; set; }

        [DisplayFormat(DataFormatString = "{0:c}")]
        public decimal TotalInternetPrice { get; set; }

        [DisplayFormat(DataFormatString = "{0:c}")]
        public decimal TotalYouPayPrice { get; set; }

        [DisplayFormat(DataFormatString = "{0:c}")]
        public decimal TotalCommissionEarned { get; set; }

        [DisplayFormat(DataFormatString = "{0:c}")]
        public decimal TotalMemberSavings
        {
            get { return TotalInternetPrice - TotalYouPayPrice; }
        }

        public string FinalColumnName
        {
            get
            {
                if (IsCampaignReport)
                {
                    return "Transaction Date";
                }

                return "Paid Date";
            }
        }
    }

    public class ProductionSummaryItemViewModel
    {
        public string AccountName { get; set; }

        [DisplayFormat(DataFormatString = "{0:c}")]
        public decimal InternetPrice { get; set; }

        [DisplayFormat(DataFormatString = "{0:c}")]
        public decimal YouPayPrice { get; set; }

        [DisplayFormat(DataFormatString = "{0:c}")]
        public decimal CommissionEarned { get; set; }

        public DateTime PaidDate { get; set; }

        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime? TransactionDate { get; set; }

        [DisplayFormat(DataFormatString = "{0:c}")]
        public decimal MemberSavings
        {
            get { return InternetPrice - YouPayPrice; }
        }
    }
}
