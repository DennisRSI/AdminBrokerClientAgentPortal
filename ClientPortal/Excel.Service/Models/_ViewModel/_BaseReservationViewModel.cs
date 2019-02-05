using System;
using System.Collections.Generic;
using System.Text;

namespace Excel.Service.Models._ViewModel
{
    public class _BaseReservationViewModel
    {
        public string Affiliate { get; set; } = "";
        public string Site { get; set; } = "";
        public string Confirmation { get; set; } = "";
        public string Property { get; set; } = "";
        public string Guest { get; set; } = "";
        public DateTime CheckOut { get; set; } = new DateTime();
        public DateTime Booked { get; set; } = new DateTime();
        public string ReservationStatus { get; set; } = "";
        public int RooomNights { get; set; } = 0;
        public decimal FlatAmount { get; set; } = 0;
        public decimal RoomRevenue { get; set; } = 0;

        public decimal ARNCallCenterFee { get; set; } = 0;
        public decimal NetCommission { get; set; } = 0;
        public decimal SubAffiliateCommission { get; set; } = 0;
        public decimal NetCommissionAfterSubAffiliate { get; set; } = 0;
        public string CID { get; set; } = "";
        public string ReservationId { get; set; } = "";
        public string RegistrationId { get; set; } = "";
        public string RegistrationName { get; set; } = "";





    }
}
