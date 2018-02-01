using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClientPortal.Models._ViewModels
{
    public class PageViewModel
    {
        public string Role { get; set; } = "";
        public int BrokerId { get; set; } = 0;
        public int ClientId { get; set; } = 0;
        public string AccountId { get; set; } = "";
    }
}
