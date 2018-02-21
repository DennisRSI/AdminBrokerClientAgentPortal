using Codes.Service.ViewModels;

namespace Codes.Service.Interfaces
{
    public interface ICampaignService
    {
        DataTableViewModel<CampaignViewModel> GetByClient(int id);
        void Create(int clientId, CampaignViewModel viewModel);
    }
}
