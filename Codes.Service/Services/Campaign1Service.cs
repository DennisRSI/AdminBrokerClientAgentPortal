using AutoMapper;
using Codes1.Service.Data;
using Codes1.Service.Domain;
using Codes1.Service.Interfaces;
using Codes1.Service.Models;
using Codes1.Service.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Codes1.Service.Services
{
    public class Campaign1Service : ICampaign1Service
    {
        private readonly ILogger _logger;
        private readonly Codes1DbContext _context;
        private readonly ICodeGenerator1Service _codeGeneratorService;

        public Campaign1Service(Codes1DbContext context, ILoggerFactory loggerFactory, IMapper mapper, ICodeGenerator1Service codeGeneratorService)
        {
            _context = context;
            _logger = loggerFactory.CreateLogger<Code1Service>();
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

        public async Task<string> Create(int clientId, CampaignViewModel viewModel)
        {
            var brokerId = _context.Clients.Single(c => c.ClientId == clientId).BrokerId;
            viewModel.PackageId = PackageCode.GetCode(viewModel.BenefitCondo, viewModel.BenefitShopping, viewModel.BenefitDining, viewModel.BenefitNewHotels, viewModel.BenefitNewCondos);

             (bool isSuccess, string message) = await _codeGeneratorService.CheckAvailableCodes(viewModel.StartNumber, viewModel.CardQuantity, viewModel.Increment, viewModel.CardPrefix, viewModel.CardSuffix, viewModel.Padding);

            if (isSuccess)
            {
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
                        CardQuantity = viewModel.CardQuantity,
                        PackageId = viewModel.PackageId,
                        Points = Convert.ToSingle(viewModel.FaceValue),
                    };

                    _context.Campaigns.Add(model);
                    _context.SaveChanges();

                    viewModel.CampaignId = model.CampaignId;

                    CreateCodes(clientId, brokerId, viewModel);

                    transaction.Commit();

                    return "Success";
                }
            }
            else
            {
                return message;
            }
        }

        public void Deactivate(int campaignId, string reason)
        {
            var campaign = _context.Campaigns.Single(c => c.CampaignId == campaignId);
            campaign.DeactivationDate = DateTime.Now;
            campaign.DeactivationReason = reason;

            _context.Campaigns.Update(campaign);
            _context.SaveChanges();
        }

        

        private void CreateCodes(int clientId, int brokerId, CampaignViewModel model)
        {
            int endNumber = model.StartNumber + (model.CardQuantity - 1) * model.Increment;

            var options = new CodeGeneratorOptions()
            {
                CampaignType = model.CampaignType,
                Prefix = model.CardPrefix,
                Suffix = model.CardSuffix,
                Increment = model.Increment,
                BrokerId = brokerId,
                ClientId = clientId,
                CampaignId = model.CampaignId,
                PackageId = model.PackageId,
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
                    DeactivationDate = q.DeactivationDate,
                    CampaignId = q.CampaignId,
                    CampaignName = q.CampaignName,
                    CampaignType = q.CampaignType,
                    BenefitText = PackageCode.GetText(q.PackageId)
                });

            var dataArray = data.ToArray();
            AddCodeInformation(dataArray);

            return new DataTableViewModel<CampaignViewModel>
            {
                NumberOfRows = queryResult.Count(),
                RecordsFiltered = queryResult.Count(),
                Data = dataArray,
                Message = "Success"
            };
        }

        private void AddCodeInformation(IEnumerable<CampaignViewModel> models)
        {
            foreach (var model in models)
            {
                var codeRanges =
                    from cr in _context.CodeRanges
                    join ccr in _context.CampaignCodeRanges on cr.CodeRangeId equals ccr.CodeRangeId
                    where ccr.CampaignId == model.CampaignId
                    orderby cr.CreationDate descending
                    select cr;

                var codeRange = codeRanges.FirstOrDefault();

                if (codeRange != null)
                {
                    model.StartNumber = codeRange.StartNumber;
                    model.EndNumber = codeRange.EndNumber;
                    model.Increment = codeRange.IncrementByNumber;
                    model.FaceValue = codeRange.Points.ToString();
                    model.CardPrefix = codeRange.PreAlphaCharacters;
                    model.CardSuffix = codeRange.PostAlphaCharacters;
                    model.CardQuantity = codeRange.GetTotalCodes();
                    model.TotalPossibleActivations = codeRange.GetTotalPossibleActivations();
                    model.ActivationsPerCard = codeRange.NumberOfUses;
                }
            }
        }
    }
}
