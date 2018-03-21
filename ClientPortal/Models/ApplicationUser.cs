using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace ClientPortal.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        private string _mobilePhone = "", _officePhone = "", _officeExt = "", _fax = "", _faxExt = "";

        public ApplicationUser()
            : base()
        {

        }

        public ApplicationUser(string companyName, string ein, string firstName, string middleName, string lastName, string address
            , string city, string state, string postalCode, string country, string mobilePhone, string officePhone
            , string officeExt, string fax, string faxExt, string email, int brokerId, int clientId, int agentId, string parentId, string creatorIP = "127.0.0.1")
        {
            CompanyName = companyName;
            FirstName = firstName;
            MiddleName = middleName;
            LastName = lastName;
            Address = address;
            City = city;
            State = state;
            PostalCode = postalCode;
            Country = country;
            Email = email;
            OfficePhone = officePhone;
            OfficeExtension = officeExt;
            MobilePhone = mobilePhone;
            Fax = fax;
            FaxExtension = faxExt;
            CreatorIP = creatorIP;
            CreationDate = DateTime.Now;
            EIN = ein;
            ParentId = parentId;
            BrokerId = brokerId;
            ClientId = clientId;
            AgentId = agentId;
        }
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
    }
}
