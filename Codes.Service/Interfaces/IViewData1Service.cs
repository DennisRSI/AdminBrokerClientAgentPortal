using Codes1.Service.Models;
using Codes1.Service.ViewModels;
using System.Collections.Generic;

namespace Codes1.Service.Interfaces
{
    public interface IViewData1Service
    {
        ClientDetailsViewModel GetClientDetails(int clientId);
    }
}
