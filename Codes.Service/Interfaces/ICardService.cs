using Codes.Service.ViewModels;

namespace Codes.Service.Interfaces
{
    public interface ICardService
    {
        CardDetailsViewModel GetDetails(int id);
    }
}
