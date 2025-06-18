using Codes1.Service.ViewModels;

namespace Codes1.Service.Interfaces
{
    public interface IDashboard1Service
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
