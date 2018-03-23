using Codes.Service.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace Codes.Service.Models
{
    [Table("Brokers")]
    public class BrokerModel : _BaseModel
    {
        private string _mobilePhone = "", _officePhone = "", _fax = "", _officeExtension = "", _faxExtension = "";

        public BrokerModel(BrokerViewModel model)
        {
            BrokerId = model.BrokerId;
            ParentBrokerId = model.ParentBrokerId;
            CompanyName = model.CompanyName;
            BrokerFirstName = model.BrokerFirstName;
            BrokerLastName = model.BrokerLastName;
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
            DeactivationReason = model.DeactivationReason;
            Email = model.Email;
            BrokerCommissionPercentage = model.BrokerCommissionPercentage;
            AgentCommissionPercentage = model.AgentCommissionPercentage;
            ClientCommissionPercentage = model.ClientCommissionPercentage;
            PhysicalCardsPercentOfFaceValue1000 = model.PhysicalCardsPercentOfFaceValue1000;
            PhysicalCardsPercentOfFaceValue5000 = model.PhysicalCardsPercentOfFaceValue5000;
            PhysicalCardsPercentOfFaceValue10000 = model.PhysicalCardsPercentOfFaceValue10000;
            PhysicalCardsPercentOfFaceValue25000 = model.PhysicalCardsPercentOfFaceValue25000;
            PhysicalCardsPercentOfFaceValue50000 = model.PhysicalCardsPercentOfFaceValue50000;
            PhysicalCardsPercentOfFaceValue100000 = model.PhysicalCardsPercentOfFaceValue100000;
            VirtualCardCap = model.VirtualCardCap;
            TimeframeBetweenCapInHours = model.TimeframeBetweenCapInHours;
            ApplicationReference = model.ApplicationReference;
            IsActive = model.IsActive;
            CreationDate = model.CreationDate;
            CreatorIP = model.CreatorIP;
            DeactivationDate = model.DeactivationDate;
            BrokerMiddleName = model.BrokerMiddleName;
        }

        public BrokerModel()
        {

        }

        [Key, Required]
        public int BrokerId { get; set; }
        [Required]
        public int ParentBrokerId { get; set; } = 0;
        [StringLength(500)]
        public string CompanyName { get; set; }
        [StringLength(255)]
        public string BrokerFirstName { get; set; }
        [StringLength(255)]
        public string BrokerMiddleName { get; set; } = "";
        [StringLength(255)]
        public string BrokerLastName { get; set; }
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
        public float BrokerCommissionPercentage { get; set; } = 0;
        [Required]
        public float AgentCommissionPercentage { get; set; } = 0;
        [Required]
        public float ClientCommissionPercentage { get; set; } = 0;
        [Required]
        public float PhysicalCardsPercentOfFaceValue1000 { get; set; } = 0;
        [Required]
        public float PhysicalCardsPercentOfFaceValue5000 { get; set; } = 0;
        [Required]
        public float PhysicalCardsPercentOfFaceValue10000 { get; set; } = 0;
        [Required]
        public float PhysicalCardsPercentOfFaceValue25000 { get; set; } = 0;
        [Required]
        public float PhysicalCardsPercentOfFaceValue50000 { get; set; } = 0;
        [Required]
        public float PhysicalCardsPercentOfFaceValue100000 { get; set; } = 0;
        [Required]
        public int VirtualCardCap { get; set; } = 25000;
        [Required]
        public int TimeframeBetweenCapInHours { get; set; } = 24;
        [StringLength(450)]
        public string ApplicationReference { get; set; } = "";

        public int? DocumentW9Id { get; set; }

        [ForeignKey("DocumentW9Id")]
        public virtual DocumentModel DocumentW9 { get; set; }

        public int? DocumentOtherId { get; set; }

        [ForeignKey("DocumentOtherId")]
        public virtual DocumentModel DocumentOther { get; set; }

        public ICollection<AgentModel> Agents { get; set; }
        public ICollection<ClientModel> Clients { get; set; }
        public ICollection<CampaignModel> Campaigns { get; set; }
        public ICollection<PendingCodeModel> PendingCodes { get; set; }
        public ICollection<UnusedCodeModel> UnusedCodes { get; set; }
        public ICollection<UsedCodeModel> UsedCodes { get; set; }
        public ICollection<DocumentModel> Documents { get; set; }
    }
}
