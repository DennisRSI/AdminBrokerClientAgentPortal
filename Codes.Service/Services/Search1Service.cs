using Codes1.Service.Data;
using Codes1.Service.Interfaces;
using Codes1.Service.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;

namespace Codes1.Service.Services
{
    public class Search1Service : ISearch1Service
    {
        private readonly ILogger _logger;
        private readonly Codes1DbContext _context;

        public Search1Service(Codes1DbContext context, ILoggerFactory loggerFactory)
        {
            _context = context;
            _logger = loggerFactory.CreateLogger<Code1Service>();
        }

        public SearchViewModel Search(string query, string accountType, int accountId)
        {
            query = query.Trim();

            string firstName = String.Empty;
            string lastName = String.Empty;
            var split = query.Split(' ');

            // Check if the query string might be in "FIRSTNAME LASTNAME" format
            if (split.Length == 2)
            {
                firstName = split[0];
                lastName = split[1];
            }

            switch (accountType.ToLower())
            {
                case "superadministrator":
                case "administrator":
                    return GetAdmin(query, firstName, lastName);

                case "broker":
                    return GetBroker(query, firstName, lastName, accountId);

                case "agent":
                    return GetAgent(query, firstName, lastName, accountId);

                case "client":
                    return GetClient(query, accountId);
            }

            throw new ArgumentException("Invalid accountType: " + accountType);
        }

        public SearchViewModel GetAdmin(string query, string firstName, string lastName)
        {
            var model = new SearchViewModel
            {
                Query = query,
                ShowAgent = true,
                ShowBroker = true,
                ShowClient = true
            };

            query += "%";

            model.Agents = _context.Agents
                                .Where(a =>
                                    EF.Functions.Like(a.AgentFirstName, query) ||
                                    EF.Functions.Like(a.AgentLastName, query) ||
                                    EF.Functions.Like(a.CompanyName, query) ||
                                    EF.Functions.Like(a.Email, query) ||
                                    (
                                        !String.IsNullOrWhiteSpace(firstName) &&
                                        !String.IsNullOrWhiteSpace(lastName) &&
                                        EF.Functions.Like(a.AgentFirstName, firstName) &&
                                        EF.Functions.Like(a.AgentLastName, lastName)
                                    )

                                )
                                .Select(a => new SearchUserViewModel()
                                {
                                    Id = a.AgentId,
                                    ApplicationReference = a.ApplicationReference,
                                    FirstName = a.AgentFirstName,
                                    LastName = a.AgentLastName,
                                    CompanyName = a.CompanyName,
                                    Email = a.Email,
                                    PhoneNumber = String.IsNullOrWhiteSpace(a.OfficePhone) ? a.MobilePhone : a.OfficePhone
                                }
                                );

            model.Brokers = _context.Brokers
                                .Where(b =>
                                    EF.Functions.Like(b.BrokerFirstName, query) ||
                                    EF.Functions.Like(b.BrokerLastName, query) ||
                                    EF.Functions.Like(b.CompanyName, query) ||
                                    EF.Functions.Like(b.Email, query) ||
                                    (
                                        !String.IsNullOrWhiteSpace(firstName) &&
                                        !String.IsNullOrWhiteSpace(lastName) &&
                                        EF.Functions.Like(b.BrokerFirstName, firstName) &&
                                        EF.Functions.Like(b.BrokerLastName, lastName)
                                    )
                                )
                                .Select(b => new SearchUserViewModel()
                                {
                                    Id = b.BrokerId,
                                    ApplicationReference = b.ApplicationReference,
                                    FirstName = b.BrokerFirstName,
                                    LastName = b.BrokerLastName,
                                    CompanyName = b.CompanyName,
                                    Email = b.Email,
                                    PhoneNumber = string.IsNullOrWhiteSpace(b.OfficePhone) ? b.MobilePhone : b.OfficePhone
                                }
                                );

            model.Clients = _context.Clients
                                .Where(c =>
                                    EF.Functions.Like(c.ContactFirstName, query) ||
                                    EF.Functions.Like(c.ContactLastName, query) ||
                                    EF.Functions.Like(c.CompanyName, query) ||
                                    EF.Functions.Like(c.Email, query) ||
                                    (
                                        !String.IsNullOrWhiteSpace(firstName) &&
                                        !String.IsNullOrWhiteSpace(lastName) &&
                                        EF.Functions.Like(c.ContactFirstName, firstName) &&
                                        EF.Functions.Like(c.ContactLastName, lastName)
                                    )

                                )
                                .Select(c => new SearchUserViewModel()
                                {
                                    Id = c.ClientId,
                                    ApplicationReference = c.ApplicationReference,
                                    FirstName = c.ContactFirstName,
                                    LastName = c.ContactLastName,
                                    CompanyName = c.CompanyName,
                                    Email = c.Email,
                                    PhoneNumber = string.IsNullOrWhiteSpace(c.OfficePhone) ? c.MobilePhone : c.OfficePhone
                                }
                                );

            model.Campaigns = _context.Campaigns
                                .Where(c =>
                                    EF.Functions.Like(c.CampaignDescription, query) ||
                                    EF.Functions.Like(c.CampaignName, query)
                                )
                                .Select(c => new SearchCampaignViewModel()
                                {
                                    Id = c.CampaignId,
                                    ApplicationReference = c.Client.ApplicationReference,
                                    Description = c.CampaignDescription,
                                    Name = c.CampaignName,
                                }
                                );

            model.Cards = _context.Codes
                                .Where(c =>
                                    EF.Functions.Like(c.Code, query)
                                )
                                .Select(c => new SearchCardViewModel()
                                    {
                                        Id = c.CodeId,
                                        CardNumber = c.Code,
                                        Status = "Purchased"
                                    }
                                );

            return model;
        }

