using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Codes1.Service.ViewModels
{
    public class CommissionResultViewModel
    {
        public string Type { get; set; }
        public string AccountName { get; set; }
        public string QueryType { get; set; }

        public bool UserIsBroker
        {
            get { return AccountName == "Hidden"; }
        }

        public string TypeDisplay
        {
            get
            {
                if (UserIsBroker)
                {
                    return "Client";
                }

                return Type;
            }
        }

        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime CheckoutStartDate { get; set; }

        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime CheckoutEndDate { get; set; }

        public int? AgentId { get; set; }
        public int? ClientId { get; set; }
        public int? BrokerId { get; set; }

        public List<CommissionResultTableViewModel> Tables { get; set; }

        [DisplayFormat(DataFormatString = "{0:N0}")]
        public int TotalCards { get; set; }

        [DisplayFormat(DataFormatString = "{0:N0}")]
        public int TotalTransactions { get; set; }

        [DisplayFormat(DataFormatString = "{0:c}")]
        public decimal TotalInternetPrice { get; set; }

        [DisplayFormat(DataFormatString = "{0:c}")]
        public decimal TotalYouPayPrice { get; set; }

        [DisplayFormat(DataFormatString = "{0:c}")]
        public decimal TotalMemberSavings { get; set; }

        [DisplayFormat(DataFormatString = "{0:c}")]
        public decimal TotalCommissionEarned { get; set; }

        public string ReportTime
        {
            get { return DateTime.Now.ToString(); }
        }

        public string Message { get; set; } = "";
        public bool IsSuccess { get; set; } = false;
    }

    public class CommissionResultTableViewModel
    {
        public string AccountType { get; set; }
        public string AccountName { get; set; }
        public string ReportGroupName { get; set; }

        public List<CommissionResultItemViewModel> Items { get; set; }

        [DisplayFormat(DataFormatString = "{0:N0}")]
        public int TotalCards { get; set; }

        [DisplayFormat(DataFormatString = "{0:N0}")]
        public int TotalTransactions { get; set; }

        [DisplayFormat(DataFormatString = "{0:c}")]
        public decimal TotalInternetPrice { get; set; }

        [DisplayFormat(DataFormatString = "{0:c}")]
        public decimal TotalYouPayPrice { get; set; }

        [DisplayFormat(DataFormatString = "{0:c}")]
        public decimal TotalMemberSavings { get; set; }

        [DisplayFormat(DataFormatString = "{0:c}")]
        public decimal TotalCommissionEarned { get; set; }
    }

    public class CommissionResultItemViewModel
    {
        public string AccountName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string CommissionType { get; set; }

        [DisplayFormat(DataFormatString = "{0:N0}")]
        public int NumberCards { get; set; }

        [DisplayFormat(DataFormatString = "{0:N0}")]
        public int NumberTransaction { get; set; }

        [DisplayFormat(DataFormatString = "{0:c}")]
        public decimal InternetPrice { get; set; }

        [DisplayFormat(DataFormatString = "{0:c}")]
        public decimal YouPayPrice { get; set; }

        [DisplayFormat(DataFormatString = "{0:c}")]
        public decimal MemberSavings { get; set; }

        [DisplayFormat(DataFormatString = "{0:c}")]
        public decimal CommissionEarned { get; set; }

        public DateTime PaidDate { get; set; }

        public string DisplayChildren
        {
            get
            {
                string result = String.Empty;

                foreach (var child in Children)
                {
                    result += $@"{child.Name}|{child.CommissionEarned.ToString("C")};";
                }

                return result;
            }
        }

        public IList<CommissionResultChildViewModel> Children { get; set; } = new List<CommissionResultChildViewModel>();
    }

    public class CommissionResultChildViewModel
    {
        public string Name { get; set; }
        public decimal CommissionEarned { get; set; }
    }
}
