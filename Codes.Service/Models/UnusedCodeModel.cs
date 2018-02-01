using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Codes.Service.Models
{
    [Table("UnusedCodes")]
    public class UnusedCodeModel : _BaseModel
    {
        [Key, Required]
        public int UnusedCodeId { get; set; }
        [Required, StringLength(1000)]
        public string Code { get; set; }
        [Required, StringLength(50)]
        public string CodeType { get; set; } = "Virtual";
        public string DeactivationReason { get; set; } = null;
        [Required]
        public int OldCodeId { get; set; } = 0;
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
       





    }
}
