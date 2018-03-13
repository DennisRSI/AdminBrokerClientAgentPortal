using Codes.Service.ViewModels;

namespace Codes.Service.Interfaces
{
    public interface IPurchaseService
    {
        void Purchase(string brokerReference, PurchaseViewModel model);
    }
}
