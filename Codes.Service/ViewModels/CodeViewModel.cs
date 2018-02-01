using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Codes.Service.ViewModels
{
    public class CodeViewModel
    {
        [JsonProperty(PropertyName = "code")]
        public string Code { get; set; }
        [JsonProperty(PropertyName = "number_of_users")]
        public int NumberOfUses { get; set; } = 1;
        [JsonProperty(PropertyName = "start_date")]
        public DateTime? StartDate { get; set; }
        [JsonProperty(PropertyName = "end_date")]
        public DateTime? EndDate { get; set; }
        [JsonProperty(PropertyName = "points")]
        public int Points { get; set; }
        [JsonProperty(PropertyName = "condo_rewards")]
        public int CondoRewards { get; set; } = 0;
        [JsonProperty(PropertyName = "charge_amount")]
        public decimal ChargeAmount { get; set; } = 0;
        [JsonProperty(PropertyName = "issuer_reference")]
        public string IssuerReference { get; set; } = "";
    }

    public class CodesViewModel
    {
        [JsonProperty(PropertyName = "issuer")]
        public string Issuer { get; set; }
        [JsonProperty(PropertyName = "codes")]
        public List<CodeViewModel> Codes { get; set; } = new List<CodeViewModel>();
        
    }

    public class CodesReturnViewModel
    {
        [JsonProperty(PropertyName = "issuer")]
        public string Issuer { get; set; }
        [JsonProperty(PropertyName = "error_codes")]
        public List<KeyValuePair<string, string>> ErrorCodes { get; set; } = new List<KeyValuePair<string, string>>();
        [JsonProperty(PropertyName = "total_sent")]
        public int TotalSent { get; set; } = 0;
        [JsonProperty(PropertyName = "total_processed")]
        public int TotalProcessed { get; set; } = 0;
        [JsonProperty(PropertyName = "total_succeeded")]
        public int TotalSucceeded { get; set; } = 0;
        [JsonProperty(PropertyName = "total_failed")]
        public int TotalFailed { get; set; } = 0;

    }

    public class CodesCallbackReturnViewModel
    {
        [JsonProperty(PropertyName = "callback_id")]
        public int CallbackId { get; set; } = 0;

        [JsonProperty(PropertyName = "status")]
        public string Status { get; set; } = "";
        [JsonProperty(PropertyName = "issuer")]
        public string Issuer { get; set; } = "";
        [JsonProperty(PropertyName = "total_sent")]
        public int TotalSent { get; set; } = 0;
        [JsonProperty(PropertyName = "total_processed")]
        public int TotalProcessed { get; set; } = 0;
        [JsonProperty(PropertyName = "total_succeeded")]
        public int TotalSucceeded { get; set; } = 0;
        [JsonProperty(PropertyName = "total_failed")]
        public int TotalFailed { get; set; } = 0;
        [JsonProperty(PropertyName = "error_codes")]
        public string ErrorCodes { get; set; } = "";
    }
}
