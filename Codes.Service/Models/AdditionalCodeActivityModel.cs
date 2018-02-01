using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Codes.Service.Models
{
    [Table("AdditionalCodeActivities")]
    public class AdditionalCodeActivityModel : _BaseModel
    {
        [Key, Required]
        public int AdditionalCodeActivityId { get; set; }
        [Required]
        public int CodeId { get; set; }
        [StringLength(500), Required]
        public string ActivationCode { get; set; }
        [Required]
        public int RSIId { get; set; }
    }
}
