using AutoMapper;
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
        private readonly IMapper _mapper;

        public AccountService(CodesDbContext context, ILoggerFactory loggerFactory, IMapper mapper)
        {
            _context = context;
            _logger = loggerFactory.CreateLogger<CodeService>();
            _mapper = mapper;
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

        public ClientEditViewModel GetClientEdit(int clientId)
        {
            var client = _context.Clients.Single(c => c.ClientId == clientId);

            var model = _mapper.Map<ClientEditViewModel>(client);

            return model;
        }

        public IEnumerable<AccountViewModel> GetClientsOfBroker(int brokerId)
        {
            return _context.Clients.Where(c => c.BrokerId == brokerId)
                    .Select(c => new AccountViewModel(c.ClientId, c.CompanyName));
        }

        public IEnumerable<AccountViewModel> GetAgentsOfBroker(int brokerId)
        {
            return _context.Agents.Where(a => a.BrokerId == brokerId)
                    .Select(a => new AccountViewModel(a.AgentId, a.CompanyName));
        }
    }
}
