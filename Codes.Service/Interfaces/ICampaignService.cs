using Codes.Service.ViewModels;

namespace Codes.Service.Interfaces
{
    public interface ICampaignService
    {
        void Clone(int campaignId);
        void Create(int clientId, CampaignViewModel viewModel);
        void Deactivate(int campaignId, string reason);
        DataTableViewModel<CampaignViewModel> GetByClient(int id);
    }
}
