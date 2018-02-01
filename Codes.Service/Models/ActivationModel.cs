using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Codes.Service.Models
{
    public class ActivationModel : _BaseModel
    {
        private string _phone1 = "", _phone2 = "", _country_code = "USA";

        [Required]
        [StringLength(500)]
        [Display(Name = "Activation Code")]
        [JsonProperty(PropertyName = "activation_code")]
        public string ActivationCode { get; set; } = "";
        [Required]
        [StringLength(50)]
        [JsonProperty(PropertyName = "issuer")]
        [Display(Name = "Issuer")]
        public string Issuer { get; set; } = "";
        [Required]
        [Display(Name = "First Name")]
        [StringLength(255)]
        [JsonProperty(PropertyName = "first_name", Required = Required.Always)]
        public string FirstName { get; set; } = "";
        [StringLength(255)]
        [Display(Name = "Middle Name")]
        [JsonProperty(PropertyName = "middle_name")]
        public string MiddleName { get; set; } = "";
        [Required]
        [StringLength(255)]
        [Display(Name = "Last Name")]
        [JsonProperty(PropertyName = "last_name", Required = Required.Always)]
        public string LastName { get; set; } = "";
        [Display(Name = "Address 1")]
        [JsonProperty(PropertyName = "address1")]
        [StringLength(255)]
        public string Address1 { get; set; } = "";
        [StringLength(255)]
        [Display(Name = "Address 2")]
        [JsonProperty(PropertyName = "address2")]
        public string Address2 { get; set; } = "";
        [StringLength(100)]
        [Display(Name = "City")]
        [JsonProperty(PropertyName = "city")]
        public string City { get; set; } = "";
        [StringLength(50)]
        [Display(Name = "State Code")]
        [JsonProperty(PropertyName = "state_code")]
        public string StateCode { get; set; } = "";
        [StringLength(50)]
        [Display(Name = "Country Code")]
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
        [StringLength(50)]
        [Display(Name = "Postal Code")]
        [JsonProperty(PropertyName = "postal_code")]
        public string PostalCode { get; set; } = "";
        [StringLength(25)]
        [Display(Name = "Phone 1")]
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
                    //_phone1 = new String(value.Where(Char.IsDigit).ToArray());
                    _phone1 = new String(value.Where(Char.IsDigit).ToArray());
                }
            }
        }
        [StringLength(25)]
        [Display(Name = "Phone 2")]
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
        [Required]
        [Display(Name = "Email")]
        [StringLength(100)]
        [JsonProperty(PropertyName = "email", Required = Required.Always)]
        [EmailAddress]
        public string Email { get; set; } = "";
        [Required]
        [StringLength(100)]
        [Display(Name = "Username")]
        [JsonProperty(PropertyName = "username")]
        public string Username { get; set; } = "";
        [Required]
        [StringLength(100)]
        [Display(Name = "Password")]
        [JsonProperty(PropertyName = "password")]
        public string Password { get; set; } = "";
    }
}
