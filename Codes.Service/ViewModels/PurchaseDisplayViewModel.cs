using System;
using System.ComponentModel.DataAnnotations;

namespace Codes.Service.ViewModels
{
    public class PurchaseDisplayViewModel
    {
        public DateTime PurchaseDate { get; set; }
        public string OrderId { get; set; }

        public int PhysicalValue { get; set; }
        public int PhysicalQuantity { get; set; }

        public int VirtualValue { get; set; }
        public int VirtualQuantity { get; set; }

        public string SequenceStart { get; set; }
        public string SequenceEnd { get; set; }

        public int TotalValue
        {
            get
            {
                return PhysicalValue * PhysicalQuantity + VirtualValue * VirtualQuantity;
            }
        }
    }
}
