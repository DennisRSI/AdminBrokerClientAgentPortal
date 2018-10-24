using Codes.Service.Domain;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ClientPortal.Models._ViewModels
{
    public class AddUserViewModel
    {
        public string UserType { get; set; }
        public int BrokerId { get; set; }
        
        public IEnumerable<SelectListItem> Countries
        {
            get
            {
                return CountryList.GetSelectList();
            }
        }

        public IEnumerable<SelectListItem> Agents { get; set; }

        [DisplayFormat(DataFormatString = "{0:0.00}", ApplyFormatInEditMode = true)]
        public decimal CommissionRate { get; set; }
    }
}
