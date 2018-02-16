using Codes.Service.ViewModels;

namespace Codes.Service.Interfaces
{
    public interface ICampaignService
    {
        DataTableViewModel<CampaignViewModel> GetByClient(int id, int draw, int startRowIndex, int numberOfRows, string sortColumn, string sortDirection, string searchValue);
    }
}
