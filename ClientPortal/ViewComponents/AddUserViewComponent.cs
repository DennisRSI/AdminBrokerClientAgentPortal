﻿using ClientPortal.Models;
using ClientPortal.Models._ViewModels;
using Codes1.Service.Domain;
using Codes1.Service.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;
using System.Threading.Tasks;

namespace ClientPortal.ViewComponents
{
    public class AddUserViewComponent : ViewComponent
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ICode1Service _codeService;
        private readonly IAccount1Service _accountService;

        public AddUserViewComponent(
            SignInManager<ApplicationUser> signInManager,
            UserManager<ApplicationUser> userManager,
            ICode1Service codeService,
            IAccount1Service accountService)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _codeService = codeService;
            _accountService = accountService;
        }

        public async Task<IViewComponentResult> InvokeAsync(int brokerId, string userType)
        {
            if (_signInManager.IsSignedIn(HttpContext.User))
            {
                var model = new AddUserViewModel()
                {
                    BrokerId = brokerId,
                    UserType = userType
                };

                if (brokerId > 0)
                {
                    var broker = await _codeService.GetBrokerById(brokerId);

                    switch (userType)
                    {
                        case "Agent":
                            model.CommissionRate = (decimal)broker.AgentCommissionPercentage;
                            model.Agents = _accountService.GetAgentsOfBroker(brokerId).Select(
                                    a => new SelectListItem() {
                                        Text = a.FullName, Value = a.Id.ToString()
                                    }
                                );
                            break;

                        case "Client":
                            model.CommissionRate = (decimal)broker.ClientCommissionPercentage;
                            break;
                    }
                }

                return await Task.FromResult(View(model));
            }

            return null;
        }
    }
}
