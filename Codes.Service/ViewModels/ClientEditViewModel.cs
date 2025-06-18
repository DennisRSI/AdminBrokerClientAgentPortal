using Codes1.Service.Domain;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Codes1.Service.ViewModels
{
    public class ClientEditViewModel
    {
        public string ApplicationReference { get; set; }
        public int ClientId { get; set; }
        public string CompanyName { get; set; }
        public string EIN { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MobilePhone { get; set; }
        public string OfficePhone { get; set; }
        public string Fax { get; set; }
        public string Email { get; set; }
        public string CommissionRate { get; set; }
        public int AgentId { get; set; }
        public DateTime? DeactivationDate { get; set; }
        public string ErrorMessage { get; set; }

        public string UserName
        {
            get { return Email; }
        }

        public IEnumerable<SelectListItem> Agents { get; set; }

        public IEnumerable<AgentViewModel> AssignedAgents { get; set; }

        public IEnumerable<SelectListItem> Countries
        {
            get
            {
                return CountryList.GetSelectList();
            }
        }
    }
}
