using Codes.Service.Data;
using Codes.Service.Interfaces;
using Codes.Service.Models;
using Microsoft.Extensions.Logging;
using System.Linq;

namespace Codes.Service.Services
{
    public class DocumentService : IDocumentService
    {
        private readonly ILogger _logger;
        private readonly CodesDbContext _context;

        public DocumentService(CodesDbContext context, ILoggerFactory loggerFactory)
        {
            _context = context;
            _logger = loggerFactory.CreateLogger<CodeService>();
        }

        public void Add(string role, int userId, DocumentType type, string contentType, byte[] data)
        {
            var document = new DocumentModel()
            {
                ContentType = contentType,
                DocumentType = type,
                Data = data
            };

            if (role.ToLower() == "broker")
            {
                var broker = _context.Brokers.Single(b => b.BrokerId == userId);

                if (type == DocumentType.W9)
                {
                    broker.DocumentW9 = document;
                }
                else
                {
                    broker.DocumentOther = document;
                }
            }

            if (role.ToLower() == "agent")
            {
                var agent = _context.Agents.Single(a => a.AgentId == userId);

                if (type == DocumentType.W9)
                {
                    agent.DocumentW9 = document;
                }
                else
                {
                    agent.DocumentOther = document;
                }
            }

            _context.SaveChanges();
        }

        public DocumentModel Get(int id)
        {
            return _context.Documents.Single(d => d.DocumentId == id);
        }
    }
}
