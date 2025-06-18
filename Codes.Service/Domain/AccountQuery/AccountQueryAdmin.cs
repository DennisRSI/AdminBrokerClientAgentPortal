using Codes1.Service.Data;
using Codes1.Service.ViewModels;
using System.Collections.Generic;
using System.Linq;

namespace Codes1.Service.Domain
{
    public class AccountQueryAdmin : IAccountQuery
    {
        private readonly Codes1DbContext _context;

        public AccountQueryAdmin(Codes1DbContext context)
        {
            _context = context;
        }

        public IEnumerable<AccountViewModel> GetAgents()
        {
            return _context.Agents.Where(a => a.IsActive)
                    .Select(a => new AccountViewModel() { Id = a.AgentId, FirstName = a.AgentFirstName, LastName = a.AgentLastName, CompanyName = a.CompanyName });
        }

        public IEnumerable<AccountViewModel> GetBrokers()
        {
            return _context.Brokers.Where(b => b.IsActive)
                .Select(b => new AccountViewModel() { Id = b.BrokerId, FirstName = b.BrokerFirstName, LastName = b.BrokerLastName, CompanyName = b.CompanyName });
        }

        public IEnumerable<AccountViewModel> GetCampaigns()
        {
            return _context.Campaigns
                .Select(c => new AccountViewModel() { Id = c.CampaignId, CompanyName = c.CampaignName });
        }

        public IEnumerable<AccountViewModel> GetClients()
        {
            return _context.Clients.Where(c => c.IsActive)
                    .Select(c => new AccountViewModel() { Id = c.ClientId, FirstName = c.ContactFirstName, LastName = c.ContactLastName, CompanyName = c.CompanyName });
        }
    }
}
