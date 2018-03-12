using System.ComponentModel.DataAnnotations;

namespace Codes.Service.ViewModels
{
    public class PurchaseViewModel
    {
        [DisplayFormat(DataFormatString = "{0:p2}")]
        public decimal RateTier1 { get; set; } = 0.0055M;

        [DisplayFormat(DataFormatString = "{0:p2}")]
        public decimal RateTier2 { get; set; } = 0.0050M;

        [DisplayFormat(DataFormatString = "{0:p2}")]
        public decimal RateTier3 { get; set; } = 0.0040M;

        [DisplayFormat(DataFormatString = "{0:p2}")]
        public decimal RateTier4 { get; set; } = 0.0035M;

        [DisplayFormat(DataFormatString = "{0:p2}")]
        public decimal RateTier5 { get; set; } = 0.0030M;

        [DisplayFormat(DataFormatString = "{0:p2}")]
        public decimal RateTier6 { get; set; } = 0.0025M;

        public int PhysicalValue { get; set; }
        public int PhysicalQuantity { get; set; }

        public int VirtualValue { get; set; }
        public int VirtualQuantity { get; set; }

        public string CardName { get; set; }
        public string CardZip { get; set; }
        public string CardNumber { get; set; }
        public string CardExpiration { get; set; }
        public string CardCvc { get; set; }

        public string ShipAddress { get; set; }
        public string ShipCity { get; set; }
        public string ShipState { get; set; }
        public string ShipZip { get; set; }

        public decimal ShippingCost { get; set; }
    }
}
