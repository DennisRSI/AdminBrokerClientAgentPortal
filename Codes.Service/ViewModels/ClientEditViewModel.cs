﻿using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace Codes.Service.ViewModels
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
        public string ContactPhone { get; set; }
        public string OfficeFax { get; set; }
        public string Email { get; set; }
        public string CommissionRate { get; set; }

        public string UserName
        {
            get { return Email; }
        }

        public IEnumerable<SelectListItem> Agents { get; set; }
    }
}

