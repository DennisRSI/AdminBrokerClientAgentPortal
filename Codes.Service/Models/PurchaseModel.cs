using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Codes1.Service.Models
{
    [Table("Purchases")]
    public class PurchaseModel : _BaseModel
    {
        [Key, Required]
        public int PurchaseId { get; set; }

        [Required]
        public int PhysicalValue { get; set; }

        [Required]
        public int PhysicalQuantity { get; set; }

        [Required]
        public int VirtualValue { get; set; }

        [Required]
        public int VirtualQuantity { get; set; }

        [Required]
        [StringLength(200)]
        public string FullName { get; set; }

        [Required]
        [StringLength(10)]
        public string BillingZip { get; set; }

        [Required]
        [StringLength(200)]
        public string Address { get; set; }

        [Required]
        [StringLength(200)]
        public string City { get; set; }

        [Required]
        [StringLength(2)]
        public string State { get; set; }

        [Required]
        [StringLength(10)]
        public string ShippingZip { get; set; }

        [Required]
        [StringLength(4)]
        public string CreditCardLast4 { get; set; }

        [Required]
        public int BrokerId { get; set; }

        [ForeignKey("BrokerId")]
        public BrokerModel Broker { get; set; }

        public List<CodeModel> Codes { get; set; }
    }
}
