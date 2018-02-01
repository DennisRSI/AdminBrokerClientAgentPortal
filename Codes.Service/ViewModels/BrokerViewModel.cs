using Codes.Service.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Codes.Service.ViewModels
{
    public class BrokerViewModel : _BaseViewModel
    {
        private string _mobilePhone = "", _officePhone = "", _fax = "", _officeExtension = "", _faxExtension = "";

        [Display(Name = "Broker Id", Prompt = "Broker Id")]
        public int BrokerId { get; set; } = 0;
        [StringLength(500), Display(Name = "Company", Prompt = "Company")]
        public string CompanyName { get; set; }
        [StringLength(255), Display(Name = "Broker First Name", Prompt = "Broker First Name")]
        public string BrokerFirstName { get; set; }
        [StringLength(255)]
        public string BrokerMiddleName { get; set; } = "";
        [StringLength(255), Display(Name = "Broker Last Name", Prompt = "Broker Last Name")]
        public string BrokerLastName { get; set; }
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
        [StringLength(100), EmailAddress, Display(Name = "Broker Email", Prompt = "Broker Email")]
        public string Email { get; set; }
        [Required, Display(Name = "Broker Commission Percentage", Prompt = "Broker Commission Percentage")]
        public float BrokerCommissionPercentage { get; set; } = 0;
        [Required, Display(Name = "Agent Commission Percentage", Prompt = "Agent Commission Percentage")]
        public float AgentCommissionPercentage { get; set; } = 0;
        [Required, Display(Name = "Client Commission Percentage", Prompt = "Client Commission Percentage")]
        public float ClientCommissionPercentage { get; set; } = 0;
        [Required, Display(Name = "1000 Cards", Prompt = "1000 Cards")]
        public float PhysicalCardsPercentOfFaceValue1000 { get; set; } = 0;
        [Required, Display(Name = "5000 Cards", Prompt = "5000 Cards")]
        public float PhysicalCardsPercentOfFaceValue5000 { get; set; } = 0;
        [Required, Display(Name = "10000 Cards", Prompt = "10000 Cards")]
        public float PhysicalCardsPercentOfFaceValue10000 { get; set; } = 0;
        [Required, Display(Name = "25000 Cards", Prompt = "25000 Cards")]
        public float PhysicalCardsPercentOfFaceValue25000 { get; set; } = 0;
        [Required, Display(Name = "50000 Cards", Prompt = "50000 Cards")]
        public float PhysicalCardsPercentOfFaceValue50000 { get; set; } = 0;
        [Required, Display(Name = "100000 Cards", Prompt = "100000 Cards")]
        public float PhysicalCardsPercentOfFaceValue100000 { get; set; } = 0;
        [Required, Display(Name = "Virtual Card Cap", Prompt = "Virtual Card Cap")]
        public int VirtualCardCap { get; set; } = 25000;
        [Required, Display(Name = "Timeframe", Prompt = "Timeframe")]
        public int TimeframeBetweenCapInHours { get; set; } = 24;
        [Display(Name = "Deactivation Reason", Prompt = "Deactivation Reason")]
        public string DeactivationReason { get; set; } = "";
        [StringLength(450)]
        public string ApplicationReference { get; set; } = "";
        [Display(Name = "Parent Broker", Prompt = "Parent Broker")]
        public int ParentBrokerId { get; set; } = 0;
        [Display(Name = "Agents", Prompt = "Agents")]
        public ICollection<AgentViewModel> Agents { get; set; } = new List<AgentViewModel>();
        //[Display(Name = "Clients", Prompt = "Clients")]
        //public ICollection<ClientModel> Clients { get; set; } = null;
        //[Display(Name = "Campaigns", Prompt = "Campaigns")]
        //public ICollection<CampaignModel> Campaigns { get; set; } = null;

    }
}
