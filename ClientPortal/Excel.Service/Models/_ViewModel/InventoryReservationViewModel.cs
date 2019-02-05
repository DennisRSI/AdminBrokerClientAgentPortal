using System;
using System.Collections.Generic;
using System.Text;

namespace Excel.Service.Models._ViewModel
{
    public class InventoryReservationViewModel : _BaseReservationViewModel
    {
        public InventoryReservationViewModel()
        {

        }

        public decimal CommissionReceived { get; set; } = 0;
        public decimal CommissionProcessingFee { get; set; } = 0;
        public decimal CollectionExpense { get; set; } = 0;
    }
}
