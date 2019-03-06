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
            var clientIdList = _context.ClientAgents.Where(ca => ca.AgentId == _agentId)
                .Select(ca => ca.ClientId);

            return _context.Campaigns.Where(c => clientIdList.Contains(c.ClientId.Value))
                .Select(c => new AccountViewModel() { Id = c.CampaignId, CompanyName = c.CampaignName });
        }

        public IEnumerable<AccountViewModel> GetClients()
        {
            return _context.ClientAgents.Where(ca => ca.AgentId == _agentId)
                    .Select(ca => new AccountViewModel() { Id = ca.ClientId, FirstName = ca.Client.ContactFirstName, LastName = ca.Client.ContactLastName, CompanyName = ca.Client.CompanyName });
        }
    }
}
