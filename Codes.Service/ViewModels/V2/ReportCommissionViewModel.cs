using System;
using System.Collections.Generic;
using System.Text;

namespace Codes1.Service.ViewModels.V2
{
    public class ReportCommissionBaseViewModel
    {
        public List<ReportCommissionBaseViewModel> Items { get; set; } = new List<ReportCommissionBaseViewModel>();
        public int TotalItems { get; set; } = 0;
        public string ReportGroupName { get; set; }
        public string AccountType { get; set; }


        public int NumberOfCards { get; set; } = 0;
        public int NumberOfTransactions { get; set; } = 0;
        public decimal InternetPrice { get; set; } = 0;
        public decimal YouPayPrice { get; set; } = 0;
        public decimal MemberSavings { get; set; } = 0;
    }

    public class ReportCommissionViewModel
    {
        public string Client { get; set; }
        public int  NumberOfCards { get; set; }
        public int NumberOfTransactions { get; set; }
        public decimal InternetPrice { get; set; }
        public decimal YouPayPrice { get; set; }
        public decimal MemberSavings { get; set; }
        public decimal Commission { get; set; }
        public string PaidDate { get; set; } = "";
    }

    public class ReportCommissionDetailViewModel
    {
        public string PersonType { get; set; }
        public decimal Commission { get; set; }
    }
}
