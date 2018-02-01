using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Codes.Service.ViewModels
{
    public class ActivationLineItemsViewModel
    {
        [JsonProperty(PropertyName = "results")]
        public List<ActivationLineItemViewModel> Results { get; set; } = new List<ActivationLineItemViewModel>();
        [JsonProperty(PropertyName = "records_returned")]
        public int RecordsReturned { get { return Results.Count(); } }
        [JsonProperty(PropertyName = "total_records")]
        public int TotalRecords { get; set; } = 0;
        
        [JsonProperty(PropertyName = "message")]
        public string Message { get; set; } = "";
        [JsonProperty(PropertyName = "is_success")]
        public bool IsSuccess { get { return Message == null || Message.Length < 1 || Message.ToUpper().IndexOf("ERROR") == -1; } }
    }

    public class ActivationLineItemViewModel
    {
        private string _phone1 = "", _phone2 = "", _country_code = "USA";

        [JsonProperty(PropertyName = "code_id")]
        public int CodeId { get; set; }
        [JsonProperty(PropertyName = "activation_code")]
        public string Code { get; set; }
        [JsonProperty(PropertyName = "issuer")]
        public string Issuer { get; set; } = "";
        [JsonProperty(PropertyName = "issuer_reference")]
        public string IssuerReference { get; set; } = "";
        [JsonProperty(PropertyName = "number_of_uses")]
        public int NumberOfUses { get; set; } = 1;
        [JsonProperty(PropertyName = "hotel_points")]
        public decimal HotelPoints { get; set; } = 0;
        [JsonProperty(PropertyName = "condo_rewards")]
        public int CondoRewards { get; set; } = 0;
        [JsonProperty(PropertyName = "charge_amount")]
        public decimal ChargeAmount { get; set; } = 0;
        [JsonProperty(PropertyName = "package_id")]
        public int PackageId { get; set; } = 0;
        [JsonProperty(PropertyName = "code_start_date")]
        public DateTime? CodeStartDate { get; set; } = null;
        [JsonProperty(PropertyName = "code_end_date")]
        public DateTime? CodeEndDate { get; set; } = null;
        [JsonProperty(PropertyName = "code_creation_date")]
        public DateTime CodeCreationDate { get; set; }
        [JsonProperty(PropertyName = "email_verified_date")]
        public DateTime? EmailVerifiedDate { get; set; } = null;
        [JsonProperty(PropertyName = "code_is_active")]
        public bool CodeIsActive { get; set; } = false;
        [JsonProperty(PropertyName = "activity_id")]
        public int CodeActivityId { get; set; } = 0;
        [JsonProperty(PropertyName = "first_name", Required = Required.Always)]
        public string FirstName { get; set; } = "";
        [JsonProperty(PropertyName = "middle_name")]
        public string MiddleName { get; set; } = "";
        [JsonProperty(PropertyName = "last_name", Required = Required.Always)]
        public string LastName { get; set; } = "";
        [JsonProperty(PropertyName = "address1")]
        public string Address1 { get; set; } = "";
        [JsonProperty(PropertyName = "address2")]
        public string Address2 { get; set; } = "";
        [JsonProperty(PropertyName = "city")]
        public string City { get; set; } = "";
        [JsonProperty(PropertyName = "state_code")]
        public string StateCode { get; set; } = "";
        [JsonProperty(PropertyName = "country_code")]
        public string CountryCode
        {
            get
            {
                return _country_code;
            }
            set
            {
                if (value != null && value.Length == 3)
                    _country_code = value;
            }
        }
        [JsonProperty(PropertyName = "postal_code")]
        public string PostalCode { get; set; } = "";
        [JsonProperty(PropertyName = "phone1")]
        public string Phone1
        {
            get
            {
                return _phone1;
            }
            set
            {
                if (value != null)
                {
                    _phone1 = new String(value.Where(Char.IsDigit).ToArray());
                }
            }
        }
        [JsonProperty(PropertyName = "phone2")]
        public string Phone2
        {
            get
            {
                return _phone2;
            }
            set
            {
                if (value != null)
                {
                    _phone2 = new String(value.Where(Char.IsDigit).ToArray());
                }
            }
        }
        [JsonProperty(PropertyName = "email", Required = Required.Always)]
        public string Email { get; set; } = "";
        [JsonProperty(PropertyName = "username")]
        public string Username { get; set; } = "";
        [JsonProperty(PropertyName = "rsi_id")]
        public int RSIId { get; set; } = 0;
        [JsonProperty(PropertyName = "status")]
        public string Status
        {
            get
            {
                if (RSIId > 0 && EmailVerifiedDate != null)
                    return "COMPLETE";

                else if (RSIId < 1 && EmailVerifiedDate != null)
                    return "ERROR";
                else if (FirstName != null && FirstName.Length > 0 && EmailVerifiedDate == null)
                    return "EMAILVERIFY";
                else
                    return "ACTIVE";

            }
        }
    }
}
