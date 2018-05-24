using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Microsoft.AspNetCore.Identity;

namespace ClientPortal.Models
{
    public class ApplicationUser : IdentityUser
    {
        private string _mobilePhone = "", _officePhone = "", _officeExt = "", _fax = "", _faxExt = "";

        [StringLength(450)]
        public string ParentId { get; set; } = "";

        [StringLength(256)]
        public string FirstName { get; set; }

        [StringLength(256)]
        public string MiddleName { get; set; }

        [StringLength(256)]
        public string LastName { get; set; }

        [StringLength(500)]
        public string Address { get; set; }

        [StringLength(100)]
        public string City { get; set; }

        [StringLength(100)]
        public string State { get; set; }

        [StringLength(50)]
        public string PostalCode { get; set; }

        [StringLength(100)]
        public string Country { get; set; }

        [StringLength(50)]
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

        [StringLength(50)]
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
                    PhoneNumber = "+" + _officePhone;
                }
            }
        }

        [StringLength(50)]
        public string OfficeExtension
        {
            get
            {
                return _officeExt;
            }

            set
            {
                if (value != null)
                {

                    _officeExt = new String(value.Where(Char.IsDigit).ToArray());
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
                return _faxExt;
            }

            set
            {
                if (value != null)
                {

                    _faxExt = new String(value.Where(Char.IsDigit).ToArray());
                }
            }
        }

        public DateTime CreationDate { get; set; } = new DateTime();

        [StringLength(50)]
        public string CreatorIP { get; set; } = "127.0.0.1";

        public DateTime? DeactivationDate { get; set; } = null;

        public string DeactivationReason { get; set; } = "";

        [StringLength(100)]
        public string CompanyName { get; set; }

        [StringLength(500)]
        public string EIN { get; set; }

        public int ClientId { get; set; } = 0;

        public int AgentId { get; set; } = 0;

        public int BrokerId { get; set; } = 0;

        public bool IsAdmin
        {
            get
            {
                return AgentId == 0 && BrokerId == 0 && ClientId == 0;
            }
        }

        public string Role
        {
            get
            {
                if (AgentId > 0) { return "Agent"; }
                if (ClientId > 0) { return "Client"; }
                if (BrokerId > 0) { return "Broker"; }

                return "Administrator";
            }
        }
    }
}
