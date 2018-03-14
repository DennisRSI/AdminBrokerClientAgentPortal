﻿using Codes.Service.Data;
using Codes.Service.Interfaces;
using Codes.Service.Models;
using Codes.Service.ViewModels;
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

        public void Purchase(string brokerReference, PurchaseViewModel model)
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
            _context.SaveChanges();
        }
    }
}