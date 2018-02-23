using Codes.Service.ViewModels;

namespace Codes.Service.Interfaces
{
    public interface ICampaignService
    {
        void Clone(int campaignId);
        void Create(int clientId, CampaignViewModel viewModel);
        DataTableViewModel<CampaignViewModel> GetByClient(int id);
    }
}
