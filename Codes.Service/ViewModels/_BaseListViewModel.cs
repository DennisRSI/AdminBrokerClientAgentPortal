using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Codes1.Service.ViewModels
{
    public class _BaseListViewModel
    {
        private string _phone = "", _name = "";

        public _BaseListViewModel()
        {

        }

        public _BaseListViewModel(string accountId, string firstName, string middleName, string lastName, string phone, string extension
            , string email, string company, DateTime activationDate, DateTime? deactivationDate)
        {
            AccountId = accountId;
            FirstName = firstName;
            MiddleName = middleName;
            LastName = lastName;
            Email = email;
            Company = company;
            Phone = phone;
            Extension = extension;
            ActivationDate = activationDate;
            DeactivationDate = deactivationDate;
        }
        [JsonProperty(PropertyName = "DT_RowId")]
        public string AccountId { get; set; }
        [JsonProperty(PropertyName = "first_name")]
        public string FirstName { get; set; } = "";
        [JsonProperty(PropertyName = "middle_name")]
        public string MiddleName { get; set; } = "";
        [JsonProperty(PropertyName = "last_name")]
        public string LastName { get; set; } = "";
        [JsonProperty(PropertyName = "email")]
        public string Email { get; set; } = "";
        [JsonProperty(PropertyName = "company")]
        public string Company { get; set; } = "";
        [JsonProperty(PropertyName = "full_name")]
        public string FullName
        {
            get
            {
                _name = FirstName;
                if (MiddleName != null && MiddleName.Length > 0)
                    _name += $" {MiddleName}";
                _name += $" {LastName}";
                return _name;
            }
        }
        [JsonProperty(PropertyName = "extension")]
        public string Extension { get; set; } = "";
        [JsonProperty(PropertyName = "phone")]
        public string Phone
        {
            get
            {
                string phone = String.Format("{0:(###) ###-####}", _phone);
                if (Extension != null && Extension.Length > 0)
                    phone += $" {Extension}";

                return phone;
            }
            set
            {
                _phone = value;
            }
        }
        [JsonProperty(PropertyName = "activation_date")]
        public DateTime ActivationDate { get; set; }
        [JsonProperty(PropertyName = "deactivation_date")]
        public DateTime? DeactivationDate { get; set; }
        
    }

    
}
