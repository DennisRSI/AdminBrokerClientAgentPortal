using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Codes.Service.ViewModels
{
    public class AdminViewModel : _BaseViewModel
    {
        private string _mobilePhone = "", _officePhone = "", _fax = "", _officeExtension = "", _faxExtension = "";
        [StringLength(100)]
        public string CompanyName { get; set; }
        [StringLength(450)]
        public string ApplicationReference { get; set; }
        [StringLength(256)]
        public string FirstName { get; set; } = "";
        [StringLength(256)]
        public string MiddleName { get; set; } = "";
        [StringLength(256)]
        public string LastName { get; set; } = "";
        [StringLength(256)]
        public string Address { get; set; }
        [StringLength(100)]
        public string City { get; set; }
        [StringLength(100)]
        public string State { get; set; }
        [StringLength(50)]
        public string PostalCode { get; set; }
        [StringLength(100)]
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
        public string DeactivationReason { get; set; } = "";
        [StringLength(100)]
        public string EIN { get; set; }
    }
}
