using System;
using System.Collections.Generic;
using System.Text;

namespace Excel.Service.Models._ViewModel
{
    public class AdjustmentViewModel
    {
        public AdjustmentViewModel()
        {

        }

        public string Confirmation { get; set; } = "";
        public string Property { get; set; } = "";
        public string Guest { get; set; } = "";
        public decimal CommissionAdjustment { get; set; } = 0;
        public string Notes { get; set; } = "";
    }
}
