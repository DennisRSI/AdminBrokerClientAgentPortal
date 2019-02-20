using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace ClientPortal.Models._ViewModels
{
    public class ProfileViewModel : ApplicationUser
    {
        public int? DocumentW9Id { get; set; }
        public decimal CommissionRate { get; set; }
        public bool IsDisabled { get; set; } = true;
        public int? ParentAgentId { get; set; }
        public string ParentAgentName { get; set; }
        public IEnumerable<SelectListItem> Agents { get; set; }
    }
}
