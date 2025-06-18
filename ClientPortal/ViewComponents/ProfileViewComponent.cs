using AutoMapper;
using ClientPortal.Models;
using ClientPortal.Models._ViewModels;
using Codes1.Service.Interfaces;
using Codes1.Service.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClientPortal.ViewComponents
{
    public class ProfileViewComponent : ViewComponent
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ICode1Service _context;
        private readonly IMapper _mapper;
        private readonly IAccount1Service _accountService;

        public ProfileViewComponent(ICode1Service context, 
            SignInManager<ApplicationUser> signInManager, 
            UserManager<ApplicationUser> userManager,
            IMapper mapper,
            IAccount1Service accountService)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _context = context;
            _mapper = mapper;
            _accountService = accountService;
        }

        public async Task<IViewComponentResult> InvokeAsync(string profileId)
        {
            var user = await _userManager.FindByIdAsync(profileId);
            var model = _mapper.Map<ApplicationUser, ProfileViewModel>(user);

            if (user.Role == "Broker")
            {
                var broker = await _context.GetBrokerByAccountId(profileId);
                model.DocumentW9Id = broker.DocumentW9Id;
            }

            if (User.IsInRole("Administrator") || User.IsInRole("Super Administrator") || User.IsInRole("Broker"))
            {
                model.IsDisabled = false;
            }

            if (user.Role == "Agent")
            {
                var loggedInUser = await _userManager.GetUserAsync(HttpContext.User);
                var agent = await _context.GetAgentByAccountId(profileId);
                model.DeactivationDate = agent.DeactivationDate;
                model.DeactivationReason = agent.DeactivationReason;
                model.ShowAgentControls = loggedInUser.Role != "Agent";

                model.Agents = _accountService.GetAgentsOfBroker(model.BrokerId).Where(a => a.Id != agent.AgentId).Select(
                                    a => new SelectListItem()
                                    {
                                        Text = a.FullName,
                                        Value = a.Id.ToString()
                                    }
                                );

                if (String.IsNullOrWhiteSpace(agent.ParentAgentName))
                {
                    model.ParentAgentName = "N/A";
                }
                else
                {
                    model.ParentAgentName = agent.ParentAgentName;
                    model.ParentAgentId = agent.ParentAgentId;
                }
            }

            var account = _accountService.GetAccountCommon(profileId);
            model.CommissionRate = account.CommissionRate;

            return View(model);
        }
    }
}
