using Codes.Service.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace Codes.Service.Models
{
    [Table("Agents")]
    public class AgentModel : _BaseModel
    {
        private string _mobilePhone = "", _officePhone = "", _fax = "", _officeExtension = "", _faxExtension = "";

        public AgentModel()
        {

        }

        public AgentModel(AgentViewModel model)
        {
            AgentId = model.AgentId;
            CompanyName = model.CompanyName;
            AgentFirstName = model.AgentFirstName;
            AgentLastName = model.AgentLastName;
            EIN = model.EIN;
            Address = model.Address;
            City = model.City;
            State = model.State;
            PostalCode = model.PostalCode;
            Country = model.Country;
            Email = model.Email;
            OfficeExtension = model.OfficeExtension;
            OfficePhone = model.OfficePhone;
            Fax = model.Fax;
            FaxExtension = model.FaxExtension;
            MobilePhone = model.MobilePhone;
            DeactivationDate = model.DeactivationDate;
            DeactivationReason = model.DeactivationReason;
            IsActive = model.IsActive;
            CreationDate = model.CreationDate;
            CreatorIP = model.CreatorIP;
            BrokerId = model.Broker != null && model.Broker.BrokerId > 0 ? model.Broker.BrokerId : 0;
            ApplicationReference = model.ApplicationReference;
            AgentMiddleName = model.AgentMiddleName;
            CommissionRate = model.CommissionRate;
        }

        [Key, Required]
        public int AgentId { get; set; }
        [Required, StringLength(500)]
        public string CompanyName { get; set; }
        [StringLength(255)]
        public string AgentFirstName { get; set; }
        [StringLength(255)]
        public string AgentMiddleName { get; set; } = "";
        [StringLength(255)]
        public string AgentLastName { get; set; }
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
        [StringLength(450)]
        public string ApplicationReference { get; set; } = "";
        [Required]
        public int BrokerId { get; set; }
        public float CommissionRate { get; set; } = 0;
        [ForeignKey("BrokerId")]
        public BrokerModel Broker { get; set; }

        public int? DocumentW9Id { get; set; }

        [ForeignKey("DocumentW9Id")]
        public virtual DocumentModel DocumentW9 { get; set; }

        public int? DocumentOtherId { get; set; }

        [ForeignKey("DocumentOtherId")]
        public virtual DocumentModel DocumentOther { get; set; }

        public ICollection<CampaignAgentModel> CampaignAgents { get; set; }
    }
}
