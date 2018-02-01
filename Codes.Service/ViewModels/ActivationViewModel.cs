using Codes.Service.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Codes.Service.ViewModels
{
    public class ActivationViewModel
    {
        [JsonProperty(PropertyName = "code_item")]
        public CodeModel Code { get; set; }
        [JsonProperty(PropertyName = "activities")]
        public ICollection<CodeActivityModel> Activities { get; set; } = new List<CodeActivityModel>();
        [JsonIgnore]
        public CodeActivityModel SelectedActivity { get; set; } = new CodeActivityModel();
        [JsonProperty(PropertyName = "number_of_activations")]
        public int NumberOfActivations
        {
            get
            {
                return Activities.Count();
            }
        }
        [JsonProperty(PropertyName = "code_status")]
        public KeyValuePair<bool, string> CodeStatus
        {
            get
            {

                KeyValuePair<bool, string> message = new KeyValuePair<bool, string>(false, "Not implemented");

                if (Code != null && Code.Code != null && Code.Code.Length > 0)
                {

                    bool startGood = false, endGood = false;

                    if (Code.StartDate == null || Code.StartDate <= DateTime.Now)
                    {
                        startGood = true;
                    }
                    else
                    {
                        message = new KeyValuePair<bool, string>(false, $"Error: Cannot activate until {Code.StartDate.GetValueOrDefault().ToString("MM/DD/YYYY")}.");
                    }

                    if (Code.EndDate == null || Code.EndDate <= DateTime.Now)
                    {
                        endGood = true;
                    }
                    else
                    {
                        message = new KeyValuePair<bool, string>(false, $"Error: Code expired on {Code.EndDate.GetValueOrDefault().AddDays(1).ToString("MM/DD/YYYY")}.");
                    }

                    if (startGood && endGood)
                    {
                        if (Code.NumberOfUses == 0 || Code.NumberOfUses > NumberOfActivations)
                        {
                            message = new KeyValuePair<bool, string>(true, "This code is ok to activate");
                        }
                        else
                        {
                            message = new KeyValuePair<bool, string>(false, "Error: Code has exceeded the number of times it can be activated.");
                        }
                    }
                }
                else
                {
                    message = new KeyValuePair<bool, string>(false, "Error: Code is not found in our system.");
                }

                return message;
            }
        }
        [JsonProperty(PropertyName = "message")]
        public string Message { get; set; } = "";
        [JsonProperty(PropertyName = "is_success")]
        public bool IsSuccess
        {
            get
            {
                return Message.ToUpper().IndexOf("ERROR") == -1;
            }
        }
    }

    public class ActivationsViewModel
    {
        [JsonProperty(PropertyName = "activation_codes")]
        public List<ActivationViewModel> Activations { get; set; } = new List<ActivationViewModel>();
        [JsonProperty(PropertyName = "activation_code_count")]
        public int ActivationCodeCount { get { return Activations.Count; } }
        [JsonProperty(PropertyName = "message")]
        public string Message { get; set; } = "";
        [JsonProperty(PropertyName = "is_success")]
        public bool IsSuccess { get { return Message.ToUpper().IndexOf("ERROR") == -1 ? true : false; } }
    }
}
