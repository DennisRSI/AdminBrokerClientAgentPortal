using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Codes.Service.ViewModels
{
    public class ClientViewModel : _BaseViewModel
    {
        private string _mobilePhone = "", _officePhone = "", _fax = "", _officeExtension = "", _faxExtension = "";

        [Display(Name = "Client Id", Prompt = "Client Id")]
        public int ClientId { get; set; } = 0;
        [StringLength(500), Display(Name = "Company", Prompt = "Company")]
        public string CompanyName { get; set; }
        [StringLength(255), Display(Name = "Contact First Name", Prompt = "Contact First Name")]
        public string ContactFirstName { get; set; }
        [StringLength(255)]
        public string ContactMiddleName { get; set; } = "";
        [StringLength(255), Display(Name = "Contact Last Name", Prompt = "Contact Last Name")]
        public string ContactLastName { get; set; }
        [StringLength(500), Display(Name = "EIN/FEIN", Prompt = "EIN/FEIN")]
        public string EIN { get; set; } = "";
        [StringLength(255), Display(Name = "Office Address", Prompt = "Office Address")]
        public string Address { get; set; } = "";
        [StringLength(100), Display(Name = "City", Prompt = "City")]
        public string City { get; set; } = "";
        [StringLength(100), Display(Name = "State", Prompt = "State")]
        public string State { get; set; } = "";
        [StringLength(50), Display(Name = "Postal Code", Prompt = "Postal Code")]
        public string PostalCode { get; set; }
        [StringLength(100), Display(Name = "Country", Prompt = "Country")]
        public string Country { get; set; }
        [StringLength(50), Display(Name = "Mobile Phone", Prompt = "Mobile Phone")]
        public string MobilePhone
        {
            get
            {
                return _mobilePhone;
            }
            set
            {
                if (value != null)
                {

                    _mobilePhone = new String(value.Where(Char.IsDigit).ToArray());
                }
            }
        }
        [StringLength(50), Display(Name = "Office Phone", Prompt = "Office Phone")]
        public string OfficePhone
        {
            get
            {
                return _officePhone;
            }
            set
            {
                if (value != null)
                {

                    _officePhone = new String(value.Where(Char.IsDigit).ToArray());
                }
            }
        }
        [StringLength(50), Display(Name = "Ext", Prompt = "Ext")]
        public string OfficeExtension
        {
            get
            {
                return _officeExtension;
            }
            set
            {
                if (value != null)
                {

                    _officeExtension = new String(value.Where(Char.IsDigit).ToArray());
                }
            }
        }
        [StringLength(50), Display(Name = "Office Fax", Prompt = "Office Fax")]
        public string Fax
        {
            get
            {
                return _fax;
            }
            set
            {
                if (value != null)
                {

                    _fax = new String(value.Where(Char.IsDigit).ToArray());
                }
            }
        }

        [StringLength(50), Display(Name = "Ext", Prompt = "Ext")]
        public string FaxExtension
        {
            get
            {
                return _faxExtension;
            }
            set
            {
                if (value != null)
                {

                    _faxExtension = new String(value.Where(Char.IsDigit).ToArray());
                }
            }
        }
        [StringLength(100), EmailAddress, Display(Name = "Contact Email", Prompt = "Contact Email")]
        public string Email { get; set; }
        [Display(Name = "Deactivation Reason", Prompt = "Deactivation Reason")]
        public string DeactivationReason { get; set; } = null;
        [Required]
        public float CommissionRate { get; set; } = 0;
        [StringLength(450)]
        public string ApplicationReference { get; set; } = "";
        [Display(Name = "Broker", Prompt = "Broker")]
        public BrokerViewModel Broker { get; set; } = new BrokerViewModel();
    }
}
