using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClientPortal.Models._ViewModels
{
    public class BrokerAdminFilter
    {
        public  DateTime StartDate { get; set; }
        public  DateTime EndDate { get; set; }
        public int StartRowIndex { get; set; } = 0;
        public int NumberOfRows { get; set; } = 0;
        public bool IsPaid { get; set; } = false;
        public string BookingType { get; set; } = "";
        public int BrokerId { get; set; } = 0;
    }
}
