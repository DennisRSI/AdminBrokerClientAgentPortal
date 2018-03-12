using Codes.Service.Data;
using Codes.Service.Interfaces;
using Codes.Service.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
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

        public void Purchase(PurchaseViewModel model)
        {
        }
    }
}
