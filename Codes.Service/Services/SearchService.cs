using Codes.Service.Data;
using Codes.Service.Interfaces;
using Codes.Service.Models;
using Codes.Service.ViewModels;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
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
            var model = new SearchViewModel
            {
                Query = query
            };

            return model;
        }
    }
}
