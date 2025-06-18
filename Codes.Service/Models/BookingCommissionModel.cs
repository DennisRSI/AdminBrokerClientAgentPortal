using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Codes1.Service.Models
{
    public class BookingCommissionModel
    {
        [Key, Required, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int BookingId { get; set; }
        [Required, StringLength(50)]
        public string BookingType { get; set; }
        [Required]
        public decimal MemberSavings { get; set; }
        [Required]
        public float CompanyBucketPrecentage { get; set; }
        public float ClientPrecentage { get; set; } = 0;
        public float AgentBucketPrecentage { get; set; } = 0;
        [Required]
        public decimal CompanyBucketCommission { get; set; }
        public decimal ClientCommission { get; set; } = 0;
        public decimal AgentBucketCommission { get; set; } = 0;
        public List<BookingCommissionBreakdownModel> BookingCommissionBreakdowns { get; set; } = new List<BookingCommissionBreakdownModel>();
        public DateTime? VendorPaidDate { get; set; } = null;
        public DateTime? BrokerPaidDate { get; set; } = null;
        public DateTime? RecordChangeDate { get; set; } = null;
        public int? RecordChangeRSIId { get; set; } = null;
        public decimal BrokerPaidAmount { get; set; } = 0;
        public string AdminNote { get; set; } = "";
    }

    public class BookingCommissionBreakdownModel
    {
        [Key, Required]
        public int BookingCommissionBreakdownId { get; set; }
        [Required]
        public int BookingId { get; set; }
        public BookingCommissionModel BookingCommission { get; set; } = new BookingCommissionModel();
        [StringLength(50), Required]
        public string PersonType { get; set; }
        [Required]
        public int Id { get; set; }
        [Required]
        public float CommissionPrecentage { get; set; }
        [Required]
        public decimal Commission { get; set; }

    }
}
