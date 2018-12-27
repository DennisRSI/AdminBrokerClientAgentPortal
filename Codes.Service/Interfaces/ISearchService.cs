using Codes.Service.ViewModels;

namespace Codes.Service.Interfaces
{
    public interface ISearchService
    {
        SearchViewModel Search(string query, string accountType, int accountId);
    }
}
