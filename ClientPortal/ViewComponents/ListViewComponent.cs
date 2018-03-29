using ClientPortal.Models;
using ClientPortal.Models._ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ClientPortal.ViewComponents
{
    public class ListViewComponent : ViewComponent
    {
        private readonly SignInManager<ApplicationUser> _signInManager;

        public ListViewComponent(SignInManager<ApplicationUser> signInManager)
        {
            _signInManager = signInManager;
        }

        public async Task<IViewComponentResult> InvokeAsync(string userType = "Client", int brokerId = 0, int clientId = 0)
        {
            if (_signInManager.IsSignedIn(HttpContext.User))
            {
                PageViewModel model = new PageViewModel()
                {
                    Role = userType,
                    BrokerId = brokerId,
                    ClientId = clientId
                };

                switch (userType)
                {
                    case "Administrator":
                    case "Super Administrator":
                        return await Task.FromResult(View("AdminList", model));

                    case "Broker":
                        return await Task.FromResult(View("BrokerList", model));

                    case "Agent":
                        return await Task.FromResult(View("AgentList", model));

                    case "Client":
                        return await Task.FromResult(View("ClientList", model));

                    case "Purchases":
                        return await Task.FromResult(View("PurchaseList", model));

                    case "Campaigns":
                        return await Task.FromResult(View("CampaignList", model));
                }
            }

            return null;
        }
    }
}
