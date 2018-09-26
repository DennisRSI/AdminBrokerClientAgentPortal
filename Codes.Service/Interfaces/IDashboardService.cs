using Codes.Service.ViewModels;

namespace Codes.Service.Interfaces
{
    public interface IDashboardService
    {
        DashboardViewModel GetAdmin();
        DashboardViewModel GetBroker(int id);
        DashboardViewModel GetAgent(int id);
        DashboardViewModel GetClient(int id);

        DashboardSelectViewModel GetListBrokers();
        DashboardSelectViewModel GetListAgents();
        DashboardSelectViewModel GetListClients();
    }
}
