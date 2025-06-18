using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Codes1.Service.ViewModels
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

        [DisplayFormat(DataFormatString = "{0:c}")]
        public decimal TotalInternetPrice { get; set; }

        [DisplayFormat(DataFormatString = "{0:c}")]
        public decimal TotalYouPayPrice { get; set; }

        [DisplayFormat(DataFormatString = "{0:c}")]
        public decimal TotalCommission { get; set; }

        [DisplayFormat(DataFormatString = "{0:c}")]
        public float TotalPointsBalance { get; set; }

        public IEnumerable<ProductionDetailItemViewModel> DetailsTable { get; set; }

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

        [DisplayFormat(DataFormatString = "{0:c}")]
        public decimal InternetPrice { get; set; }

        [DisplayFormat(DataFormatString = "{0:c}")]
        public decimal YouPayPrice { get; set; }

        [DisplayFormat(DataFormatString = "{0:c}")]
        public Single PointsBalance { get; set; }

        [DisplayFormat(DataFormatString = "{0:c}")]
        public decimal Commission { get; set; }

        [DisplayFormat(DataFormatString = "{0:c}")]
        public decimal MemberSavings
        {
            get { return InternetPrice - YouPayPrice; }
        }
    }
}
