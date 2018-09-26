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

        public string PurchaseDateString
        {
            get { return PurchaseDate.ToString("d"); }
        }

        public string PhysicalValueString
        {
            get { return PhysicalValue.ToString("c"); }
        }

        public string VirtualValueString
        {
            get { return VirtualValue.ToString("c"); }
        }

        public string TotalValue
        {
            get
            {
                var total = PhysicalValue * PhysicalQuantity + VirtualValue * VirtualQuantity;
                return total.ToString("c");
            }
        }
    }
}
