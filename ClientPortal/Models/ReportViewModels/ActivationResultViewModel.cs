using System;
using System.Collections.Generic;

namespace ClientPortal.Models
{
    public class ActivationResultViewModel
    {
        public ActivationResultViewModel()
        {
            Tables = new List<ActivationTableViewModel>();
        }

        public string AccountName { get; set; }
        public string Type { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }

        public List<ActivationTableViewModel> Tables { get; set; }

        public string ReportTime
        {
            get
            {
                return DateTime.Now.ToString();
            }
        }
    }
}
