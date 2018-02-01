using ClientPortal.Models;
using ClientPortal.Models._ViewModels;
using Codes.Service.Interfaces;
using Codes.Service.Services;
using Codes.Service.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
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

                return null;
                
            }
            else
                return null;

            /*
            {
                switch (role)
                {
                    case "Super Administrator":
                    case "Administrator":
                        ListViewModel<AdminListViewModel> adminModel = new ListViewModel<AdminListViewModel>
                        {
                            TotalCount = (await _userManager.GetUsersInRoleAsync(role)).Count()
                        };

                        decimal d = (decimal)adminModel.TotalCount / (decimal)numberOfRows;

                        adminModel.TotalPages = (int)Math.Ceiling(d);
                        
                        var tmp = from t in await _userManager.GetUsersInRoleAsync(role)
                                  select new AdminListViewModel
                                  {
                                      AccountId = t.Id,
                                      ActivationDate = t.CreationDate,
                                      Company = t.CompanyName,
                                      DeactivationDate = t.DeactivationDate,
                                      Email = t.Email,
                                      FirstName = t.FirstName,
                                      LastName = t.LastName,
                                      MiddleName = t.MiddleName,
                                      Extension = t.MobilePhone != null && t.MobilePhone.Length > 0 ? "" : t.OfficeExtension,
                                      Phone = t.MobilePhone != null && t.MobilePhone.Length > 0 ? t.MobilePhone : t.OfficePhone
                                  };

                        switch (sortColumn)
                        {
                            case "LastName":
                                if (sortDirection.ToUpper() == "DESC")
                                    tmp = tmp.OrderByDescending(o => o.LastName);
                                else
                                    tmp = tmp.OrderBy(o => o.LastName);
                                break;
                            case "FirstName":
                                if (sortDirection.ToUpper() == "DESC")
                                    tmp = tmp.OrderByDescending(o => o.FirstName);
                                else
                                    tmp = tmp.OrderBy(o => o.FirstName);
                                break;
                            case "Email":
                                if (sortDirection.ToUpper() == "DESC")
                                    tmp = tmp.OrderByDescending(o => o.Email);
                                else
                                    tmp = tmp.OrderBy(o => o.Email);
                                break;
                            default:
                                if (sortDirection.ToUpper() == "DESC")
                                    tmp = tmp.OrderByDescending(o => o.ActivationDate);
                                else
                                    tmp = tmp.OrderBy(o => o.ActivationDate);
                                break;
                        }

                        adminModel.Items = tmp.Skip(startRowIndex).Take(numberOfRows).ToList();
                        adminModel.Reference = role;
                        return View("AdminList", adminModel);
                        
                    case "Broker":
                        ListViewModel<BrokerListViewModel> brokerModel = await _codeService.GetBrokers(startRowIndex, numberOfRows, sortColumn);
                        return View("BrokerList", brokerModel);
                    case "Client":
                        ListViewModel<ClientListViewModel> clientModel = await _codeService.GetClients(brokerId, startRowIndex, numberOfRows, sortColumn);
                        return View("ClinetList", clientModel);
                }

                return null;
            }

            return null;*/
        }
    }
}
