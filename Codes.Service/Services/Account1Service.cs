﻿using AutoMapper;
using Codes1.Service.Data;
using Codes1.Service.Interfaces;
using Codes1.Service.Models;
using Codes1.Service.ViewModels;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Codes1.Service.Services
{
    public class Account1Service : IAccount1Service
    {
        private readonly ILogger _logger;
        private readonly Codes1DbContext _context;
        private readonly IMapper _mapper;
        private readonly ICode1Service _codeService;

        public Account1Service(Codes1DbContext context, ILoggerFactory loggerFactory, IMapper mapper, ICode1Service codeService)
        {
            _context = context;
            _logger = loggerFactory.CreateLogger<Code1Service>();
            _mapper = mapper;
            _codeService = codeService;
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
            var data = _context.Clients.Where(c => c.BrokerId == brokerId && c.IsActive)
                .Select(c =>
                       new MyClientViewModel()
                       {
                           ApplicationReference = c.ApplicationReference,
                           ClientId = c.ClientId,
                           CardQuantity = 1,
                           CompanyName = c.CompanyName,
                           ContactName = $"{c.ContactFirstName} {c.ContactLastName}",
                           Email = c.Email,
                           PhoneNumber = !String.IsNullOrWhiteSpace(c.OfficePhone) ? c.OfficePhone : c.MobilePhone,
                           SalesAgent = $"{c.Agent.AgentFirstName} {c.Agent.AgentLastName}"
                       }
                );

            var result = data.ToList();

            foreach (var item in result)
            {
                item.CardQuantity = GetCardQuantityByClient(item.ClientId);
            }

            return result;
        }

        public IEnumerable<MyClientViewModel> GetClientsByAgent(int agentId)
        {
            var data = _context.Clients.Where(c => c.ClientAgents.Any(a => a.AgentId == agentId))
                .Select(c =>
                       new MyClientViewModel()
                       {
                           ApplicationReference = c.ApplicationReference,
                           ClientId = c.ClientId,
                           CardQuantity = 1,
                           CompanyName = c.CompanyName,
                           ContactName = $"{c.ContactFirstName} {c.ContactLastName}",
                           Email = c.Email,
                           PhoneNumber = !String.IsNullOrWhiteSpace(c.OfficePhone) ? c.OfficePhone : c.MobilePhone,
                           SalesAgent = $"{c.Agent.AgentFirstName} {c.Agent.AgentLastName}"
                       }
                );

            var result = data.ToList();

            foreach (var item in result)
            {
                item.CardQuantity = GetCardQuantityByClient(item.ClientId);
            }

            return result;
        }

        public int GetCardQuantityByClient(int clientId, string cardType = "")
        {
            var query =
                from uc in _context.UnusedCodes
                join cr in _context.CodeRanges on uc.CodeRangeId equals cr.CodeRangeId
                join ccr in _context.CampaignCodeRanges on cr.CodeRangeId equals ccr.CodeRangeId
                join camp in _context.Campaigns on ccr.CampaignId equals camp.CampaignId
                join c in _context.Clients on camp.ClientId equals c.ClientId
                where c.ClientId == clientId && (cardType == uc.CodeType || cardType == "")
                select uc;

            int ct =  query.Count();

            return ct;
        }

        public void AddAgentToClient(int clientId, int agentId)
        {
            AgentModel agent = _context.Agents.Where(a => a.AgentId == agentId).Single();
            AgentModel parent = null;

            if(agent.ParentAgentId != null && agent.ParentAgentId > 0)
            {
                parent = _context.Agents.FirstOrDefault(x => x.AgentId == agent.ParentAgentId);

                if(_context.ClientAgents.Count(w => w.AgentId == parent.AgentId) < 1)
                {
                    ClientAgentModel link1 = new ClientAgentModel()
                    {
                        ClientId = clientId,
                        AgentId = parent.AgentId,
                        CommissionRate = (decimal)parent.CommissionRate
                    };

                    _context.ClientAgents.Add(link1);
                    _context.SaveChanges();
                }
            }

            ClientAgentModel link = new ClientAgentModel()
            {
                ClientId = clientId,
                AgentId = agentId,
                CommissionRate = (decimal) agent.CommissionRate
            };

            _context.ClientAgents.Add(link);
            _context.SaveChanges();
        }

        public void RemoveAgentFromClient(int clientId, int agentId)
        {
            var link = _context.ClientAgents.Single(ca => ca.ClientId == clientId && ca.AgentId == agentId);
            _context.ClientAgents.Remove(link);
            _context.SaveChanges();
        }

        public async Task<bool> UpdateClientCommissionRate(int clientId, int agentId, decimal commissionRate)
        {
            var client = _context.Clients.Single(c => c.ClientId == clientId);
            var percentages = await _codeService.GetBrokerPercentage(client.BrokerId);

            if (percentages.TotalBrokerCommissionPercentage - (float)commissionRate > 0)
            {
                var link = _context.ClientAgents.Single(ca => ca.ClientId == clientId && ca.AgentId == agentId);
                link.CommissionRate = commissionRate;
                _context.SaveChanges();

                return true;
            }

            return false;
        }

        public ClientEditViewModel GetClientEdit(int clientId)
        {
            var client = _context.Clients.Single(c => c.ClientId == clientId);
            var model = _mapper.Map<ClientEditViewModel>(client);

            model.Agents = GetAgentsOfBroker(client.BrokerId)
                .Select(a => new SelectListItem() { Value = a.Id.ToString(), Text = a.FullName, Selected = (a.Id == client.AgentId) });

            model.AssignedAgents = GetAgentsOfClient(client.ClientId);

            model.FirstName = client.ContactFirstName;
            model.LastName = client.ContactLastName;
            model.CommissionRate = client.CommissionRate.ToString();
            model.DeactivationDate = client.DeactivationDate;

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

        public IEnumerable<AgentViewModel> GetAgentsOfClient(int clientId)
        {
            return _context.ClientAgents.Where(ca => ca.ClientId == clientId)
                .Select(ca => new AgentViewModel() { AgentId = ca.AgentId, AgentFirstName = ca.Agent.AgentFirstName, AgentLastName = ca.Agent.AgentLastName, CommissionRate = (float)ca.CommissionRate });
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

        public void DeactivateClient(int clientId, string reason)
        {
            var client = _context.Clients.Single(c => c.ClientId == clientId);
            client.DeactivationDate = DateTime.Now;
            client.DeactivationReason = reason;
            _context.SaveChanges();
        }

        public void DeactivateAgent(int agentId, string reason)
        {
            var agent = _context.Agents.Single(a => a.AgentId == agentId);
            agent.DeactivationDate = DateTime.Now;
            agent.DeactivationReason = reason;
            _context.SaveChanges();
        }

        public IEnumerable<MyAgentViewModel> GetAgentsByAgent(int agentId)
        {
            return _context.Agents.Where(a => a.ParentAgentId == agentId)
                            .Select(a => new MyAgentViewModel()
                            {
                                AgentId = a.AgentId,
                                ApplicationReference = a.ApplicationReference,
                                Name = a.AgentFirstName + " " + a.AgentLastName,
                                Email = a.Email,
                                PhoneNumber = a.OfficePhone
                            }
                        );
        }

        public IEnumerable<MyAgentViewModel> GetAgentsByBroker(int brokerId)
        {
            return _context.Agents.Where(a => a.BrokerId == brokerId && a.ParentAgent == null)
                            .Select(a => new MyAgentViewModel()
                            {
                                AgentId = a.AgentId,
                                ApplicationReference = a.ApplicationReference,
                                Name = a.AgentFirstName + " " + a.AgentLastName,
                                Email = a.Email,
                                PhoneNumber = a.OfficePhone
                            }
                        );
        }
    }
}
