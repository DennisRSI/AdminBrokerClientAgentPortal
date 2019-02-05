using Codes.Service.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClientPortal.Services._Interfaces
{
    public interface IUserService
    {
        
        Task<BrokerViewModel> BrokerAdd(BrokerViewModel model, string password);
        Task<BrokerViewModel> BrokerUpdate(BrokerViewModel model);
        Task<AgentViewModel> AgentAdd(AgentViewModel model, string password);
        Task<AgentViewModel> AgentUpdate(AgentViewModel model);
        Task<ClientViewModel> ClientAdd(ClientViewModel model, string password);
        Task<ClientViewModel> ClientUpdate(ClientViewModel model);
        Task<AdminViewModel> AdminAdd(AdminViewModel model, string password);
        Task<AdminViewModel> SuperAdminAdd(AdminViewModel model, string password);
        Task<AdminViewModel> AdminUpdate(AdminViewModel model);
        Task<ResultViewModel> ChangePassword(string id, string password);
        Task<ResultViewModel> ChangePasswordClient(string id, string password);
    }
}
