using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Codes1.Service.Models
{
    public class _BaseModel
    {
        [Required]
        [Display(Name = "Is Active")]
        [JsonProperty(PropertyName ="is_active")]
        public bool IsActive { get; set; } = true;

        [Timestamp]
        [Display(Name = "Row Version")]
        [JsonIgnore]
        public Byte[] RowVersion { get; set; }

        [Required]
        [Display(Name = "Creation Date")]
        [JsonProperty(PropertyName = "creation_date")]
        public DateTime CreationDate { get; set; } = DateTime.Now;

        [Required]
        [StringLength(50)]
        [Display(Name = "Creator IP")]
        [JsonProperty(PropertyName = "creator_ip")]
        public string CreatorIP { get; set; } = "127.0.0.1";

        [Display(Name = "Deactivation Date")]
        [JsonProperty(PropertyName = "deactivation_date")]
        public DateTime? DeactivationDate { get; set; } = null;

        [JsonProperty(PropertyName = "message")]
        [NotMapped]
        public string Message { get; set; } = "";

        [JsonProperty(PropertyName = "is_success")]
        [NotMapped]
        public bool IsSuccess { get { return Message.ToUpper().IndexOf("ERROR") == -1; } }
    }
}
