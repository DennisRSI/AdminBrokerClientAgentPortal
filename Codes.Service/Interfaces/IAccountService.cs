using Codes.Service.ViewModels;
using System.Collections.Generic;

namespace Codes.Service.Interfaces
{
    public interface IAccountService
    {
        int GetIdFromReference(string reference);
        IEnumerable<MyClientViewModel> GetClientsByBroker(int brokerId);
        ClientEditViewModel GetClientEdit(int clientId);
    }
}
