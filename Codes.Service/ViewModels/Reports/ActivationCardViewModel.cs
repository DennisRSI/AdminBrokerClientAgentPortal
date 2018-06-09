using System;

namespace Codes.Service.ViewModels
{
    public class ActivationCardViewModel : _BaseViewModel
    {
        public string CardNumber { get; set; }
        public DateTime ActivationDate { get; set; }
        public string MemberName { get; set; }
        public Single Denomination { get; set; }
        public string CardType { get; set; }
        public string IsCardUsed { get; set; }
        public string CampaignName { get; set; }
        public string CardStatus { get; set; }
    }
}
