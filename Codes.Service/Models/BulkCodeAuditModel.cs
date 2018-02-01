using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Codes.Service.Models
{
    [Table("BulkCodeAudits")]
    public class BulkCodeAuditModel : _BaseModel
    {
        [Key]
        [Required]
        public int BulkCodeAuditId { get; set; }
        public DateTime? FinishDate { get; set; } = null;
        [Required]
        [StringLength(50)]
        public string Issuer { get; set; } = "";
        public string Errors { get; set; } = "";
        public string OrigionalFileSent { get; set; } = "";
        public int TotalSent { get; set; } = 0;
        public int TotalProcessed { get; set; } = 0;
        public int TotalSucceeded { get; set; } = 0;
        public int TotalFailed { get; set; } = 0;
    }
}
