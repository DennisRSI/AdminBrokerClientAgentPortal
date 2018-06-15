using Codes.Service.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Codes.Service.Models
{
    [Table("Clients")]
    public class ClientModel : _BaseModel
    {
        private string _mobilePhone = "", _officePhone = "", _fax = "", _officeExtension = "", _faxExtension = "";

        public ClientModel()
        {
        }

        public ClientModel(ClientViewModel model)
        {
            ClientId = model.ClientId;
            CompanyName = model.CompanyName;
            ContactFirstName = model.ContactFirstName;
            ContactLastName = model.ContactLastName;
            EIN = model.EIN;
            Address = model.Address;
            City = model.City;
            State = model.State;
            PostalCode = model.PostalCode;
            Country = model.Country;
            MobilePhone = model.MobilePhone;
            OfficeExtension = model.OfficeExtension;
            OfficePhone = model.OfficePhone;
            Fax = model.Fax;
            FaxExtension = model.FaxExtension;
            Email = model.Email;
            IsActive = model.IsActive;
            CreationDate = model.CreationDate;
            DeactivationDate = model.DeactivationDate;
            CreatorIP = model.CreatorIP;
            DeactivationReason = model.DeactivationReason;
            BrokerId = model.Broker != null && model.Broker.BrokerId > 0 ? model.Broker.BrokerId : 0;
            CommissionRate = model.CommissionRate;
            ApplicationReference = model.ApplicationReference;
            ContactMiddleName = model.ContactMiddleName;
        }

        [Key, Required]
        public int ClientId { get; set; }

        [Required, StringLength(500)]
        public string CompanyName { get; set; }

        [StringLength(255)]
        public string ContactFirstName { get; set; }

        [StringLength(255)]
        public string ContactMiddleName { get; set; } = "";

        [StringLength(255)]
        public string ContactLastName { get; set; }

        [StringLength(500)]
        public string EIN { get; set; } = "";

        [StringLength(255)]
        public string Address { get; set; } = "";

        [StringLength(100)]
        public string City { get; set; } = "";

        [StringLength(100)]
        public string State { get; set; } = "";

        [StringLength(50)]
        public string PostalCode { get; set; }

        [Required, StringLength(100)]
        public string Country { get; set; } = "USA";

        [StringLength(50), Phone]
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

        [StringLength(50), Phone]
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

        [StringLength(50)]
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

        [StringLength(50)]
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

        [StringLength(50)]
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

        [StringLength(100), EmailAddress]
        public string Email { get; set; }

        public string DeactivationReason { get; set; } = null;

        [Required]
        public float CommissionRate { get; set; } = 0;

        [StringLength(450)]
        public string ApplicationReference { get; set; } = "";

        [Required]
        public int BrokerId { get; set; }

        [ForeignKey("BrokerId")]
        public BrokerModel Broker { get; set; }

        public int? AgentId { get; set; }

        [ForeignKey("AgentId")]
        public AgentModel Agent { get; set; }

        public ICollection<CampaignModel> Campaigns { get; set; }
    }
}
