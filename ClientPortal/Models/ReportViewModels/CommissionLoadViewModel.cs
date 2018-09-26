using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace ClientPortal.Models
{
    public class CommissionLoadViewModel
    {
        public IEnumerable<SelectListItem> ReportType { get; set; }
        public IEnumerable<SelectListItem> Brokers { get; set; }
        public IEnumerable<SelectListItem> Clients { get; set; }
        public IEnumerable<SelectListItem> Agents { get; set; }
        public IEnumerable<SelectListItem> Campaigns { get; set; }
    }
}
