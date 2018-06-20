using Codes.Service.ViewModels;
using System.Collections.Generic;

namespace Codes.Service.Interfaces
{
    public interface IAccountService
    {
        int GetIdFromReference(string reference);
        IEnumerable<MyClientViewModel> GetClientsByBroker(int brokerId);
        ClientEditViewModel GetClientEdit(int clientId);

        // Admin
        IEnumerable<AccountViewModel> GetAllBrokers();
        IEnumerable<AccountViewModel> GetAllClients();
        IEnumerable<AccountViewModel> GetAllAgents();

        // Broker
        IEnumerable<AccountViewModel> GetClientsOfBroker(int brokerId);
        IEnumerable<AccountViewModel> GetAgentsOfBroker(int brokerId);
        IEnumerable<AccountViewModel> GetCampaignsOfBroker(int brokerId);

        // Agent
        IEnumerable<AccountViewModel> GetClientsOfAgent(int agentId);
        IEnumerable<AccountViewModel> GetCampaignsOfAgent(int agentId);

        // Client
        IEnumerable<AccountViewModel> GetCampaignsOfClient(int clientId);

        AccountCommonModel GetAccountCommon(string reference);
    }
}
