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
            using (var transaction = _context.Database.BeginTransaction())
            {
                var model = new CampaignModel
                {
                    BrokerId = 1, // Is the broker needed here because the client already is assigned to a broker?
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

                CreateCodes(viewModel);

                transaction.Commit();
            }

        }

        private void CreateCodes(CampaignViewModel model)
        {
            int startNumber = 1000;
            int endNumber = 1000;

            var options = new CodeGeneratorOptions()
            {
                Prefix = model.CardPrefix,
                Suffix = model.CardSuffix,
                Increment = model.Increment,
                BrokerId = 1, // TODO
                ClientId = 1, // TODO
                CampaignId = 1, // TODO
                PackageId = 0, // TODO
                Padding = model.Padding,
                StartNumber = startNumber,
                EndNumber = endNumber,
                FaceValue = 0, // TODO
                Quantity = model.CardQuantity,
                ActivationsPerCode = model.ActivationsPerCard,
                StartDate = DateTime.Now, //  TODO model.StartDate,
                EndDate = DateTime.Now // TODO model.EndDate
            };

            _codeGeneratorService.GenerateCodes(1, options); // TODO: Change 1
        }

        public DataTableViewModel<CampaignViewModel> GetByClient(int id)
        {
            var queryResult = _context.Campaigns
                                .Where(c => c.ClientId == id && c.IsActive)
                                .OrderByDescending(c => c.CreationDate);

            var data = _mapper.Map<IEnumerable<CampaignModel>, IEnumerable<CampaignViewModel>>(queryResult);

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
