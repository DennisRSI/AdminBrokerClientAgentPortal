using Codes1.Service.ViewModels;

namespace Codes1.Service.Interfaces
{
    public interface ISearch1Service
    {
        SearchViewModel Search(string query, string accountType, int accountId);
    }
}
