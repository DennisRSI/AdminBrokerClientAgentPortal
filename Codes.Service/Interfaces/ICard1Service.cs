using Codes1.Service.ViewModels;
using System.Threading.Tasks;

namespace Codes1.Service.Interfaces
{
    public interface ICard1Service
    {
        Task<CardDetailsViewModel> GetDetails(string code);
    }
}
