using Codes1.Service.ViewModels;
using System.Threading.Tasks;

namespace Codes1.Service.Interfaces
{
    public interface ICampaign1Service
    {
        void Clone(int campaignId);
        Task<string> Create(int clientId, CampaignViewModel viewModel);
        void Deactivate(int campaignId, string reason);
        DataTableViewModel<CampaignViewModel> GetByClient(int id);
    }
}
