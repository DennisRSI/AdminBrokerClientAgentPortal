using System;
using System.Linq;
using System.Threading.Tasks;
using ClientPortal.Models;
using Codes.Service.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Codes.Service.Interfaces;

namespace ClientPortal.Controllers.APIs
{
    [Route("api/[controller]")]
    public class ListController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ICodeService _context;
        public ListController(ICodeService context, UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
            _context = context;
        }

        private async Task<DataTableViewModel<AdminListViewModel>> _GetAdminData(string role)
        {
            var model = new DataTableViewModel<AdminListViewModel>();
            var users = (await _userManager.GetUsersInRoleAsync(role));

            model.Data = (from t in users
                            select new AdminListViewModel
                            {
                                Company = t.CompanyName,
                                AccountId = t.Id,
                                ActivationDate = t.CreationDate,
                                DeactivationDate = t.DeactivationDate,
                                Email = t.Email,
                                FirstName = t.FirstName,
                                LastName = t.LastName,
                                Extension = t.MobilePhone != null && t.MobilePhone.Length > 0 ? "" : t.OfficeExtension,
                                Phone = t.MobilePhone != null && t.MobilePhone.Length > 0 ? t.MobilePhone : t.OfficePhone
                            }).ToArray();

            return model;
        }

        private (int draw, int startRowIndex, int numberOfRows, string sortColumn, string sortDirection, string searchValue) _ParseForm(Microsoft.AspNetCore.Http.IFormCollection form)
        {
            (int draw, int startRowIndex, int numberOfRows, string sortColumn, string sortDirection, string searchValue) model = (0, 0, 10, "DEFAULT", "ASC", "");

            try
            {
                int.TryParse(form["start"], out model.startRowIndex);
                int.TryParse(form["length"].FirstOrDefault(), out model.numberOfRows);
                model.sortColumn = form["columns[" + form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault();
                model.sortDirection = form["order[0][dir]"].FirstOrDefault();
                model.searchValue = form["search[value]"].FirstOrDefault();
                int.TryParse(form["draw"].FirstOrDefault(), out model.draw);
            }
            catch (Exception)
            {

                throw;
            }

            return model;
        }
        
        [HttpPost("sa")]
        public async Task<DataTableViewModel<AdminListViewModel>> Sa()
        {
            return await _GetAdminData("Super Administrator");
        }

        [HttpPost("admin")]
        public async Task<DataTableViewModel<AdminListViewModel>> Admin()
        {
            return await _GetAdminData("Administrator");
        }

        [HttpPost("client/{brokerId}/{campaignId}")]
        public async Task<DataTableViewModel<ClientListViewModel>> Client(int brokerId, int campaignId)
        {
            DataTableViewModel<ClientListViewModel> model = new DataTableViewModel<ClientListViewModel>();

            try
            {
                model = await _context.GetClients(brokerId);
            }
            catch (Exception ex)
            {
                model.Message = $"Error: {ex.Message}";
            }

            return model;
        }

        [HttpPost("agent/{brokerId}/{campaignId}")]
        public async Task<DataTableViewModel<AgentListViewModel>> Agent(int brokerId, int campaignId)
        {
            DataTableViewModel<AgentListViewModel> model = new DataTableViewModel<AgentListViewModel>();

            try
            {
                (int draw, int startRowIndex, int numberOfRows, string sortColumn, string sortDirection, string searchValue) = _ParseForm(Request.Form);

                model = await _context.GetAgents(draw, brokerId, startRowIndex, numberOfRows, searchValue, campaignId, sortColumn, sortDirection);
            }
            catch (Exception ex)
            {
                model.Message = $"Error: {ex.Message}";
            }
            
            return model;
        }

        [HttpPost("broker")]
        public async Task<DataTableViewModel<BrokerListViewModel>> Broker()
        {
            DataTableViewModel<BrokerListViewModel> model = new DataTableViewModel<BrokerListViewModel>();

            try
            {
                model = await _context.GetBrokers();
            }
            catch (Exception ex)
            {
                model.Message = $"Error: {ex.Message}";
            }

            return model;
        }

        [HttpPost("purchase/{brokerId}")]
        public async Task<DataTableViewModel<CodeListViewModel>> Purchases(int brokerId)
        {
            DataTableViewModel<CodeListViewModel> model = new DataTableViewModel<CodeListViewModel>();
            try
            {
                (int draw, int startRowIndex, int numberOfRows, string sortColumn, string sortDirection, string searchValue) = _ParseForm(Request.Form);

                model = await _context.GetBrokerCodes(draw, brokerId, startRowIndex, numberOfRows, searchValue, sortColumn, sortDirection);
            }
            catch (Exception ex)
            {
                model.Message = $"Error: {ex.Message}";
            }
            return model;
        }
    }
}
