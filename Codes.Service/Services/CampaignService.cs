using AutoMapper;
using Codes.Service.Data;
using Codes.Service.Domain;
using Codes.Service.Interfaces;
using Codes.Service.Models;
using Codes.Service.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;

namespace Codes.Service.Services
{
    public class CampaignService : ICampaignService
    {
        private readonly ILogger _logger;
        private readonly CodesDbContext _context;
        private readonly IMapper _mapper;
        private readonly ICodeGeneratorService _codeGeneratorService;

        public CampaignService(CodesDbContext context, ILoggerFactory loggerFactory, IMapper mapper, ICodeGeneratorService codeGeneratorService)
        {
            _context = context;
            _logger = loggerFactory.CreateLogger<CodeService>();
            _codeGeneratorService = codeGeneratorService;
        }

        public void Clone(int campaignId)
        {
            var clone = _context.Campaigns
                            .AsNoTracking()
                            .Single(c => c.CampaignId == campaignId);

            clone.CampaignId = 0;
            clone.CreationDate = DateTime.Now;

            if (!clone.CampaignName.Contains(" Copy"))
            {
                clone.CampaignName += " Copy";
            }

            _context.Campaigns.Add(clone);
            _context.SaveChanges();
        }

        public void Create(int clientId, CampaignViewModel viewModel)
        {
            var brokerId = _context.Clients.Single(c => c.ClientId == clientId).BrokerId;

            using (var transaction = _context.Database.BeginTransaction())
            {
                var model = new CampaignModel
                {
                    BrokerId = brokerId,
                    CampaignDescription = viewModel.CampaignDescription,
                    CampaignName = viewModel.CampaignName,
                    CampaignType = viewModel.CampaignType,
                    CustomCSS = viewModel.CustomCSS,
                    CustomCssPost = viewModel.CustomCssPost,
                    EndDateTime = viewModel.EndDateTime,
                    GoogleAnalyticsCode = viewModel.GoogleAnalyticsCode,
                    IsActive = true,
                    StartDateTime = viewModel.StartDateTime,
                    ClientId = clientId,
                    PostLoginVideoId = viewModel.PostLoginVideoId,
                    PreLoginVideoId = viewModel.PreLoginVideoId,
                    CardQuantity = viewModel.CardQuantity
                };

                _context.Campaigns.Add(model);
                _context.SaveChanges();

                viewModel.CampaignId = model.CampaignId;

                CreateCodes(clientId, brokerId, viewModel);

                transaction.Commit();
            }

        }

        private void CreateCodes(int clientId, int brokerId, CampaignViewModel model)
        {
            int endNumber = model.StartNumber + (model.CardQuantity - 1) * model.Increment;
            int packageId = PackageCode.GetCode(model.BenefitCondo, model.BenefitShopping, model.BenefitDining);

            var options = new CodeGeneratorOptions()
            {
                CampaignType = model.CampaignType,
                Prefix = model.CardPrefix,
                Suffix = model.CardSuffix,
                Increment = model.Increment,
                BrokerId = brokerId,
                ClientId = clientId,
                CampaignId = model.CampaignId,
                PackageId = packageId,
                Padding = model.Padding,
                StartNumber = model.StartNumber,
                EndNumber = endNumber,
                FaceValue = Convert.ToInt32(model.FaceValue),
                Quantity = model.CardQuantity,
                ActivationsPerCode = model.ActivationsPerCard,
                StartDate = GetDate(model.StartDate),
                EndDate = GetNullableDate(model.EndDate)
            };

            _codeGeneratorService.GenerateCodes(options);
        }

        private DateTime GetDate(string dateString)
        {
            return DateTime.Parse(dateString);
        }

        private DateTime? GetNullableDate(string dateString)
        {
            if (String.IsNullOrWhiteSpace(dateString))
            {
                return null;
            }

            return GetDate(dateString);
        }

        public DataTableViewModel<CampaignViewModel> GetByClient(int id)
        {
            var queryResult = _context.Campaigns
                                .Where(c => c.ClientId == id && c.IsActive)
                                .OrderByDescending(c => c.CreationDate);

            var data = queryResult.Select(q => new CampaignViewModel()
                {
                    CampaignId = q.CampaignId,
                    CampaignName = q.CampaignName,
                    CardQuantity = q.CardQuantity,
                    CampaignType = q.CampaignType,
                    StartNumber = (q.CampaignCodeRange.Count == 0) ? 0 : q.CampaignCodeRange.Max(r => r.CodeRange.StartNumber)
                });

            return new DataTableViewModel<CampaignViewModel>
            {
                NumberOfRows = queryResult.Count(),
                RecordsFiltered = queryResult.Count(),
                Data = data.ToArray(),
                Message = "Success"
            };
        }
    }
}
