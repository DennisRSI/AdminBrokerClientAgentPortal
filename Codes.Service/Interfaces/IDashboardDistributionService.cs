using Codes.Service.ViewModels;

namespace Codes.Service.Interfaces
{
    public interface IDashboardDistributionService
    {
        DataTableViewModel<CardDistributionViewModel> GetAdmin();
        DataTableViewModel<CardDistributionViewModel> GetBroker(int brokerId);
        DataTableViewModel<CardDistributionViewModel> GetAgent(int agentId);
        DataTableViewModel<CardDistributionViewModel> GetClient(int clientId);
    }
}
