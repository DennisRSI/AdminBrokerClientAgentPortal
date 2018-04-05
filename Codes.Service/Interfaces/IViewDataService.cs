using Codes.Service.Models;
using Codes.Service.ViewModels;
using System.Collections.Generic;

namespace Codes.Service.Interfaces
{
    public interface IViewDataService
    {
        ClientDetailsViewModel GetClientDetails(int clientId);
    }
}
