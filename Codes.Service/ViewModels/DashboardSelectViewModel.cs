using System.Collections.Generic;

namespace Codes.Service.ViewModels
{
    public class DashboardSelectViewModel
    {
        public string Role { get; set; }
        public IEnumerable<DashboardAccountViewModel> Accounts { get; set; }
    }

    public class DashboardAccountViewModel
    {
        public int AccountId { get; set; }
        public string Name { get; set; }
    }
}
