using Codes.Service.Data;
using Codes.Service.Interfaces;
using Codes.Service.Models;
using Codes.Service.ViewModels;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;

namespace Codes.Service.Services
{
    public class AccountService : IAccountService
    {
        private readonly ILogger _logger;
        private readonly CodesDbContext _context;

        public AccountService(CodesDbContext context, ILoggerFactory loggerFactory)
        {
            _context = context;
            _logger = loggerFactory.CreateLogger<CodeService>();
        }

        public int GetIdFromReference(string reference)
        {
            var agent = _context.Agents.SingleOrDefault(a => a.ApplicationReference == reference);

            if (agent != null)
            {
                return agent.AgentId;
            }

            var broker = _context.Brokers.SingleOrDefault(b => b.ApplicationReference == reference);

            if (broker != null)
            {
                return broker.BrokerId;
            }

            var client = _context.Clients.SingleOrDefault(c => c.ApplicationReference == reference);

            if (client != null)
            {
                return client.ClientId;
            }

            return 0;
        }

        public IEnumerable<MyClientViewModel> GetClientsByBroker(int brokerId)
        {
            return _context.Clients.Where(c => c.BrokerId == brokerId)
                .Select(c =>
                       new MyClientViewModel()
                       {
                           ApplicationReference = c.ApplicationReference,
                           ClientId = c.ClientId,
                           CardQuantity = 1,
                           CompanyName = c.CompanyName,
                           ContactName = $"{c.ContactFirstName} {c.ContactLastName}",
                           Email = c.Email,
                           PhoneNumber = c.OfficePhone,
                           SalesAgent = "?"
                       }
                );
        }
    }
}
