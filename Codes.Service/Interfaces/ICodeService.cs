using Codes.Service.Models;
using Codes.Service.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Codes.Service.Interfaces
{
    public interface ICodeService
    {
        Task<(CardsTotalViewModel model, string message, bool isSuccess)> GetCardTotalsForBroker(int brokerId);
        Task<CodeActivityModel> GetActivityByRSIId(int rsiId);
        Task<string> GetCodeByRSIId(int rsiId);
        Task<int> GetRSIIdByActivationCode(string activationCode, int issuer);
        Task<CodeModel> GetCodeByActivationCode(string activationCode, string issuer);
        Task<string> AddNewCode(int rsiId, string code, string creatorIP);
        CodeLookupViewModel LookupCodeOrRSIId(CodeLookupViewModel model);
        Task<CodeLookupViewModel> LookupCodeOrRSIIdAsync(CodeLookupViewModel model);
        Task<CodeLookupViewModel> LookupRSIIdByCode(string code, int issuer);
        CodeActivityModel GetCodeActivityByUsernamePassword(string username, string password, string issuer);

        Task<CodeActivityModel> GetCodeActivityByUsernamePasswordAsync(string username, string password, string issuer);
        Task<CodeActivityModel> GetCodeActivityByUsernameAsync(string username, string issuer);
        Task<CodeActivityModel> GetCodeActivityByEmailAsync(string email, string issuer);
        Task<CodeActivityModel> GetCodeActivityByCodeAsync(string activationCode, string issuer);

        ActivationsViewModel GetActivations(string issuer, string[] codes = null, int startRowIndex = 0, int numberOfRows = 10
            , DateTime? startDate = null, DateTime? endDate = null, string status = "", string dateSerchType = "");

        Task<ActivationLineItemsViewModel> GetActivationLineItemsAsync(string issuer, string[] codes = null, int startRowIndex = 0, int numberOfRows = 10
            , DateTime? startDate = null, DateTime? endDate = null, string status = "", string dateSerchType = "");
        Task<ActivationsViewModel> GetActivationsAsync(string issuer, string[] codes = null, int startRowIndex = 0, int numberOfRows = 10
            , DateTime? startDate = null, DateTime? endDate = null, string status = "", string dateSerchType = "");

        string AddBulkCodes(CodesViewModel codesToAdd, int bulkCodeId = 0, int batchCount = 1000);
        Task<string> AddBulkCodesAsync(CodesViewModel codesToAdd, int bulkCodeId = 0, int batchCount = 1000);

        string AddRangeCodes(string startRange, string endRange, string issuer, int hotelPoints, int condoRewards = 0, int numberOfUses = 1
            , decimal chargeAmount = 0, DateTime? startDate = null, DateTime? endDate = null);
        Task<string> AddRangeCodesAsync(string startRange, string endRange, string issuer, int hotelPoints, int condoRewards = 0, int numberOfUses = 1
            , decimal chargeAmount = 0, DateTime? startDate = null, DateTime? endDate = null);

        string AddBulkCodesCallback(CodesViewModel codesToAdd);
        Task<string> AddBulkCodesCallbackAsync(CodesViewModel codesToAdd);

        string GetBulkCodesCallbackById(int bulkCodeAuditId, string issuer);
        Task<string> GetBulkCodesCallbackByIdAsync(int bulkCodeAuditId, string issuer);
        ActivationViewModel GetActivationInfoByCodeActivityId(int codeActivityId);
        Task<ActivationViewModel> GetActivationInfoByCodeActivityIdAsync(int codeActivityId);
        ActivationViewModel GetActivationInfo(string code, string issuer);
        Task<ActivationViewModel> GetActivationInfoAsync(string code, string issuer);
        string UpdateActivityRSIId(int codeActivityId, int rsiId);
        Task<string> UpdateActivityRSIIdAsync(int codeActivityId, int rsiId);
        ActivationViewModel VerifyEmail(int codeActivityId);
        Task<ActivationViewModel> VerifyEmailAsync(int codeActivityId);
        string AddRSIIdToCodeActivity(int codeActivityId, int rsiId);
        Task<string> AddRSIIdToCodeActivityAsync(int codeActivityId, int rsiId);
        ActivationModel ActivateCode(ActivationModel model);
        Task<ActivationModel> ActivateCodeAsync(ActivationModel model);


        
        string AddBulkCodesAudit(CodesViewModel codesToAdd);
        Task<string> AddBulkCodesAuditAsync(CodesViewModel codesToAdd);

        string UpdateBulkCodesAudit(int bulkCodeAuditId, int numberProcessed, int numberSucceeded, int numberFailed, List<KeyValuePair<string, string>> errors);
        Task<string> UpdateBulkCodesAuditAsync(int bulkCodeAuditId, int numberProcessed, int numberSucceeded, int numberFailed, List<KeyValuePair<string, string>> errors);

        string FinishBulkCodesAudit(int bulkCodeAuditId);
        Task<string> FinishBulkCodesAuditAsync(int bulkCodeAuditId);
        Task<BrokerViewModel> BrokerAdd(BrokerViewModel model);
        Task<BrokerViewModel> BrokerUpdate(BrokerViewModel model);
        Task<AgentViewModel> AgentAdd(AgentViewModel model);
        Task<AgentViewModel> AgentUpdate(AgentViewModel model);
        Task<ClientViewModel> ClientAdd(ClientViewModel model);
        Task<ClientViewModel> ClientUpdate(ClientViewModel model);
        Task<CampaignViewModel> CampaignAdd(CampaignViewModel model);
        Task<CampaignViewModel> CampaignUpdate(CampaignViewModel model);
        Task<CampaignCodeRangeViewModel> CampaignCodeRangeAdd(CampaignCodeRangeViewModel model);
        Task<string> CampaignCodeRangeDelete(int campaignId, int codeRangeId);
        Task<CampaignAgentViewModel> CampaignAgentAdd(CampaignAgentViewModel model);
        Task<string> CampaignAgentDeleteAll(int campaignId);
        //Task<(bool isSuccess, int codeRangeId, int codeActivationId, string message)> IsCodeInRange(int rsiOrgId, string preAlpha, string postAlpha, int numericValue);
        Task<BrokerViewModel> GetBrokerById(int brokerId);
        Task<BrokerViewModel> GetBrokerByAccountId(string accountId);
        Task<DataTableViewModel<BrokerListViewModel>> GetBrokers(int draw, int startRowIndex = 0, int numberOfRows = 10, string searchValue = null, string sortColumn = "DEFAULT", string sortDirection = "ASC", bool onlyActive = false);
        Task<DataTableViewModel<BrokerListViewModel>> GetBrokers();
        Task<AgentViewModel> GetAgentById(int agentId);
        Task<AgentViewModel> GetAgentByAccountId(string accountId);
        Task<DataTableViewModel<AgentListViewModel>> GetAgents(int draw, int brokerId, int startRowIndex = 0, int numberOfRows = 10, string searchValue = null, int? campaignId = null, string sortColumn = "DEFAULT", string sortDirection = "ASC");
        Task<ClientViewModel> GetClientById(int clientId);
        Task<(int count, string message, bool isSuccess)> GetClientCampaignCount(int clientId);
        Task<List<(int agentId, string agentName)>> GetAgentsAssignedToClient(int clientId);
        Task<ClientViewModel> GetClientByAccountId(string accountId);
        Task<DataTableViewModel<ClientListViewModel>> GetClients(int draw, int brokerId, int startRowIndex = 0, int numberOfRows = 10, string searchValue = null, string sortColumn = "DEFAULT", string sortDirection = "ASC");
        //Task<CampaignViewModel> GetCampaignById(int brokerId);
        //Task<ListViewModel<CampaignViewModel>> GetCampaigns(int brokerId, int startRowIndex = 0, int numberOfRows = 10, string sortColumn = "DEFAULT");
        Task<ListViewModel<CampaignViewModel>> GetCampaigns(int brokerId, int startRowIndex = 0, int numberOfRows = 10, string sortColumn = "DEFAULT");
        Task<CampaignViewModel> GetCampaignById(int brokerId);
        Task<BrokerViewModel> GetBrokerByReference(string referenceId);
        Task<AgentViewModel> GetAgentByReference(string referenceId);
        Task<ClientViewModel> GetClientByReference(string referenceId);
        Task<string> BrokerAddAppReference(int brokerId, string referenceId);
        Task<string> AgentAddAppReference(int agentId, string referenceId);
        Task<string> ClientAddAppReference(int clientId, string referenceId);
        Task<DataTableViewModel<CodeListViewModel>> GetBrokerCodes(int draw, int brokerId, int startRowIndex = 0, int numberOfRows = 10, string searchValue = null, string sortColumn = "DEFAULT", string sortDirection = "ASC");
    }
}
