﻿using System.Collections.Generic;

namespace Codes1.Service.ViewModels
{
    public class AccountCommonModel
    {
        public decimal CommissionRate { get; set; }

        // These only exist for brokers
        public decimal ClientCommissionRate { get; set; }
        public decimal AgentCommissionRate { get; set; }
    }
}
