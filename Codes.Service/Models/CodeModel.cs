using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Codes.Service.Models
{
    [Table("Codes")]
    public class CodeModel : _BaseModel
    {
        [Key]
        [Required]
        [JsonIgnore]
        public int CodeId { get; set; }
        [Required]
        [StringLength(500)]
        [JsonProperty(PropertyName = "activation_code")]
        [Display(Name = "Code")]
        public string  Code { get; set; }
        [Required]
        [StringLength(50)]
        [Display(Name = "Issuer")]
        [JsonProperty(PropertyName = "issuer")]
        public string Issuer { get; set; } = "";
        [StringLength(100)]
        [Display(Name = "IssuerReference")]
        [JsonProperty(PropertyName = "issuer_reference")]
        public string IssuerReference { get; set; } = "";
        [Required]
        [Display(Name = "Number of Uses (0 for unlimited)")]
        [JsonProperty(PropertyName = "number_of_uses")]
        public int NumberOfUses { get; set; } = 1;
        [Display(Name = "Start Date")]
        [JsonProperty(PropertyName = "start_date")]
        public DateTime? StartDate { get; set; } = null;
        [JsonProperty(PropertyName = "end_date")]
        [Display(Name = "End Date")]
        public DateTime? EndDate { get; set; } = null;
        [Required]
        [Display(Name = "Hotel Points")]
        [JsonProperty(PropertyName = "hotel_points")]
        public int HotelPoints { get; set; } = 0;
        [Required]
        [Display(Name = "Condo Rewards")]
        [JsonProperty(PropertyName = "condo_rewards")]
        public int CondoRewards { get; set; } = 0;
        [Required]
        [Display(Name = "Charge Amount (0 of free)")]
        [JsonProperty(PropertyName = "charge_amount")]
        public decimal ChargeAmount { get; set; } = 0;
        [Required]
        [JsonProperty(PropertyName = "verify_email")]
        public bool VerifyEmail { get; set; } = true;
        [Required]
        [JsonProperty(PropertyName = "package_id")]
        public int PackageId { get; set; } = 0;
        /*[JsonProperty(PropertyName = "broker_id")]
        public int BrokerId { get; set; } = 0;
        [NotMapped, JsonProperty(PropertyName = "broker_name", NullValueHandling = NullValueHandling.Ignore)]
        public string BrokerName { get; set; }*/

        [JsonIgnore]
        public ICollection<CodeActivityModel> CodeActivities { get; set; } = new List<CodeActivityModel>();

    }
}
