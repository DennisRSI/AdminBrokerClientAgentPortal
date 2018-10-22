using System.Collections.Generic;

namespace Codes.Service.ViewModels
{
    public class SearchViewModel
    {
        public string Query { get; set; }

        public bool ShowBroker { get; set; }
        public bool ShowAgent { get; set; }
        public bool ShowClient { get; set; }

        public IEnumerable<SearchUserViewModel> Agents { get; set; }
        public IEnumerable<SearchUserViewModel> Brokers { get; set; }
        public IEnumerable<SearchUserViewModel> Clients { get; set; }
        public IEnumerable<SearchCampaignViewModel> Campaigns { get; set; }
        public IEnumerable<SearchCardViewModel> Cards { get; set; } 
    }

    public class SearchUserViewModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string CompanyName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }

        public string FullName
        {
            get { return $"{FirstName} {LastName}"; }
        }
    }

    public class SearchCampaignViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }

    public class SearchCardViewModel
    {
        public int Id { get; set; }
        public string CardNumber { get; set; }
        public string Status { get; set; }
    }
}
