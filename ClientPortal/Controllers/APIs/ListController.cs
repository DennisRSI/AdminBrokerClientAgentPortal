using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClientPortal.Models;
using ClientPortal.Models._ViewModels;
using Codes.Service.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Dynamic;
using Codes.Service.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

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

        private async Task<DataTableViewModel<AdminListViewModel>> _GetAdminData(int startRowIndex, int numberOfRows, int draw, string sortColumn, string sortDirection, string searchValue, string role)
        {
            DataTableViewModel<AdminListViewModel> model = new DataTableViewModel<AdminListViewModel>();
            try
            {
                model.Draw = draw;

                IEnumerable<ApplicationUser> tmp = (await _userManager.GetUsersInRoleAsync(role)) as IEnumerable<ApplicationUser>;

                model.RecordsTotal = tmp.Count();

                if (!string.IsNullOrEmpty(searchValue))
                {
                    tmp = tmp.Where(x => x.Id == searchValue
                        || x.CompanyName.Contains(searchValue)
                        || x.Email.Contains(searchValue)
                        || x.FirstName.Contains(searchValue)
                        || x.LastName.Contains(searchValue)
                        || x.PhoneNumber.Contains(searchValue)
                        || x.OfficePhone.Contains(searchValue)
                        || x.Fax.Contains(searchValue));
                }

                if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortDirection)))
                {
                    switch (sortColumn)
                    {
                        case "company_name":
                            if (sortDirection.ToUpper() == "ASC")
                                tmp = tmp.OrderBy(o => o.CompanyName);
                            else
                                tmp = tmp.OrderByDescending(o => o.CompanyName);
                            break;
                        case "email":
                            if (sortDirection.ToUpper() == "ASC")
                                tmp = tmp.OrderBy(o => o.Email);
                            else
                                tmp = tmp.OrderByDescending(o => o.Email);
                            break;
                        case "phone":
                            if (sortDirection.ToUpper() == "ASC")
                                tmp = tmp.OrderBy(o => o.PhoneNumber);
                            else
                                tmp = tmp.OrderByDescending(o => o.PhoneNumber);
                            break;
                        case "activation_date":
                            if (sortDirection.ToUpper() == "ASC")
                                tmp = tmp.OrderBy(o => o.CreationDate);
                            else
                                tmp = tmp.OrderByDescending(o => o.CreationDate);
                            break;
                        case "deactivation_date":
                            if (sortDirection.ToUpper() == "ASC")
                                tmp = tmp.OrderBy(o => o.DeactivationDate);
                            else
                                tmp = tmp.OrderByDescending(o => o.DeactivationDate);
                            break;
                        default:
                            if (sortDirection.ToUpper() == "ASC")
                                tmp = tmp.OrderBy(o => o.FirstName);
                            else
                                tmp = tmp.OrderByDescending(o => o.FirstName);
                            break;
                    }
                    //tmp = tmp.OrderBy(search + " " + sortDirection);
                }

                model.RecordsFiltered = tmp.Count();

                model.Data = (from t in tmp
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
                              }).Skip(startRowIndex).Take(numberOfRows).ToArray();
            }
            catch (Exception ex)
            {
                model.Message = $"Error: {ex.Message}";
            }

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
            DataTableViewModel<AdminListViewModel> model = new DataTableViewModel<AdminListViewModel>();

            try
            {
                (int draw, int startRowIndex, int numberOfRows, string sortColumn, string sortDirection, string searchValue) = _ParseForm(Request.Form);
                
                model = await _GetAdminData(startRowIndex, numberOfRows, draw, sortColumn, sortDirection, searchValue, "Super Administrator");
            }
            catch (Exception ex)
            {
                model.Message = $"Error: {ex.Message}";
            }

            return model;
        }
        // GET: api/<controller>
        [HttpPost("admin")]
        public async Task<DataTableViewModel<AdminListViewModel>> Admin()
        {
            DataTableViewModel<AdminListViewModel> model = new DataTableViewModel<AdminListViewModel>();

            try
            {
                (int draw, int startRowIndex, int numberOfRows, string sortColumn, string sortDirection, string searchValue) = _ParseForm(Request.Form);

                model = await _GetAdminData(startRowIndex, numberOfRows, draw, sortColumn, sortDirection, searchValue, "Administrator");
                
            }
            catch (Exception ex)
            {
                model.Message = $"Error: {ex.Message}";
            }

            return model;
        }

        [HttpPost("client/{brokerId}/{campaignId}")]
        public async Task<DataTableViewModel<ClientListViewModel>> Client(int brokerId, int campaignId)
        {
            DataTableViewModel<ClientListViewModel> model = new DataTableViewModel<ClientListViewModel>();

            try
            {
                (int draw, int startRowIndex, int numberOfRows, string sortColumn, string sortDirection, string searchValue) = _ParseForm(Request.Form);

                model = await _context.GetClients(draw, brokerId, startRowIndex, numberOfRows, searchValue, sortColumn, sortDirection);
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
                (int draw, int startRowIndex, int numberOfRows, string sortColumn, string sortDirection, string searchValue) = _ParseForm(Request.Form);

                model = await _context.GetBrokers(draw, startRowIndex, numberOfRows, searchValue, sortColumn, sortDirection);
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
