using Codes.Service.Data;
using Codes.Service.Interfaces;
using Codes.Service.Models;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;

namespace Codes.Service.Services
{
    public class VideoService : IVideoService
    {
        private readonly ILogger _logger;
        private readonly CodesDbContext _context;

        public VideoService(CodesDbContext context, ILoggerFactory loggerFactory)
        {
            _context = context;
            _logger = loggerFactory.CreateLogger<CodeService>();
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
