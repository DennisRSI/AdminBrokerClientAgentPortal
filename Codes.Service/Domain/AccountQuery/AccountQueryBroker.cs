using Codes1.Service.Data;
using Codes1.Service.ViewModels;
using System.Collections.Generic;
using System.Linq;

namespace Codes1.Service.Domain
{
    public class AccountQueryBroker : IAccountQuery
    {
        private readonly int _brokerId;
        private readonly Codes1DbContext _context;

        public AccountQueryBroker(Codes1DbContext context, int brokerId)
        {
            _context = context;
            _brokerId = brokerId;
        }

        public IEnumerable<AccountViewModel> GetAgents()
        {
            return _context.Agents.Where(a => a.BrokerId == _brokerId)
                    .Select(a => new AccountViewModel() { Id = a.AgentId, FirstName = a.AgentFirstName, LastName = a.AgentLastName, CompanyName = a.CompanyName });
        }

        public IEnumerable<AccountViewModel> GetBrokers()
        {
            return Enumerable.Empty<AccountViewModel>();
        }

        public IEnumerable<AccountViewModel> GetCampaigns()
        {
            return _context.Campaigns.Where(c => c.BrokerId == _brokerId)
                .Select(c => new AccountViewModel() { Id = c.CampaignId, CompanyName = c.CampaignName });
        }

        public IEnumerable<AccountViewModel> GetClients()
        {
            return _context.Clients.Where(c => c.BrokerId == _brokerId)
                    .Select(c => new AccountViewModel() { Id = c.ClientId, FirstName = c.ContactFirstName, LastName = c.ContactLastName, CompanyName = c.CompanyName });
        }
    }
}
