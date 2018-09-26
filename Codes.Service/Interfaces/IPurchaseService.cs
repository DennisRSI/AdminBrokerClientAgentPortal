using Codes.Service.ViewModels;
using System.Linq;

namespace Codes.Service.Interfaces
{
    public interface IPurchaseService
    {
        PurchaseDisplayViewModel Purchase(string brokerReference, PurchaseViewModel model);
        IQueryable<PurchaseDisplayViewModel> GetList(int brokerId);
        PurchaseDisplayViewModel GetDetails(int purchaseId);
    }
}
