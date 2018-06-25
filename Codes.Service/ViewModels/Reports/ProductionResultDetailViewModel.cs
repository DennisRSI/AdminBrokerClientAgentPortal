using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ClientPortal.Models
{
    public class ProductionResultDetailViewModel
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
        
        public int TotalNights { get; set; }
        public string TotalInternetPrice { get; set; }
        public string TotalYouPay { get; set; }
        public string TotalMemberSavings { get; set; }
        public string TotalCommission { get; set; }
        public string TotalPointBalance { get; set; }

        public IEnumerable<ProductionDetailItemViewModel> DetailsTable { get; set; }

        public string ReportTime
        {
            get { return DateTime.Now.ToString(); }
        }
    }

    public class ProductionDetailItemViewModel
    {
        public string ConfirmationNumber { get; set; }
        public string CardNumber { get; set; }
        public string MemberFullName { get; set; }
        public string GuestFullName { get; set; }

        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime BookingDate { get; set; }

        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime CheckInDate { get; set; }

        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime CheckOutDate { get; set; }

        public string Canceled { get; set; }
        public decimal InternetPrice { get; set; }
        public decimal YouPayPrice { get; set; }
        public Single MemberSavings { get; set; }
        public Single PointsBalance { get; set; }
    }
}
