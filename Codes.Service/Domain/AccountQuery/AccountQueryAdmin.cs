using Codes.Service.Data;
using Codes.Service.ViewModels;
using System.Collections.Generic;
using System.Linq;

namespace Codes.Service.Domain
{
    public class AccountQueryAdmin : IAccountQuery
    {
        private readonly CodesDbContext _context;

        public AccountQueryAdmin(CodesDbContext context)
        {
            _context = context;
        }

        public IEnumerable<AccountViewModel> GetAgents()
        {
            return _context.Agents
                    .Select(a => new AccountViewModel() { Id = a.AgentId, FirstName = a.AgentFirstName, LastName = a.AgentLastName, CompanyName = a.CompanyName });
        }

        public IEnumerable<AccountViewModel> GetBrokers()
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<AccountViewModel> GetCampaigns()
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<AccountViewModel> GetClients()
        {
            return _context.Clients
                    .Select(c => new AccountViewModel() { Id = c.ClientId, FirstName = c.ContactFirstName, LastName = c.ContactLastName, CompanyName = c.CompanyName });
        }
    }
}