        public SearchViewModel GetBroker(string query, string firstName, string lastName, int brokerId)
        {
            var model = new SearchViewModel
            {
                Query = query,
                ShowAgent = true,
                ShowBroker = false,
                ShowClient = true
            };

            query += "%";

            model.Agents = _context.Agents
                                .Where(a => a.BrokerId == brokerId)
                                .Where(a =>
                                    EF.Functions.Like(a.AgentFirstName, query) ||
                                    EF.Functions.Like(a.AgentLastName, query) ||
                                    EF.Functions.Like(a.CompanyName, query) ||
                                    EF.Functions.Like(a.Email, query) ||
                                    (
                                        !String.IsNullOrWhiteSpace(firstName) &&
                                        !String.IsNullOrWhiteSpace(lastName) &&
                                        EF.Functions.Like(a.AgentFirstName, firstName) &&
                                        EF.Functions.Like(a.AgentLastName, lastName)
                                    )
                                )
                                .Select(a => new SearchUserViewModel()
                                {
                                    Id = a.AgentId,
                                    ApplicationReference = a.ApplicationReference,
                                    FirstName = a.AgentFirstName,
                                    LastName = a.AgentLastName,
                                    CompanyName = a.CompanyName,
                                    Email = a.Email,
                                    PhoneNumber = String.IsNullOrWhiteSpace(a.OfficePhone) ? a.MobilePhone : a.OfficePhone
                                }
                                );

            model.Clients = _context.Clients
                                .Where(c => c.BrokerId == brokerId)
                                .Where(c =>
                                    EF.Functions.Like(c.ContactFirstName, query) ||
                                    EF.Functions.Like(c.ContactLastName, query) ||
                                    EF.Functions.Like(c.CompanyName, query) ||
                                    EF.Functions.Like(c.Email, query) ||
                                    (
                                        !String.IsNullOrWhiteSpace(firstName) &&
                                        !String.IsNullOrWhiteSpace(lastName) &&
                                        EF.Functions.Like(c.ContactFirstName, firstName) &&
                                        EF.Functions.Like(c.ContactLastName, lastName)
                                    )

                                )
                                .Select(c => new SearchUserViewModel()
                                {
                                    Id = c.ClientId,
                                    ApplicationReference = c.ApplicationReference,
                                    FirstName = c.ContactFirstName,
                                    LastName = c.ContactLastName,
                                    CompanyName = c.CompanyName,
                                    Email = c.Email,
                                    PhoneNumber = string.IsNullOrWhiteSpace(c.OfficePhone) ? c.MobilePhone : c.OfficePhone
                                }
                                );

            model.Campaigns = _context.Campaigns
                                .Where(c => c.BrokerId == brokerId)
                                .Where(c =>
                                    EF.Functions.Like(c.CampaignDescription, query) ||
                                    EF.Functions.Like(c.CampaignName, query)
                                )
                                .Select(c => new SearchCampaignViewModel()
                                {
                                    Id = c.CampaignId,
                                    ApplicationReference = c.Client.ApplicationReference,
                                    Description = c.CampaignDescription,
                                    Name = c.CampaignName,
                                }
                                );

            model.Cards = _context.Codes
                                .Where(c => c.Campaign.BrokerId == brokerId)
                                .Where(c =>
                                    EF.Functions.Like(c.Code, query)
                                )
                                .Select(c => new SearchCardViewModel()
                                {
                                    Id = c.CodeId,
                                    CardNumber = c.Code,
                                    Status = "Purchased"
                                }
                                );

            return model;
        }

