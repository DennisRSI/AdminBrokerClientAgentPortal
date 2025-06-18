using Codes1.Service.Data;
using Codes1.Service.ViewModels;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Codes1.Service.Domain
{
    public class AccountQueryAgent : IAccountQuery
    {
        private readonly int _agentId;
        private readonly Codes1DbContext _context;

        public AccountQueryAgent(Codes1DbContext context, int agentId)
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
