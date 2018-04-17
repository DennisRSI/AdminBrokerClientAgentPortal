using Codes.Service.Data;
using Codes.Service.Interfaces;
using Codes.Service.Models;
using Codes.Service.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;

namespace Codes.Service.Services
{
    public class PurchaseService : IPurchaseService
    {
        private readonly ILogger _logger;
        private readonly CodesDbContext _context;

        public PurchaseService(CodesDbContext context, ILoggerFactory loggerFactory)
        {
            _context = context;
            _logger = loggerFactory.CreateLogger<CodeService>();
        }

        public PurchaseDisplayViewModel Purchase(string brokerReference, PurchaseViewModel model)
        {
            // TODO: Make the actual purchase here once the credit card processor is determined

            var broker = _context.Brokers.Single(b => b.ApplicationReference == brokerReference);

            var creditCard = new String(model.CreditCardNumber.Where(char.IsDigit).ToArray());
            var last4 = creditCard.Substring(Math.Max(0, creditCard.Length - 4));

            var purchase = new PurchaseModel()
            {
                Broker = broker,
                PhysicalValue = model.PhysicalValue,
                PhysicalQuantity = model.PhysicalQuantity,
                VirtualValue = model.VirtualValue,
                VirtualQuantity = model.VirtualQuantity,
                FullName = model.FullName,
                BillingZip = model.BillingZip,
                Address = model.Address,
                City = model.City,
                State = model.State,
                ShippingZip = model.ShippingZip,
                CreditCardLast4 = last4,
            };

            _context.Purchases.Add(purchase);

            if (model.PhysicalQuantity > 0)
            {
                GenerateCodes(broker.BrokerId, "Physical", model.PhysicalQuantity, model.PhysicalValue);
            }

            if (model.VirtualQuantity > 0)
            {
                GenerateCodes(broker.BrokerId, "Virtual", model.VirtualQuantity, model.VirtualValue);
            }

            _context.SaveChanges();

            return GetDisplayViewModel(purchase);
        }

        public IQueryable<PurchaseDisplayViewModel> GetList(int brokerId)
        {
            return _context.Purchases.Where(p => p.BrokerId == brokerId).OrderByDescending(p => p.PurchaseId)
                        .Select(p => GetDisplayViewModel(p));
        }

        public PurchaseDisplayViewModel GetDetails(int purchaseId)
        {
            var purchase = _context.Purchases.Single(p => p.PurchaseId == purchaseId);
            return GetDisplayViewModel(purchase);
        }

        private PurchaseDisplayViewModel GetDisplayViewModel(PurchaseModel model)
        {
            return new PurchaseDisplayViewModel()
            {
                PurchaseDate = model.CreationDate,
                OrderId = model.PurchaseId.ToString(),
                PhysicalValue = model.PhysicalValue,
                PhysicalQuantity = model.PhysicalQuantity,
                VirtualValue = model.VirtualValue,
                VirtualQuantity = model.VirtualQuantity,
                SequenceStart = "SEQSTART",
                SequenceEnd = "SEQEND"
            };
        }

        private void GenerateCodes(int brokerId, string codeType, int quantity, decimal chargeAmount)
        {
            const string letterStart = "aa";
            const string letterEnd = "zz";
            const int increment = 3;

            int start = 0;
            int end = 0;

            var codeRange = _context.CodeRanges
                .Where(r => r.PreAlphaCharacters == letterStart && r.PostAlphaCharacters == letterEnd)
                .OrderByDescending(r => r.EndNumber)
                .Take(1);

            if (codeRange.Any())
            {
                var single = codeRange.Single();
                start = single.EndNumber + increment;
            }

            end = start + quantity * increment;

            _context.Brokers.FromSql("EXECUTE dbo.GenerateCodes {0}, {1}, {2}, {3}, {4}, {5}, {6}, {7}, {8}, {9}, {10}, {11}, {12}, {13}, {14}, {15}, {16}, {17}, {18}, {19}",
                  letterStart, // @LettersStart
                  letterEnd, // @LettersEnd
                  increment, // @IncrementBy
                  brokerId, // @BrokerId
                  null, // @ClientId
                  null, // @CampaignId
                  codeType, // @CodeType
                  String.Empty, // @Issuer
                  0, // @PackageId
                  5, // @PaddingNumber
                  "0", // @PaddingValue
                  start, // @StartNumber
                  end, // @EndNumber
                  0, // @HotelPoints
                  0, // @CondoRewards
                  1, // @NumberOfUses  ??
                  chargeAmount, // @ChargeAmount
                  null, // @StartDate
                  null, // @EndDate
                  1 // @VerifyEmail
                  ).ToList();
        }
    }
}
