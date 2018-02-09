using Codes.Service.Models;
using System.Collections.Generic;

namespace Codes.Service.Interfaces
{
    public interface IVideoService
    {
        IEnumerable<VideoModel> Get();
        IEnumerable<VideoModel> Get(bool isPreLogin);
        VideoModel Get(int id);
    }
}
