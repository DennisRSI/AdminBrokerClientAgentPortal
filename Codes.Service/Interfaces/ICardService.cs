using Codes.Service.ViewModels;
using System.Threading.Tasks;

namespace Codes.Service.Interfaces
{
    public interface ICardService
    {
        Task<CardDetailsViewModel> GetDetails(string code);
    }
}
