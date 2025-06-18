using Codes1.Service.Models;
using System.Collections.Generic;

namespace Codes1.Service.Interfaces
{
    public interface IVideo1Service
    {
        IEnumerable<VideoModel> Get();
        IEnumerable<VideoModel> Get(bool isPreLogin);
        VideoModel Get(int id);
    }
}
