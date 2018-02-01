using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Codes.Service.ViewModels
{
    public class CodeLookupViewModel
    {
        [JsonProperty(PropertyName = "code")]
        public string Code { get; set; } = "";
        [JsonProperty(PropertyName = "rsi_id")]
        public int RSIId { get; set; } = 0;
        [JsonProperty(PropertyName = "email_verification_date")]
        public DateTime? EmailVerificationDate { get; set; } = null;
        [JsonProperty(PropertyName = "status")]
        public string Status { get; set; } = "";
        [JsonProperty(PropertyName = "message")]
        public string Message = "";
        [JsonProperty(PropertyName = "is_success")]
        public bool IsSuccess
        {
            get { return Message.ToUpper().IndexOf("ERROR") == -1; }
        }
    }
}
