using Codes.Service.Data;
using Codes.Service.Interfaces;
using Codes.Service.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;

namespace Codes.Service.Services
{
    public class SearchService : ISearchService
    {
        private readonly ILogger _logger;
        private readonly CodesDbContext _context;

        public SearchService(CodesDbContext context, ILoggerFactory loggerFactory)
        {
            _context = context;
            _logger = loggerFactory.CreateLogger<CodeService>();
        }

        public SearchViewModel GetAdmin(string query)
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

            var model = new SearchViewModel
            {
                Query = query
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
                                    Id = a.BrokerId,
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
                                    Id = c.BrokerId,
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
                                    Description = c.CampaignDescription,
                                    Name = c.CampaignName,
                                }
                                );

            var codes = _context.Codes
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

            var unusedCodes = _context.UnusedCodes
                                .Where(c =>
                                    EF.Functions.Like(c.Code, query)
                                )
                                .Select(c => new SearchCardViewModel()
                                {
                                    Id = c.UnusedCodeId,
                                    CardNumber = c.Code,
                                    Status = "In Campaign"
                                }
                                );

            var pendingCodes = _context.PendingCodes
                                .Where(c =>
                                    EF.Functions.Like(c.Code, query)
                                )
                                .Select(c => new SearchCardViewModel()
                                {
                                    Id = c.PendingCodeId,
                                    CardNumber = c.Code,
                                    Status = "Registered"
                                }
                                );

            var usedCodes = _context.UsedCodes
                                .Where(c =>
                                    EF.Functions.Like(c.Code, query)
                                )
                                .Select(c => new SearchCardViewModel()
                                {
                                    Id = c.UsedCodeId,
                                    CardNumber = c.Code,
                                    Status = "Activated"
                                }
                                );

            model.Cards = codes.Concat(unusedCodes).Concat(pendingCodes).Concat(usedCodes);

            return model;
        }
    }
}
