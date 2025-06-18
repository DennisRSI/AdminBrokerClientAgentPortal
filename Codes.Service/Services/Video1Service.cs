using Codes1.Service.Data;
using Codes1.Service.Interfaces;
using Codes1.Service.Models;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;

namespace Codes1.Service.Services
{
    public class Video1Service : IVideo1Service
    {
        private readonly ILogger _logger;
        private readonly Codes1DbContext _context;

        public Video1Service(Codes1DbContext context, ILoggerFactory loggerFactory)
        {
            _context = context;
            _logger = loggerFactory.CreateLogger<Code1Service>();
        }

        public IEnumerable<VideoModel> Get()
        {
            return _context.Videos.Where(v => v.IsActive);
        }

        public IEnumerable<VideoModel> Get(bool isPreLogin)
        {
            return _context.Videos.Where(v => v.IsActive && v.IsPreLogin == isPreLogin);
        }

        public VideoModel Get(int id)
        {
            return _context.Videos.SingleOrDefault(x => x.VideoId == id);
        }
    }
}
