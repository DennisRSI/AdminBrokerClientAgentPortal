using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace Codes1.Service.Models
{
    [Table("UsedCodes")]
    public class UsedCodeModel : _BaseModel
    {
        private string _phone1 = "", _phone2 = "";

        [Key, Required]
        public int UsedCodeId { get; set; }
        [Required, StringLength(1000)]
        public string Code { get; set; }
        [Required, StringLength(50)]
        public string CodeType { get; set; } = "Virtual";
        public string DeactivationReason { get; set; } = null;
        [Required]
        public int? PackageId { get; set; } = 0;
        [Required]
        public int NumberOfUses { get; set; } = 1;
        [Required]
        public float Points { get; set; } = 0;
        public DateTime? StartDate { get; set; } = null;
        public DateTime? EndDate { get; set; } = null;
        [Required]
        public decimal Cost { get; set; } = 0;
        [Required]
        public bool VerifyEmail { get; set; } = true;
        [Required]
        public DateTime CodeCreatedDate { get; set; }
        [Required]
        public DateTime EmailSentDate { get; set; }
        [Required]
        public int RSIId { get; set; }
        [Required, StringLength(255)]
        public string FirstName { get; set; }
        [StringLength(255)]
        public string MiddleName { get; set; }
        [Required, StringLength(255)]
        public string LastName { get; set; }
        [StringLength(50)]
        public string Phone1
        {
            get
            {
                return _phone1;
            }
            set
            {
                if (value != null)
                {
                    //_phone1 = new String(value.Where(Char.IsDigit).ToArray());
                    _phone1 = new String(value.Where(Char.IsDigit).ToArray());
                }
            }
        }
        [StringLength(50)]
        public string Phone2
        {
            get
            {
                return _phone2;
            }
            set
            {
                if (value != null)
                {
                    //_phone1 = new String(value.Where(Char.IsDigit).ToArray());
                    _phone2 = new String(value.Where(Char.IsDigit).ToArray());
                }
            }
        }
        [Required, StringLength(100)]
        public string Email { get; set; }
        [StringLength(255)]
        public string Address1 { get; set; }
        [StringLength(255)]
        public string Address2 { get; set; }
        [StringLength(100)]
        public string City { get; set; }
        [StringLength(100)]
        public string State { get; set; }
        [StringLength(50)]
        public string PostalCode { get; set; }
        [StringLength(100)]
        public string Country { get; set; }
        [Required, StringLength(100)]
        public string Username { get; set; }
        [Required, StringLength(100)]
        public string Password { get; set; }
        [Required]
        public decimal Paid { get; set; } = 0;
        [Required]
        public int OldCodeId { get; set; } = 0;
        [Required]
        public int OldCodeActivityId { get; set; } = 0;
        [Required]
        public int BrokerId { get; set; }
        [ForeignKey("BrokerId")]
        public BrokerModel Broker { get; set; }
        [Required]
        public int CodeRangeId { get; set; }
        [ForeignKey("CodeRangeId")]
        public CodeRangeModel CodeRange { get; set; }
        public int? CampaignId { get; set; } = null;
        [ForeignKey("CampaignId")]
        public CampaignModel Campaign { get; set; } = null;

        /*public int? ClientId { get; set; } = null;
        [ForeignKey("ClientId")]
        public CampaignModel Client { get; set; } = null;*/
    }
}
