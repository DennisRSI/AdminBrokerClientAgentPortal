using Codes1.Service.Data;
using Codes1.Service.Interfaces;
using Codes1.Service.Models;
using Codes1.Service.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;

namespace Codes1.Service.Services
{
    public class Purchase1Service : IPurchase1Service
    {
        private readonly ILogger _logger;
        private readonly Codes1DbContext _context;

        public Purchase1Service(Codes1DbContext context, ILoggerFactory loggerFactory)
        {
            _context = context;
            _logger = loggerFactory.CreateLogger<Code1Service>();
        }

        public PurchaseDisplayViewModel Purchase(string brokerReference, PurchaseViewModel model)
        {
            // TODO: Make the actual purchase here once the credit card processor is determined

            var broker = _context.Brokers.Single(b => b.ApplicationReference == brokerReference);

            var creditCard = new String(model.CreditCardNumber.Where(char.IsDigit).ToArray());
            var last4 = creditCard.Substring(Math.Max(0, creditCard.Length - 4));

            using (var transaction = _context.Database.BeginTransaction())
            {
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
                _context.SaveChanges();

                if (model.PhysicalQuantity > 0)
                {
                    GenerateCodes(broker.BrokerId, purchase.PurchaseId, "Physical", model.PhysicalQuantity, model.PhysicalValue);
                }

                if (model.VirtualQuantity > 0)
                {
                    GenerateCodes(broker.BrokerId, purchase.PurchaseId, "Virtual", model.VirtualQuantity, model.VirtualValue);
                }

                _context.SaveChanges();
                transaction.Commit();

                return GetDisplayViewModel(purchase);
            }
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

        private void GenerateCodes(int brokerId, int purchaseId, string codeType, int quantity, decimal chargeAmount)
        {
            const string prefix = "aa";
            const string suffix = "zz";
            const int increment = 3;

            _context.Brokers.FromSqlRaw("EXECUTE dbo.CreateCodes {0}, {1}, {2}, {3}, {4}, {5}, {6}, {7}",
                                        brokerId,
                                        purchaseId,
                                        quantity,
                                        1, // TODO: Number of uses. How to determine this?
                                        codeType,
                                        prefix,
                                        suffix,
                                        increment
                                    ).ToList();
        }
    }
}
