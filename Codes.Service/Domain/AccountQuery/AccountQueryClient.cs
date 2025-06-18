using Codes1.Service.Data;
using Codes1.Service.ViewModels;
using System.Collections.Generic;
using System.Linq;

namespace Codes1.Service.Domain
{
    public class AccountQueryClient : IAccountQuery
    {
        private readonly int _clientId;
        private readonly Codes1DbContext _context;

        public AccountQueryClient(Codes1DbContext context, int clientId)
        {
            _context = context;
            _clientId = clientId;
        }

        public IEnumerable<AccountViewModel> GetAgents()
        {
            return Enumerable.Empty<AccountViewModel>();
        }

        public IEnumerable<AccountViewModel> GetBrokers()
        {
            return Enumerable.Empty<AccountViewModel>();
        }

        public IEnumerable<AccountViewModel> GetCampaigns()
        {
            return _context.Campaigns.Where(c => c.ClientId == _clientId)
                .Select(c => new AccountViewModel() { Id = c.CampaignId, CompanyName = c.CampaignName });
        }

        public IEnumerable<AccountViewModel> GetClients()
        {
            return Enumerable.Empty<AccountViewModel>();
        }
    }
}
