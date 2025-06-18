using Codes1.Service.ViewModels;

namespace Codes1.Service.Interfaces
{
    public interface IDashboardDistribution1Service
    {
        DataTableViewModel<CardDistributionViewModel> GetAdmin();
        DataTableViewModel<CardDistributionViewModel> GetBroker(int brokerId);
        DataTableViewModel<CardDistributionViewModel> GetAgent(int agentId);
        DataTableViewModel<CardDistributionViewModel> GetClient(int clientId);
    }
}
