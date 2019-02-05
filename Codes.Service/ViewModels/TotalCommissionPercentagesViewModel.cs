using System;
using System.Collections.Generic;
using System.Text;

namespace Codes.Service.ViewModels
{
    public class TotalCommissionPercentagesViewModel
    {
        public float TotalCommissionPercentage { get; set; } = 0;
        public float TotalCommissionUsed { get { return TotalCommissionPercentage - TotalAgentCommissionPercentage - TotalClientCommissionPercentage; } }
        public float TotalAgentCommissionPercentage { get; set; } = 0;
        public float TotalClientCommissionPercentage { get; set; } = 0;
        public float TotalBrokerCommissionPercentage { get { return TotalClientCommissionPercentage - TotalCommissionUsed; } }
        public string Message { get; set; } = "";
        public bool IsSuccess { get; set; } = false;
    }
}
