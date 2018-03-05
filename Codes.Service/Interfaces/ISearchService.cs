using Codes.Service.ViewModels;

namespace Codes.Service.Interfaces
{
    public interface ISearchService
    {
        SearchViewModel GetAdmin(string query);
    }
}
