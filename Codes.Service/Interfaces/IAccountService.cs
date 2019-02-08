using Codes.Service.ViewModels;
using System.Collections.Generic;

namespace Codes.Service.Interfaces
{
    public interface IAccountService
    {
        int GetIdFromReference(string reference);
        IEnumerable<MyClientViewModel> GetClientsByBroker(int brokerId);
        ClientEditViewModel GetClientEdit(int clientId);

        IEnumerable<MyAgentViewModel> GetAgentsByAgent(int agentId);
        IEnumerable<MyAgentViewModel> GetAgentsByBroker(int brokerId);

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
        void DeactivateAgent(int agentId, string reason);

        // Client
        IEnumerable<AccountViewModel> GetCampaignsOfClient(int clientId);
        void DeactivateClient(int clientId, string reason);
        int GetCardQuantityByClient(int clientId, string cardType = "");
        void AddAgentToClient(int clientId, int agentId);
        void RemoveAgentFromClient(int clientAgentId);

        AccountCommonModel GetAccountCommon(string reference);
    }
}
