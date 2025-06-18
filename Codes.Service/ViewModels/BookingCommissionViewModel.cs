using Codes1.Service.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Codes1.Service.ViewModels
{
    public class BookingnCommissionViewModel
    {
       
        public int BookingId { get; set; }
        public string BookingType { get; set; }
        public decimal MemberSavings { get; set; }
        public float CompanyBucketPrecentage { get; set; }
        public float ClientPrecentage { get; set; } = 0;
        public float AgentBucketPrecentage { get; set; } = 0;
        public decimal CompanyBucketCommission { get; set; }
        public decimal ClientCommission { get; set; } = 0;
        public decimal AgentBucketCommission { get; set; } = 0;
        public List<BookingCommissionBreakdownViewModel> BookingCommissionBreakdowns { get; set; } = new List<BookingCommissionBreakdownViewModel>();
        public DateTime? VendorPaidDate { get; set; } = null;
        public DateTime? BrokerPaidDate { get; set; } = null;
        public DateTime? RecordChangeDate { get; set; } = null;
        public int? RecordChangeRSIId { get; set; } = null;
        public decimal BrokerPaidAmount { get; set; } = 0;
        public string AdminNote { get; set; } = "";


        public string Message { get; set; } = "";
        public bool IsSuccess { get; set; } = false;
    }

    public class BookingCommissionBreakdownViewModel
    {
      
        public int BookingCommissionBreakdownId { get; set; }
        
        public int BookingId { get; set; }
       
        public string PersonType { get; set; }
      
        public int Id { get; set; }
     
        public float CommissionPrecentage { get; set; }
       
        public decimal Commission { get; set; }
    }
}
