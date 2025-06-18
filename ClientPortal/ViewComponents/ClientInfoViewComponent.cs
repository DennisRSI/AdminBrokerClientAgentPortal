using ClientPortal.Models;
using Codes1.Service.Interfaces;
using Codes1.Service.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClientPortal.ViewComponents
{
    public class ClientInfoViewComponent : ViewComponent
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ICode1Service _context;
        public ClientInfoViewComponent(ICode1Service context, SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync(string accountId)
        {
            ClientInfoViewModel model = new ClientInfoViewModel();

            try
            {
                if (_signInManager.IsSignedIn(HttpContext.User))
                { 
                    ClientViewModel res = await _context.GetClientByReference(accountId);
                    if (res != null && res.ClientId > 0)
                    {
                        (int count, string message, bool isSuccess) = await _context.GetClientCampaignCount(res.ClientId);
                        List<(int agentId, string agentName)> agents = await _context.GetAgentsAssignedToClient(res.ClientId);
                        BrokerViewModel broker = await _context.GetBrokerById(res.Broker.BrokerId);
                        int brokerId = 0;
                        string brokerName = "";
                        if(broker != null && broker.BrokerId > 0)
                        {
                            brokerId = broker.BrokerId;
                            
                            if((broker.BrokerFirstName != null && broker.BrokerFirstName.Length > 0) 
                                ||(broker.BrokerLastName != null && broker.BrokerLastName.Length > 0))
                            {
                                if (broker.BrokerFirstName != null && broker.BrokerFirstName.Length > 0)
                                    brokerName = broker.BrokerFirstName;

                                if(broker.BrokerLastName != null && broker.BrokerLastName.Length > 0)
                                {
                                    if (brokerName.Length > 0)
                                        brokerName += " ";

                                    brokerName += broker.BrokerLastName;
                                }
                                
                            }
                            else
                            {
                                if (broker.CompanyName != null && broker.CompanyName.Length > 0)
                                    brokerName = broker.CompanyName;
                            }
                        }

                        model = new ClientInfoViewModel()
                        {
                            Address = res.Address,
                            AssignedAgents = res.AssignedAgents,
                            CampaignCount = count,
                            City = res.City,
                            ClientId = res.ClientId,
                            ClientNotes = "",
                            CompanyName = res.CompanyName,
                            Country = res.Country,
                            CreationDate = res.CreationDate,
                            Email = res.Email,
                            FirstName = res.ContactFirstName,
                            LastName = res.ContactLastName,
                            Message = "Success",
                            MiddleName = res.ContactMiddleName,
                            MobilePhone = res.MobilePhone,
                            OfficeExtension = res.OfficeExtension,
                            OfficePhone = res.OfficePhone,
                            PostalCode = res.PostalCode,
                            State = res.State,
                            BrokerId = brokerId,
                            BrokerName = brokerName
                        };

                        return View(model);
                    }
                    else
                    {
                        model.Message = "Error: Client not found";
                    }
                }
                else
                {
                    model.Message = "Error: Not signed in";
                }
            
            }
            catch (Exception ex)
            {
                if (model == null)
                    model = new ClientInfoViewModel();

                model.Message = $"Error: {ex.Message}";
            }

            return null;
            
        }
    }
}
