using AutoMapper;
using Codes.Service.Data;
using Codes.Service.Interfaces;
using Codes.Service.Models;
using Codes.Service.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
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

            model.Agents = GetAgentsOfBroker(client.BrokerId)
                .Select(a => new SelectListItem() { Value = a.Id.ToString(), Text = a.FullName, Selected = (a.Id == client.AgentId) });

            model.CommissionRate = client.CommissionRate.ToString();

            return model;
        }

        public IEnumerable<AccountViewModel> GetClientsOfBroker(int brokerId)
        {
            return _context.Clients.Where(c => c.BrokerId == brokerId)
                    .Select(c => new AccountViewModel() { Id = c.ClientId, FirstName = c.ContactFirstName, LastName = c.ContactLastName, CompanyName = c.CompanyName });
        }

        public IEnumerable<AccountViewModel> GetAgentsOfBroker(int brokerId)
        {
            return _context.Agents.Where(a => a.BrokerId == brokerId)
                    .Select(a => new AccountViewModel() { Id = a.AgentId, FirstName = a.AgentFirstName, LastName = a.AgentLastName, CompanyName = a.CompanyName });
        }

        public IEnumerable<AccountViewModel> GetAllBrokers()
        {
            return _context.Brokers.Where(b => b.IsActive)
                    .Select(b => new AccountViewModel() { Id = b.BrokerId, FirstName = b.BrokerFirstName, LastName = b.BrokerLastName, CompanyName = b.CompanyName });
        }

        public IEnumerable<AccountViewModel> GetAllClients()
        {
            return _context.Clients.Where(b => b.IsActive)
                    .Select(c => new AccountViewModel() { Id = c.ClientId, FirstName = c.ContactFirstName, LastName = c.ContactLastName, CompanyName = c.CompanyName });
        }

        public IEnumerable<AccountViewModel> GetAllAgents()
        {
            return _context.Agents.Where(b => b.IsActive)
                    .Select(a => new AccountViewModel() { Id = a.AgentId, FirstName = a.AgentFirstName, LastName = a.AgentLastName, CompanyName = a.CompanyName });
        }

        public IEnumerable<AccountViewModel> GetCampaignsOfBroker(int brokerId)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<AccountViewModel> GetClientsOfAgent(int agentId)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<AccountViewModel> GetCampaignsOfAgent(int agentId)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<AccountViewModel> GetCampaignsOfClient(int clientId)
        {
            throw new System.NotImplementedException();
        }

        public AccountCommonModel GetAccountCommon(string reference)
        {
            var model = new AccountCommonModel();

            var agent = _context.Agents.SingleOrDefault(a => a.ApplicationReference == reference);
            var broker = _context.Brokers.SingleOrDefault(b => b.ApplicationReference == reference);
            var client = _context.Clients.SingleOrDefault(c => c.ApplicationReference == reference);

            if (agent != null)
            {
                model.CommissionRate = (decimal)agent.CommissionRate;
            }

            if (broker != null)
            {
                model.CommissionRate = (decimal)broker.BrokerCommissionPercentage;
                model.ClientCommissionRate = (decimal)broker.ClientCommissionPercentage;
                model.AgentCommissionRate = (decimal)broker.AgentCommissionPercentage;
            }

            if (client != null)
            {
                model.CommissionRate = (decimal)client.CommissionRate;
            }

            return model;
        }
    }
}
