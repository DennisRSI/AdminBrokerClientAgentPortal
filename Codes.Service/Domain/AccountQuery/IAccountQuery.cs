using Codes1.Service.ViewModels;
using System.Collections.Generic;

namespace Codes1.Service.Domain
{
    public interface IAccountQuery
    {
        IEnumerable<AccountViewModel> GetBrokers();
        IEnumerable<AccountViewModel> GetAgents();
        IEnumerable<AccountViewModel> GetClients();
        IEnumerable<AccountViewModel> GetCampaigns();
    }
}
