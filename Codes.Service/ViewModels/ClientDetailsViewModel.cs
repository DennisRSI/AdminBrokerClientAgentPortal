using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Codes1.Service.ViewModels
{
    public class ClientDetailsViewModel
    {
        public string ApplicationReference { get; set; }
        public int ClientId { get; set; }
        public string CompanyName { get; set; }

        public bool ShowEditButton { get; set; }
        public bool ShowAddCampaignButton { get; set; }

        [DisplayFormat(DataFormatString = "{0:n0}")]
        public int PhysicalInCampaigns { get; set; }

        [DisplayFormat(DataFormatString = "{0:n0}")]
        public int VirtualInCampaigns { get; set; }

        public IEnumerable<ClientDetailsCampaignViewModel> Campaigns { get; set; }
    }

    public class ClientDetailsCampaignViewModel
    {
        public int CampaignId { get; set; }
        public string CampaignName { get; set; }
        public int CardQuantity { get; set; }
        public string CardType { get; set; }
        public string Benefits { get; set; }
        public string Status { get; set; }
    }
}
