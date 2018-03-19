using Codes.Service.Data;
using Codes.Service.Interfaces;
using Codes.Service.Models;
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
    }
}
