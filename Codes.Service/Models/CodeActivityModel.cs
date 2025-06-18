using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Codes1.Service.Models
{
    public class CodeActivityModel : ActivationModel
    {
        [Key]
        [Required]
        [Display(Name = "Code Activity Id")]
        [JsonProperty(PropertyName = "code_activity_id")]
        public int CodeActivityId { get; set; }

        [Required]
        [Display(Name = "RSI Id")]
        [JsonProperty(PropertyName = "rsi_id")]
        public int RSIId { get; set; } = 0;

        [JsonProperty(PropertyName = "email_verified_date")]
        public DateTime? EmailVerifiedDate { get; set; } = null;

        [NotMapped]
        [JsonProperty(PropertyName = "hotel_points")]
        public decimal HotelPoints { get; set; } = 0;
        

        [Required]
        [JsonIgnore]
        public int CodeId { get; set; }

        [Required]
        [ForeignKey("CodeId")]
        [JsonIgnore]
        public CodeModel Code { get; set; } = new CodeModel();
    }
}