        public SearchViewModel GetAgent(string query, string firstName, string lastName, int agentId)
        {
            var model = new SearchViewModel
            {
                Query = query,
                ShowClient = true
            };

            query += "%";

            model.Clients = _context.Clients
                                .Where(c => c.AgentId == agentId)
                                .Where(c =>
                                    EF.Functions.Like(c.ContactFirstName, query) ||
                                    EF.Functions.Like(c.ContactLastName, query) ||
                                    EF.Functions.Like(c.CompanyName, query) ||
                                    EF.Functions.Like(c.Email, query) ||
                                    (
                                        !String.IsNullOrWhiteSpace(firstName) &&
                                        !String.IsNullOrWhiteSpace(lastName) &&
                                        EF.Functions.Like(c.ContactFirstName, firstName) &&
                                        EF.Functions.Like(c.ContactLastName, lastName)
                                    )

                                )
                                .Select(c => new SearchUserViewModel()
                                {
                                    Id = c.ClientId,
                                    ApplicationReference = c.ApplicationReference,
                                    FirstName = c.ContactFirstName,
                                    LastName = c.ContactLastName,
                                    CompanyName = c.CompanyName,
                                    Email = c.Email,
                                    PhoneNumber = string.IsNullOrWhiteSpace(c.OfficePhone) ? c.MobilePhone : c.OfficePhone
                                }
                                );

            model.Campaigns = _context.Campaigns
                                .Where(c => c.Client.AgentId == agentId)
                                .Where(c =>
                                    EF.Functions.Like(c.CampaignDescription, query) ||
                                    EF.Functions.Like(c.CampaignName, query)
                                )
                                .Select(c => new SearchCampaignViewModel()
                                {
                                    Id = c.CampaignId,
                                    ApplicationReference = c.Client.ApplicationReference,
                                    Description = c.CampaignDescription,
                                    Name = c.CampaignName,
                                }
                                );

            model.Cards = _context.Codes
                                .Where(c => c.Campaign.Client.AgentId == agentId)
                                .Where(c =>
                                    EF.Functions.Like(c.Code, query)
                                )
                                .Select(c => new SearchCardViewModel()
                                {
                                    Id = c.CodeId,
                                    CardNumber = c.Code,
                                    Status = "Purchased"
                                }
                                );

            return model;
        }

        public SearchViewModel GetClient(string query, int clientId)
        {
            var model = new SearchViewModel
            {
                Query = query
            };

            query += "%";

            model.Campaigns = _context.Campaigns
                                .Where(c => c.ClientId == clientId)
                                .Where(c =>
                                    EF.Functions.Like(c.CampaignDescription, query) ||
                                    EF.Functions.Like(c.CampaignName, query)
                                )
                                .Select(c => new SearchCampaignViewModel()
                                {
                                    Id = c.CampaignId,
                                    ApplicationReference = c.Client.ApplicationReference,
                                    Description = c.CampaignDescription,
                                    Name = c.CampaignName,
                                }
                                );

            model.Cards = _context.Codes
                                .Where(c => c.Campaign.ClientId == clientId)
                                .Where(c =>
                                    EF.Functions.Like(c.Code, query)
                                )
                                .Select(c => new SearchCardViewModel()
                                {
                                    Id = c.CodeId,
                                    CardNumber = c.Code,
                                    Status = "Purchased"
                                }
                                );

            return model;
        }
    }
}
