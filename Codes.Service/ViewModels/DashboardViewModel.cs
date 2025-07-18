﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Codes1.Service.ViewModels
{
    public class DashboardViewModel
    {
        [DisplayFormat(DataFormatString = "{0:n0}")]
        public int PhysicalCardsPurchased { get; set; }

        [DisplayFormat(DataFormatString = "{0:n0}")]
        public int PhysicalCardsActivated { get; set; }

        [DisplayFormat(DataFormatString = "{0:n0}")]
        public int VirtualCardsGenerated { get; set; }

        [DisplayFormat(DataFormatString = "{0:n0}")]
        public int VirtualCardsActivated { get; set; }

        [DisplayFormat(DataFormatString = "{0:c}")]
        public decimal OverviewSavings { get; set; }

        [DisplayFormat(DataFormatString = "{0:c}")]
        public decimal OverviewCommisionsPaid { get; set; }

        [DisplayFormat(DataFormatString = "{0:c}")]
        public decimal OverviewCommisionsOwed { get; set; }

        [DisplayFormat(DataFormatString = "{0:c}")]
        public decimal HotelSavings { get; set; }

        [DisplayFormat(DataFormatString = "{0:c}")]
        public decimal HotelCommissionsPaid { get; set; }

        [DisplayFormat(DataFormatString = "{0:c}")]
        public decimal HotelCommissionsOwed { get; set; }

        [DisplayFormat(DataFormatString = "{0:c}")]
        public decimal CondoSavings { get; set; }

        [DisplayFormat(DataFormatString = "{0:c}")]
        public decimal CondoCommissionsPaid { get; set; }

        [DisplayFormat(DataFormatString = "{0:c}")]
        public decimal CondoCommissionsOwed { get; set; }

        [DisplayFormat(DataFormatString = "{0:c}")]
        public decimal TotalCommissionOwed { get; set; }

        [DisplayFormat(DataFormatString = "{0:c}")]
        public decimal ShoppingVolume { get; set; }

        [DisplayFormat(DataFormatString = "{0:c}")]
        public decimal ShoppingCashback { get; set; }

        [DisplayFormat(DataFormatString = "{0:c}")]
        public decimal ShoppingPendingCashback { get; set; }

        public string DistributionDetailType { get; set; }
        public string Role { get; set; }
        public int Id { get; set; }
        public bool IsSimulating { get; set; }
        public string AccountName { get; set; }
        public decimal CondoCompanyCommissionsOwed { get; set; } = 0;
        public decimal HotelCompanyCommissionsOwed { get; set; } = 0;
    }
}
