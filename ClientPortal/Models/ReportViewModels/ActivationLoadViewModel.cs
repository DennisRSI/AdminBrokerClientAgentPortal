using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace ClientPortal.Models
{
    public class ActivationLoadViewModel
    {
        public ActivationLoadViewModel()
        {
            Clients = new List<SelectListItem>();
            Agents = new List<SelectListItem>();
        }

        public IEnumerable<SelectListItem> Clients { get; set; }
        public IEnumerable<SelectListItem> Agents { get; set; }
    }
}
