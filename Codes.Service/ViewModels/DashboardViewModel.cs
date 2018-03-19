using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Codes.Service.ViewModels
{
    public class DashboardViewModel
    {
        [DisplayFormat(DataFormatString = "{0:n0}")]
        public int PhysicalCardsPurchased { get; set; }

        [DisplayFormat(DataFormatString = "{0:n0}")]
        public int PhysicalCardsInCampaigns { get; set; }

        [DisplayFormat(DataFormatString = "{0:n0}")]
        public int PhysicalCardsActivated { get; set; }

        [DisplayFormat(DataFormatString = "{0:n0}")]
        public int VirtualCardsGenerated { get; set; }

        [DisplayFormat(DataFormatString = "{0:n0}")]
        public int VirtualCardsInCampaigns { get; set; }

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
        public decimal ShoppingVolume { get; set; }

        [DisplayFormat(DataFormatString = "{0:c}")]
        public decimal ShoppingCashback { get; set; }

        [DisplayFormat(DataFormatString = "{0:c}")]
        public decimal ShoppingPendingCashback { get; set; }

        public string DistributionDetailType { get; set; }

        public IEnumerable<CardDistributionViewModel> CardDistributions { get; set; } = new List<CardDistributionViewModel>();
    }

    public class CardDistributionViewModel
    {
        public string Name { get; set; }

        [DisplayFormat(DataFormatString = "{0:n0}")]
        public int PhysicalCardsTotal { get; set; }

        [DisplayFormat(DataFormatString = "{0:n0}")]
        public int PhysicalCardsActivated { get; set; }

        [DisplayFormat(DataFormatString = "{0:n0}")]
        public int VirtualCardsTotal { get; set; }

        [DisplayFormat(DataFormatString = "{0:n0}")]
        public int VirtualCardsActivated { get; set; }

        [DisplayFormat(DataFormatString = "{0:p1}")]
        public decimal PhysicalCardsActivationRate
        {
            get
            {
                if (PhysicalCardsTotal == 0)
                {
                    return 0;
                }

                return (decimal)PhysicalCardsActivated / PhysicalCardsTotal;
            }
        }

        [DisplayFormat(DataFormatString = "{0:p1}")]
        public decimal VirtualCardsActivationRate
        {
            get
            {
                if (VirtualCardsTotal == 0)
                {
                    return 0;
                }

                return (decimal)VirtualCardsActivated / VirtualCardsTotal;
            }
        }
    }
}
