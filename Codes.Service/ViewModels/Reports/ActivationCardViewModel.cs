using System;

namespace Codes.Service.ViewModels
{
    public class ActivationCardViewModel : _BaseViewModel
    {
        public string CardNumber { get; set; }
        public DateTime ActivationDate { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Single Denomination { get; set; }
        public string CardType { get; set; }
        public string IsCardUsed { get; set; }
        public string CampaignName { get; set; }
        public string CardStatus { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string PostalCode { get; set; }
        public int LoginCount { get; set; }

        public string MemberName
        {
            get { return $"{FirstName} {LastName}"; }
        }
    }
}
