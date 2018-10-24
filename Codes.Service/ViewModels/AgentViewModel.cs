using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Codes.Service.ViewModels
{
    public class AgentViewModel : _BaseViewModel
    {
        private string _mobilePhone = "", _officePhone = "", _fax = "", _officeExtension = "", _faxExtension = "";

        [Display(Name = "Agent Id", Prompt = "Agent Id")]
        public int AgentId { get; set; } = 0;

        [Display(Name = "Broker Id", Prompt = "Broker Id")]
        public int BrokerId { get; set; }

        [StringLength(500), Display(Name = "Company", Prompt = "Company")]
        public string CompanyName { get; set; }

        [StringLength(255), Display(Name = "Agent First Name", Prompt = "Agent First Name")]
        public string AgentFirstName { get; set; }

        [StringLength(255)]
        public string AgentMiddleName { get; set; } = "";

        [StringLength(255), Display(Name = "Agent Last Name", Prompt = "Agent Last Name")]
        public string AgentLastName { get; set; }

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
        [StringLength(100), EmailAddress, Display(Name = "Agent Email", Prompt = "Agent Email")]
        public string Email { get; set; }
        [Display(Name = "Deactivation Reason", Prompt = "Deactivation Reason")]
        public string DeactivationReason { get; set; } = "";
        [StringLength(450)]
        public string ApplicationReference { get; set; } = "";
        [Display(Name = "Broker", Prompt = "Broker")]
        public BrokerViewModel Broker { get; set; } = new BrokerViewModel();
        [Display(Name = "Commission Rate", Prompt = "Commission Rate")]
        public float CommissionRate { get; set; } = 0;

        public int ParentAgentId { get; set; }
        public string ParentAgentName { get; set; }
    }
}
