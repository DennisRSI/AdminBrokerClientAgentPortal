using Codes1.Service.ViewModels;
using System.Linq;

namespace Codes1.Service.Interfaces
{
    public interface IPurchase1Service
    {
        PurchaseDisplayViewModel Purchase(string brokerReference, PurchaseViewModel model);
        IQueryable<PurchaseDisplayViewModel> GetList(int brokerId);
        PurchaseDisplayViewModel GetDetails(int purchaseId);
    }
}
