using System;
using System.Collections.Generic;
using System.Text;

namespace Excel.Service.Models._ViewModel
{
    public class MerchantInventoryReservationViewModel : _BaseReservationViewModel
    {
        public MerchantInventoryReservationViewModel()
        {

        }

        public decimal GrossSaleWithTax { get; set; } = 0;
        public decimal CostOfHotelWithTax { get; set; } = 0;
        public decimal CreditCardProcessingFee { get; set; } = 0;
        public decimal NetProfit { get; set; } = 0;
        public float AffiliateCommissionPercentage { get; set; } = 0;
        public decimal ARNTransferFee { get; set; } = 0;
    }
}
