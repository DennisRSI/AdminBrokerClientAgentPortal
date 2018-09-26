using System;
using System.Collections.Generic;
using System.Text;

namespace Codes.Service.ViewModels
{
    public class ClientInfoViewModel
    {
        public int ClientId { get; set; }
        public int BrokerId { get; set; }
        public string CompanyName { get; set; }
        public string BrokerName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }
        public string FullAddress {
            get
            {
                return $"{Address}, {City}, {State} {PostalCode} {Country}";
            }
        }
        public DateTime CreationDate { get; set; }
        public string FirstName { get; set; } = "";
        public string MiddleName { get; set; } = "";
        public string LastName { get; set; } = "";
        public string FullName
        {
            get
            {
                string fullName = "";

                if (FirstName != null && FirstName.Length > 0)
                    fullName = FirstName;

                if(MiddleName != null && MiddleName.Length > 0)
                {
                    if (fullName.Length > 0)
                        fullName += " ";

                    fullName += MiddleName;
                }

                if(LastName != null && LastName.Length > 0)
                {
                    if (fullName.Length > 0)
                        fullName += " ";

                    fullName += LastName;
                }

                return fullName;
            }
        }
        public string OfficePhone { get; set; } = "";
        public string OfficeExtension { get; set; } = "";
        public string OfficePhoneFormatted
        {
            get
            {
                string officePhone = "";

                if (OfficePhone != null && OfficePhone.Length > 0)
                {
                    officePhone = String.Format("{0:(###) ###-####}", OfficePhone);

                    if (OfficeExtension != null && OfficeExtension.Length > 0)
                        officePhone += $" ext: {OfficeExtension}";
                }

                return officePhone;
            }
        }
        public string MobilePhone { get; set; } = "";
        public string MobilePhoneFormatted
        {
            get
            {
                string phone = "";

                if(MobilePhone != null && MobilePhone.Length > 0)
                    phone = String.Format("{0:(###) ###-####}", MobilePhone);

                return phone;
            }
        }
        public string Email { get; set; } = "";
        public string AgentFullName { get; set; }
        public int CampaignCount { get; set; } = 0;
        public string ClientNotes { get; set; } = "";
        public string Message { get; set; } = "";
        public bool IsSuccess { get { return Message.ToUpper().IndexOf("ERROR") == -1 ? true : false; } }
    }
}
