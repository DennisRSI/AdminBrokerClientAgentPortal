using Codes.Service.Data;
using Codes.Service.ViewModels;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Codes.Service.Domain
{
    public class AccountQueryAgent : IAccountQuery
    {
        private readonly int _agentId;
        private readonly CodesDbContext _context;

        public AccountQueryAgent(CodesDbContext context, int agentId)
        {
            _context = context;
            _agentId = agentId;
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
            return _context.Campaigns.Where(c => c.Client.AgentId == _agentId)
                .Select(c => new AccountViewModel() { Id = c.CampaignId, CompanyName = c.CampaignName });
        }

        public IEnumerable<AccountViewModel> GetClients()
        {
            return _context.Clients.Where(c => c.AgentId == _agentId)
                    .Select(c => new AccountViewModel() { Id = c.ClientId, FirstName = c.ContactFirstName, LastName = c.ContactLastName, CompanyName = c.CompanyName });
        }
    }
}
