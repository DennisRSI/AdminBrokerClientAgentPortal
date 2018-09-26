using Codes.Service.Data;
using Codes.Service.Domain;
using Codes.Service.Interfaces;
using Codes.Service.Models;
using Codes.Service.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Codes.Service.Services
{
    public class CodeService : ICodeService
    {
        private readonly ILogger _logger;
        private readonly CodesDbContext _context;

        public CodeService(CodesDbContext context, ILoggerFactory loggerFactory)
        {
            _context = context;
            _logger = loggerFactory.CreateLogger<CodeService>();
        }
        
        public async Task<BrokerViewModel> BrokerAdd(BrokerViewModel model)
        {
            try
            {
                BrokerModel broker = new BrokerModel(model);
                broker.CreationDate = DateTime.Now;
                await _context.Brokers.AddAsync(broker);
                await _context.SaveChangesAsync();
                model.Message = "Success";
                model.BrokerId = broker.BrokerId;
                
            }
            catch (Exception ex)
            {

                if (model == null)
                    model = new BrokerViewModel();

                model.Message = $"Error: {ex.Message}";
            }

            return model;
        }
        public async Task<BrokerViewModel> BrokerUpdate(BrokerViewModel model)
        {
            try
            {
                BrokerModel broker = await _context.Brokers.FirstOrDefaultAsync(x => x.BrokerId == model.BrokerId);

                if(broker != null && broker.BrokerId > 0)
                {
                    broker.Address = model.Address;
                    broker.AgentCommissionPercentage = model.AgentCommissionPercentage;
                    broker.BrokerCommissionPercentage = model.BrokerCommissionPercentage;
                    broker.BrokerFirstName = model.BrokerFirstName;
                    broker.BrokerLastName = model.BrokerLastName;
                    broker.City = model.City;
                    broker.ClientCommissionPercentage = model.ClientCommissionPercentage;
                    broker.CompanyName = model.CompanyName;
                    broker.Country = model.Country;
                    broker.DeactivationDate = model.DeactivationDate;
                    broker.DeactivationReason = model.DeactivationReason;
                    broker.EIN = model.EIN;
                    broker.Email = model.Email;
                    broker.Fax = model.Fax;
                    broker.FaxExtension = model.FaxExtension;
                    broker.IsActive = model.IsActive;
                    broker.MobilePhone = model.MobilePhone;
                    broker.OfficeExtension = model.OfficeExtension;
                    broker.OfficePhone = model.OfficePhone;
                    broker.ParentBrokerId = model.ParentBrokerId;
                    broker.PhysicalCardsPercentOfFaceValue1000 = model.PhysicalCardsPercentOfFaceValue1000;
                    broker.PhysicalCardsPercentOfFaceValue10000 = model.PhysicalCardsPercentOfFaceValue10000;
                    broker.PhysicalCardsPercentOfFaceValue100000 = model.PhysicalCardsPercentOfFaceValue100000;
                    broker.PhysicalCardsPercentOfFaceValue25000 = model.PhysicalCardsPercentOfFaceValue25000;
                    broker.PhysicalCardsPercentOfFaceValue5000 = model.PhysicalCardsPercentOfFaceValue5000;
                    broker.PhysicalCardsPercentOfFaceValue50000 = model.PhysicalCardsPercentOfFaceValue50000;
                    broker.PostalCode = model.PostalCode;
                    broker.State = model.State;
                    broker.TimeframeBetweenCapInHours = model.TimeframeBetweenCapInHours;
                    broker.VirtualCardCap = model.VirtualCardCap;
                    broker.ApplicationReference = model.ApplicationReference;

                    await _context.SaveChangesAsync();
                    model.Message = "Success";
                }
                else
                    model.Message = $"Error (CodeService/BrokerUpdate): Broker not found: {model.BrokerId}";
            }
            catch (Exception ex)
            {
                if (model == null)
                    model = new BrokerViewModel();

                model.Message = $"Error: {ex.Message}";
            }

            return model;
        }
        public async Task<AgentViewModel> AgentAdd(AgentViewModel model)
        {
            try
            {
                AgentModel agent = new AgentModel(model);
                await _context.AddAsync(agent);
                await _context.SaveChangesAsync();
                model.Message = "Success";
                model.AgentId = agent.AgentId;
            }
            catch (Exception ex)
            {
                if (model == null)
                    model = new AgentViewModel();

                model.Message = $"Error: {ex.Message}";
            }

            return model;
        }
        public async Task<AgentViewModel> AgentUpdate(AgentViewModel model)
        {
            try
            {
                var agent = await _context.Agents.FirstOrDefaultAsync(x => x.AgentId == model.AgentId);

                if (agent != null && agent.AgentId > 0)
                {
                    agent.AgentId = model.AgentId;
                    agent.BrokerId = model.BrokerId;
                    agent.Address = model.Address;
                    agent.AgentFirstName = model.AgentFirstName;
                    agent.AgentLastName = model.AgentLastName;
                    agent.City = model.City;
                    agent.CompanyName = model.CompanyName;
                    agent.Country = model.Country;
                    agent.DeactivationDate = model.DeactivationDate;
                    agent.DeactivationReason = model.DeactivationReason;
                    agent.EIN = model.EIN;
                    agent.Email = model.Email;
                    agent.Fax = model.Fax;
                    agent.FaxExtension = model.FaxExtension;
                    agent.IsActive = model.IsActive;
                    agent.MobilePhone = model.MobilePhone;
                    agent.OfficeExtension = model.OfficeExtension;
                    agent.OfficePhone = model.OfficePhone;
                    agent.PostalCode = model.PostalCode;
                    agent.State = model.State;
                    agent.ApplicationReference = model.ApplicationReference;

                    await _context.SaveChangesAsync();
                    model.Message = "Success";
                }
                else
                    model.Message = "Error: Agent not found";
            }
            catch (Exception ex)
            {
                if (model == null)
                    model = new AgentViewModel();

                model.Message = $"Error: {ex.Message}";
            }

            return model;
        }
        public async Task<ClientViewModel> ClientAdd(ClientViewModel model)
        {
            try
            {
                ClientModel client = new ClientModel(model);
                await _context.Clients.AddAsync(client);
                await _context.SaveChangesAsync();
                model.ClientId = client.ClientId;
                model.Message = "Success";
            }
            catch (Exception ex)
            {
                if (model == null)
                    model = new ClientViewModel();

                model.Message = $"Error: {ex.Message}";
            }
            return model;
        }
        public async Task<ClientViewModel> ClientUpdate(ClientViewModel model)
        {
            try
            {
                ClientModel client = await _context.Clients.FirstOrDefaultAsync(x => x.ClientId == model.ClientId);

                if(client != null && client.ClientId > 0)
                {
                    client.Address = model.Address;
                    client.City = model.City;
                    client.CommissionRate = model.CommissionRate;
                    client.CompanyName = model.CompanyName;
                    client.ContactFirstName = model.ContactFirstName;
                    client.ContactLastName = model.ContactLastName;
                    client.Country = model.Country;
                    client.DeactivationDate = model.DeactivationDate;
                    client.DeactivationReason = model.DeactivationReason;
                    client.EIN = model.EIN;
                    client.Email = model.Email;
                    client.Fax = model.Fax;
                    client.FaxExtension = model.FaxExtension;
                    client.IsActive = model.IsActive;
                    client.MobilePhone = model.MobilePhone;
                    client.OfficeExtension = model.OfficeExtension;
                    client.OfficePhone = model.OfficePhone;
                    client.PostalCode = model.PostalCode;
                    client.State = model.State;
                    client.AgentId = model.AgentId;

                    await _context.SaveChangesAsync();
                    model.Message = "Success";
                }
                else
                    model.Message = "Error: Client not found";

            }
            catch (Exception ex)
            {
                if (model == null)
                    model = new ClientViewModel();

                model.Message = $"Error: {ex.Message}";
            }

            return model;
        }
        public async Task<CampaignViewModel> CampaignAdd(CampaignViewModel model)
        {
            try
            {
                CampaignModel campaign = new CampaignModel(model);
                await _context.AddAsync(campaign);
                await _context.SaveChangesAsync();
                model.CampaignId = campaign.CampaignId;
                model.Message = "Success";
            }
            catch (Exception ex)
            {
                if (model == null)
                    model = new CampaignViewModel();

                model.Message = $"Error: {ex.Message}";
            }

            return model;
        }
        public async Task<CampaignViewModel> CampaignUpdate(CampaignViewModel model)
        {
            try
            {
                CampaignModel campaign = await _context.Campaigns.FirstOrDefaultAsync(x => x.CampaignId == model.CampaignId);

                if(campaign != null && campaign.CampaignId > 0)
                {
                    campaign.CampaignDescription = model.CampaignDescription;
                    campaign.CampaignName = model.CampaignName;
                    campaign.CampaignType = model.CampaignType;
                    campaign.CustomCSS = model.CustomCSS;
                    campaign.DeactivationDate = model.DeactivationDate;
                    campaign.DeactivationReason = model.DeactivationReason;
                    campaign.EndDateTime = model.EndDateTime;
                    campaign.GoogleAnalyticsCode = model.GoogleAnalyticsCode;
                    campaign.IsActive = model.IsActive;
                    campaign.PackageId = model.PackageId;
                    campaign.StartDateTime = model.StartDateTime;

                    await _context.SaveChangesAsync();
                    model.Message = "Success";
                }
                else
                    model.Message = "Error: Campaign not found";
            }
            catch (Exception ex)
            {
                if (model == null)
                    model = new CampaignViewModel();

                model.Message = $"Error: {ex.Message}";
            }

            return model;
        }
        public async Task<CampaignCodeRangeViewModel> CampaignCodeRangeAdd(CampaignCodeRangeViewModel model)
        {
            try
            {
                CampaignCodeRangeModel tmp = await _context.CampaignCodeRanges.FirstOrDefaultAsync(x => x.CodeRangeId == model.CodeRangeId && model.CampaignId == model.CampaignId);

                if(tmp == null || (tmp.CampaignId < 1 && tmp.CodeRangeId < 1))
                {
                    CampaignCodeRangeModel campaignCodeRangeModel = new CampaignCodeRangeModel(model);
                    await _context.CampaignCodeRanges.AddAsync(campaignCodeRangeModel);
                    await _context.SaveChangesAsync();
                    model.CodeRangeId = campaignCodeRangeModel.CodeRangeId;
                    model.CampaignId = campaignCodeRangeModel.CampaignId;
                }
                else
                {
                    model.CampaignId = tmp.CampaignId;
                    model.CodeRangeId = tmp.CodeRangeId;
                }

                model.Message = "Success";
            }
            catch (Exception ex)
            {
                if (model == null)
                    model = new CampaignCodeRangeViewModel();

                model.Message = $"Error: {ex.Message}";
            }

            return model;
        }
        public async Task<string> CampaignCodeRangeDelete(int CampaignId, int CodeRangeId)
        {
            string message = "";

            try
            {
                CampaignCodeRangeModel campaign = await _context.CampaignCodeRanges.FirstOrDefaultAsync(x => x.CampaignId == CampaignId && x.CodeRangeId == CodeRangeId);
                if(campaign != null && campaign.CampaignId > 0 && campaign.CodeRangeId > 0)
                {
                    _context.CampaignCodeRanges.Remove(campaign);
                    await _context.SaveChangesAsync();
                }

                message = "Success";
            }
            catch (Exception ex)
            {
                message = $"Error: {ex.Message}";
            }

            return message;
        }
        public async Task<CampaignAgentViewModel> CampaignAgentAdd(CampaignAgentViewModel model)
        {
            try
            {
                CampaignAgentModel tmp = await _context.CampaignAgents.FirstOrDefaultAsync(x => x.CampaignId == model.CampaignId && x.AgentId == model.AgentId);

                if (tmp == null || (tmp.CampaignId < 1 && tmp.AgentId < 1))
                {
                    CampaignAgentModel campaignAgent = new CampaignAgentModel(model);
                    await _context.CampaignAgents.AddAsync(campaignAgent);
                    await _context.SaveChangesAsync();
                    model.CampaignId = campaignAgent.CampaignId;
                    model.AgentId = campaignAgent.AgentId;
                }
                else
                {
                    model.CampaignId = tmp.CampaignId;
                    model.AgentId = tmp.AgentId;
                }

                model.Message = "Success";
            }
            catch (Exception ex)
            {
                if (model == null)
                    model = new CampaignAgentViewModel();

                model.Message = $"Error: {ex.Message}";
            }

            return model;
        }
        public async Task<string> CampaignAgentDeleteAll(int campaignId)
        {
            string message = "";
            
            try
            {
                var agents = _context.CampaignAgents.Where(x => x.CampaignId == campaignId);

                foreach (var row in agents)
                    _context.CampaignAgents.Remove(row);

                await _context.SaveChangesAsync();

                message = "Success";
            }
            catch (Exception ex)
            {
                message = $"Error: {ex.Message}";
            }

            return message;
        }

        public async Task<CampaignViewModel> GetCampaignById(int campaignId)
        {
            CampaignViewModel campaign = new CampaignViewModel();

            try
            {
                CampaignModel c = await _context.Campaigns.FirstOrDefaultAsync(x => x.CampaignId == campaignId);
                if (c != null && c.CampaignId > 0)
                {
                    campaign.Broker.BrokerId = c.BrokerId;
                    campaign.CreationDate = c.CreationDate;
                    campaign.CreatorIP = c.CreatorIP;
                    campaign.DeactivationDate = c.DeactivationDate;
                    campaign.DeactivationReason = c.DeactivationReason;
                    campaign.IsActive = c.IsActive;
                    campaign.Message = "Success";
                    campaign.CampaignDescription = c.CampaignDescription;
                    campaign.CampaignId = c.CampaignId;
                    campaign.CampaignName = c.CampaignName;
                    campaign.CampaignType = c.CampaignType;
                    //campaign.CardQuantity = c.q
                    campaign.Client.ClientId = c.ClientId.GetValueOrDefault(0);
                    campaign.CreationDate = c.CreationDate;
                    campaign.CreatorIP = c.CreatorIP;
                    campaign.CustomCSS = c.CustomCSS;
                    campaign.DeactivationDate = c.DeactivationDate;
                    campaign.DeactivationReason = c.DeactivationReason;
                    campaign.EndDateTime = c.EndDateTime;
                    campaign.GoogleAnalyticsCode = c.GoogleAnalyticsCode;
                    campaign.PackageId = c.PackageId;
                    campaign.StartDateTime = c.StartDateTime;

                }
                else
                    c.Message = "Error: Campaign not found";
            }
            catch (Exception ex)
            {
                if (campaign == null)
                    campaign = new CampaignViewModel();

                campaign.Message = $"Error: {ex.Message}";
            }

            return campaign;
        }

        public async Task<ListViewModel<CampaignViewModel>> GetCampaigns(int brokerId, int startRowIndex = 0, int numberOfRows = 10, string sortColumn = "DEFAULT")
        {
            ListViewModel<CampaignViewModel> items = new ListViewModel<CampaignViewModel>();

            try
            {
                var tmp = from c in _context.Campaigns where c.BrokerId == brokerId select c;

                switch (sortColumn)
                {
                    case "AgentFirstName":
                        tmp = tmp.OrderBy(o => o.CampaignName);
                        break;
                    default:
                        tmp = tmp.OrderBy(o => o.CreationDate);
                        break;
                }

                var ct = tmp;

                items.TotalCount = ct.Count();

                items.Items = await (from t in tmp
                                     select new CampaignViewModel
                                     {
                                         CreationDate = t.CreationDate,
                                         CreatorIP = t.CreatorIP,
                                         DeactivationDate = t.DeactivationDate,
                                         IsActive = t.IsActive,
                                         Message = t.Message,
                                         CampaignDescription = t.CampaignDescription,
                                         CampaignId = t.CampaignId,
                                         CampaignName = t.CampaignName,
                                         CampaignType = t.CampaignType,
                                         CustomCSS = t.CustomCSS,
                                         DeactivationReason = t.DeactivationReason,
                                         EndDateTime = t.EndDateTime,
                                         GoogleAnalyticsCode = t.GoogleAnalyticsCode,
                                         PackageId = t.PackageId,
                                         StartDateTime = t.StartDateTime
                                     }).Skip(startRowIndex).Take(numberOfRows).ToListAsync();
                items.Message = "Success";
            }
            catch (Exception ex)
            {
                items.Message = $"Error: {ex.Message}";
            }

            return items;
        }

        /*public async Task<(bool isSuccess, int codeRangeId, int codeActivationId, string message)> IsCodeInRange(int rsiOrgId, string preAlpha, string postAlpha, int numericValue)
        {
            (bool isSuccess, int codeRangeId, int codeActivationId, string message) model = (false, 0, 0, "");
            
            try
            {
                CodeRangeModel range = await _context.CodeRanges.FirstOrDefaultAsync(x => x.PreAlphaCharacters == preAlpha 
                    && x.PostAlphaCharacters == postAlpha 
                    && numericValue >= x.StartNumber 
                    && numericValue <= x.EndNumber);

                if (range != null && range.CodeRangeId > 0)
                {
                    bool found = false;

                    model.codeRangeId = range.CodeRangeId;

                    if (numericValue == range.StartNumber || numericValue == range.EndNumber)
                    {
                        found = true;
                    }
                    else
                    {
                        float v = ((range.StartNumber - numericValue) / range.IncrementByNumber);

                        if ((v % 1) == 0)
                        {
                            found = true;
                        }
                    }

                    if (found)
                    {
                        string code = preAlpha + numericValue.ToString().PadLeft(range.IncrementByNumber, '0') + postAlpha;
                        CodeActivationModel activation = await _context.CodeActivations.FirstOrDefaultAsync(x => x.Code.ToUpper() == code && x.CodeRange.RSIOrganizationId == rsiOrgId);

                        if (activation != null && activation.CodeActivationId > 0)
                            model.codeActivationId = activation.CodeActivationId;

                        model.isSuccess = true;
                        model.message = "Success";
                    }
                    else
                    {
                        model.message = "Error: Code not found in range";
                        model.isSuccess = false;
                    }
                }
                else
                {
                    model.message = "Error: Code not in any ranges";
                    model.isSuccess = false;
                }
            }
            catch (Exception ex)
            {
                model.message = $"Error: {ex.Message}";
                model.isSuccess = false;
            }

            return model;
        }*/

        /* Not Used
        public async Task<CampaignViewModel> GetCampaignById(int campaignId)
        {
            CampaignViewModel campaign = new CampaignViewModel();

            try
            {
                CampaignModel c = await _context.Campaigns.FirstOrDefaultAsync(x => x.CampaignId == campaignId);
                if (c != null && c.CampaignId > 0)
                {
                    campaign.Broker.BrokerId = c.BrokerId;
                    campaign.CreationDate = c.CreationDate;
                    campaign.CreatorIP = c.CreatorIP;
                    campaign.DeactivationDate = c.DeactivationDate;
                    campaign.DeactivationReason = c.DeactivationReason;
                    campaign.IsActive = c.IsActive;
                    campaign.Message = "Success";
                    campaign.CampaignDescription = c.CampaignDescription;
                    campaign.CampaignId = c.CampaignId;
                    campaign.CampaignName = c.CampaignName;
                    campaign.CampaignType = c.CampaignType;
                    //campaign.CardQuantity = c.q
                    campaign.Client.ClientId = c.ClientId.GetValueOrDefault(0);
                    campaign.CreationDate = c.CreationDate;
                    campaign.CreatorIP = c.CreatorIP;
                    campaign.CustomCSS = c.CustomCSS;
                    campaign.DeactivationDate = c.DeactivationDate;
                    campaign.DeactivationReason = c.DeactivationReason;
                    campaign.EndDateTime = c.EndDateTime;
                    campaign.GoogleAnalyticsCode = c.GoogleAnalyticsCode;
                    campaign.PackageId = c.PackageId;
                    campaign.StartDateTime = c.StartDateTime;
                     
                }
                else
                    c.Message = "Error: Campaign not found";
            }
            catch (Exception ex)
            {
                if (campaign == null)
                    campaign = new CampaignViewModel();

                campaign.Message = $"Error: {ex.Message}";
            }

            return campaign;
        }
        */

        /* Not used
        public async Task<ListViewModel<CampaignViewModel>> GetCampaigns(int brokerId, int startRowIndex = 0, int numberOfRows = 10, string sortColumn = "DEFAULT")
        {
            ListViewModel<CampaignViewModel> items = new ListViewModel<CampaignViewModel>();

            try
            {
                var tmp = from c in _context.Campaigns where c.BrokerId == brokerId select c;

                switch (sortColumn)
                {
                    case "AgentFirstName":
                        tmp = tmp.OrderBy(o => o.CampaignName);
                        break;
                    default:
                        tmp = tmp.OrderBy(o => o.CreationDate);
                        break;
                }

                var ct = tmp;

                items.TotalCount = ct.Count();

                items.Items = await (from t in tmp
                                     select new CampaignViewModel
                                     {
                                         CreationDate = t.CreationDate,
                                         CreatorIP = t.CreatorIP,
                                         DeactivationDate = t.DeactivationDate,
                                         IsActive = t.IsActive,
                                         Message = t.Message,
                                         CampaignDescription = t.CampaignDescription,
                                         CampaignId = t.CampaignId,
                                         CampaignName = t.CampaignName,
                                         CampaignType = t.CampaignType,
                                         CustomCSS = t.CustomCSS,
                                         DeactivationReason = t.DeactivationReason,
                                         EndDateTime = t.EndDateTime,
                                         GoogleAnalyticsCode = t.GoogleAnalyticsCode,
                                         PackageId = t.PackageId,
                                         StartDateTime = t.StartDateTime
                                     }).Skip(startRowIndex).Take(numberOfRows).ToListAsync();
                items.Message = "Success";
            }
            catch (Exception ex)
            {
                items.Message = $"Error: {ex.Message}";
            }

            return items;
        }
        */

        public async Task<ClientViewModel> GetClientByAccountId(string accountId)
        {
            ClientViewModel client = new ClientViewModel();

            try
            {
                ClientModel c = await _context.Clients.FirstOrDefaultAsync(x => x.ApplicationReference == accountId);
                if (c != null && c.ClientId > 0)
                {
                    client = new ClientViewModel()
                    {
                        Address = c.Address,
                        City = c.City,
                        CompanyName = c.CompanyName,
                        Country = c.Country,
                        CreationDate = c.CreationDate,
                        CreatorIP = c.CreatorIP,
                        DeactivationDate = c.DeactivationDate,
                        DeactivationReason = c.DeactivationReason,
                        EIN = c.EIN,
                        Email = c.Email,
                        Fax = c.Fax,
                        FaxExtension = c.FaxExtension,
                        IsActive = c.IsActive,
                        Message = "Success",
                        MobilePhone = c.MobilePhone,
                        OfficeExtension = c.OfficeExtension,
                        OfficePhone = c.OfficePhone,
                        PostalCode = c.PostalCode,
                        State = c.State,
                        ClientId = c.ClientId,
                        CommissionRate = c.CommissionRate,
                        ContactFirstName = c.ContactFirstName,
                        ContactLastName = c.ContactLastName,
                        ApplicationReference = c.ApplicationReference,
                        ContactMiddleName = c.ContactMiddleName
                    };

                    client.Broker.BrokerId = c.BrokerId;
                }
                else
                    client.Message = "Error: Client not found";
            }
            catch (Exception ex)
            {
                if (client == null)
                    client = new ClientViewModel();

                client.Message = $"Error: {ex.Message}";
            }

            return client;
        }
        public async Task<ClientViewModel> GetClientById(int clientId)
        {
            ClientViewModel client = new ClientViewModel();

            try
            {
                ClientModel c = await _context.Clients.FirstOrDefaultAsync(x => x.ClientId == clientId);
                if (c != null && c.ClientId > 0)
                {
                    client = new ClientViewModel()
                    {
                        Address = c.Address,
                        City = c.City,
                        CompanyName = c.CompanyName,
                        Country = c.Country,
                        CreationDate = c.CreationDate,
                        CreatorIP = c.CreatorIP,
                        DeactivationDate = c.DeactivationDate,
                        DeactivationReason = c.DeactivationReason,
                        EIN = c.EIN,
                        Email = c.Email,
                        Fax = c.Fax,
                        FaxExtension = c.FaxExtension,
                        IsActive = c.IsActive,
                        Message = "Success",
                        MobilePhone = c.MobilePhone,
                        OfficeExtension = c.OfficeExtension,
                        OfficePhone = c.OfficePhone,
                        PostalCode = c.PostalCode,
                        State = c.State,
                        ClientId = c.ClientId,
                        CommissionRate = c.CommissionRate,
                        ContactFirstName = c.ContactFirstName,
                        ContactLastName = c.ContactLastName,
                        ApplicationReference = c.ApplicationReference,
                        ContactMiddleName = c.ContactMiddleName
                    };

                    client.Broker.BrokerId = c.BrokerId;
                }
                else
                    client.Message = "Error: Client not found";
            }
            catch (Exception ex)
            {
                if (client == null)
                    client = new ClientViewModel();

                client.Message = $"Error: {ex.Message}";
            }

            return client;
        }

#pragma warning disable 1998
        public async Task<(CardsTotalViewModel model, string message, bool isSuccess)> GetCardTotalsForBroker(int brokerId)
        {
            (CardsTotalViewModel model, string message, bool isSuccess) model = (new CardsTotalViewModel(), "", false);

            try
            {


                model.isSuccess = true;
                model.message = "Success";
            }
            catch (Exception ex)
            {
                model.isSuccess = false;
                model.message = ex.Message;
            }

            return model;
        }
#pragma warning restore 1998

        public async Task<DataTableViewModel<ClientListViewModel>> GetClients(int draw, int brokerId, int startRowIndex = 0, int numberOfRows = 10, string searchValue = null, string sortColumn = "DEFAULT", string sortDirection = "ASC")
        {
            DataTableViewModel<ClientListViewModel> model = new DataTableViewModel<ClientListViewModel>();
            
            try
            {
                model.RoleName = "Client";
                var broker = await GetBrokerById(brokerId);
                if(broker != null && broker.BrokerId > 0)
                {
                    if((broker.BrokerFirstName != null && broker.BrokerFirstName.Length > 0) || (broker.BrokerLastName != null && broker.BrokerLastName.Length > 0))
                    {
                        if (broker.BrokerFirstName != null && broker.BrokerFirstName.Length > 0)
                            model.HeaderValue += broker.BrokerFirstName;

                        if (broker.BrokerLastName != null && broker.BrokerLastName.Length > 0)
                        {
                            if (model.HeaderValue.Length > 0)
                                model.HeaderValue += " ";

                            model.HeaderValue += broker.BrokerLastName;
                        }
                        else
                            model.HeaderValue = broker.CompanyName;
                    }
                }
                model.Draw = draw;
                var tmp = from a in _context.Clients where a.BrokerId == brokerId && a.IsActive select a;
                model.NumberOfRows = await tmp.CountAsync();

                if (!string.IsNullOrEmpty(searchValue))
                {
                    int? c = null;

                    int c1 = 0;
                    if (int.TryParse(searchValue, out c1))
                        c = c1;

                    tmp = tmp.Where(x => x.ClientId == c
                        || x.CompanyName.Contains(searchValue)
                        || x.Email.Contains(searchValue)
                        || x.ContactFirstName.Contains(searchValue)
                        || x.ContactLastName.Contains(searchValue)
                        || x.MobilePhone.Contains(searchValue)
                        || x.OfficePhone.Contains(searchValue)
                        || x.Fax.Contains(searchValue)
                        || x.ApplicationReference.Contains(searchValue));
                }
                model.RecordsFiltered = tmp.Count();
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
                                tmp = tmp.OrderBy(o => o.OfficePhone);
                            else
                                tmp = tmp.OrderByDescending(o => o.OfficePhone);
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
                                tmp = tmp.OrderBy(o => o.ContactFirstName);
                            else
                                tmp = tmp.OrderByDescending(o => o.ContactFirstName);
                            break;
                    }
                    //tmp = tmp.OrderBy(search + " " + sortDirection);
                }

                var ct = tmp;

                model.RecordsFiltered = ct.Count();

                var data = await (from t in tmp
                                     select new ClientListViewModel
                                     {
                                         AccountId = t.ApplicationReference,
                                         ActivationDate = t.CreationDate,
                                         CardQuantity = 0,
                                         ClientId = t.ClientId,
                                         Company = t.CompanyName,
                                         DeactivationDate = t.DeactivationDate,
                                         Email = t.Email,
                                         FirstName = t.ContactFirstName,
                                         LastName = t.ContactLastName,
                                         MiddleName = t.ContactMiddleName,
                                         Extension = t.MobilePhone != null && t.MobilePhone.Length > 0 ? "" : t.OfficeExtension,
                                         Phone = t.MobilePhone != null && t.MobilePhone.Length > 0 ? t.MobilePhone.FormatPhone() : t.OfficePhone.FormatPhone(),
                                         CommissionRate = t.CommissionRate
                                          
                                     }).Skip(startRowIndex).Take(numberOfRows).ToArrayAsync();

                foreach (var item in data)
                {
                    item.CardQuantity = await GetCardQuantityByClient(item.ClientId);
                }
                    
                model.Data = data;
                model.Message = "Success";
            }
            catch (Exception ex)
            {
                if (model == null)
                    model = new DataTableViewModel<ClientListViewModel>();

                model.Message = $"Error: {ex.Message}";
            }

            return model;
        }

        private async Task<int> GetCardQuantityByClient(int clientId)
        {
            var query =
                from uc in _context.UnusedCodes
                join cr in _context.CodeRanges on uc.CodeRangeId equals cr.CodeRangeId
                join ccr in _context.CampaignCodeRanges on cr.CodeRangeId equals ccr.CodeRangeId
                join camp in _context.Campaigns on ccr.CampaignId equals camp.CampaignId
                join c in _context.Clients on camp.ClientId equals c.ClientId
                where c.ClientId == clientId
                select uc;

            return await query.CountAsync();
        }

        public async Task<AgentViewModel> GetAgentById(int agentId)
        {
            AgentViewModel agent = new AgentViewModel();

            try
            {
                AgentModel a = await _context.Agents.FirstOrDefaultAsync(x => x.AgentId == agentId);
                if (a != null && a.AgentId > 0)
                {
                    agent = new AgentViewModel()
                    {
                        Address = a.Address,
                        AgentFirstName = a.AgentFirstName,
                        AgentId = a.AgentId,
                        AgentLastName = a.AgentLastName,
                        City = a.City,
                        CompanyName = a.CompanyName,
                        Country = a.Country,
                        CreationDate = a.CreationDate,
                        CreatorIP = a.CreatorIP,
                        DeactivationDate = a.DeactivationDate,
                        DeactivationReason = a.DeactivationReason,
                        EIN = a.EIN,
                        Email = a.Email,
                        Fax = a.Fax,
                        FaxExtension = a.FaxExtension,
                        IsActive = a.IsActive,
                        Message = "Success",
                        MobilePhone = a.MobilePhone,
                        OfficeExtension = a.OfficeExtension,
                        OfficePhone = a.OfficePhone,
                        PostalCode = a.PostalCode,
                        State = a.State,
                        ApplicationReference = a.ApplicationReference,
                        CommissionRate = a.CommissionRate
                    };

                    agent.Broker.BrokerId = a.BrokerId;
                }
                else
                    agent.Message = $"Error (GetAgentById): Agent not found: {agentId}";
            }
            catch (Exception ex)
            {
                if (agent == null)
                    agent = new AgentViewModel();

                agent.Message = $"Error: {ex.Message}";
            }

            return agent;
        }
        public async Task<AgentViewModel> GetAgentByAccountId(string accountId)
        {
            AgentViewModel agent = new AgentViewModel();

            try
            {
                AgentModel a = await _context.Agents.FirstOrDefaultAsync(x => x.ApplicationReference == accountId);
                if (a != null && a.AgentId > 0)
                {
                    agent = new AgentViewModel()
                    {
                        Address = a.Address,
                        AgentFirstName = a.AgentFirstName,
                        AgentId = a.AgentId,
                        AgentLastName = a.AgentLastName,
                        City = a.City,
                        CompanyName = a.CompanyName,
                        Country = a.Country,
                        CreationDate = a.CreationDate,
                        CreatorIP = a.CreatorIP,
                        DeactivationDate = a.DeactivationDate,
                        DeactivationReason = a.DeactivationReason,
                        EIN = a.EIN,
                        Email = a.Email,
                        Fax = a.Fax,
                        FaxExtension = a.FaxExtension,
                        IsActive = a.IsActive,
                        Message = "Success",
                        MobilePhone = a.MobilePhone,
                        OfficeExtension = a.OfficeExtension,
                        OfficePhone = a.OfficePhone,
                        PostalCode = a.PostalCode,
                        State = a.State,
                        ApplicationReference = a.ApplicationReference,
                        CommissionRate = a.CommissionRate
                    };

                    agent.Broker.BrokerId = a.BrokerId;
                }
                else
                    agent.Message = $"Error (GetAgentByAccountId): Agent not found {accountId}";
            }
            catch (Exception ex)
            {
                if (agent == null)
                    agent = new AgentViewModel();

                agent.Message = $"Error: {ex.Message}";
            }

            return agent;
        }

        public async Task<DataTableViewModel<CodeListViewModel>> GetBrokerCodes(int draw,int brokerId, int startRowIndex = 0, int numberOfRows = 10, string searchValue = null, string sortColumn = "DEFAULT", string sortDirection = "ASC")
        {
            DataTableViewModel<CodeListViewModel> model = new DataTableViewModel<CodeListViewModel>();

            try
            {
                model.RoleName = "Purchases";
                var broker = await GetBrokerById(brokerId);
                if (broker != null && broker.BrokerId > 0)
                {
                    if ((broker.BrokerFirstName != null && broker.BrokerFirstName.Length > 0) || (broker.BrokerLastName != null && broker.BrokerLastName.Length > 0))
                    {
                        if (broker.BrokerFirstName != null && broker.BrokerFirstName.Length > 0)
                            model.HeaderValue += broker.BrokerFirstName;

                        if (broker.BrokerLastName != null && broker.BrokerLastName.Length > 0)
                        {
                            if (model.HeaderValue.Length > 0)
                                model.HeaderValue += " ";

                            model.HeaderValue += broker.BrokerLastName;
                        }
                        else
                            model.HeaderValue = broker.CompanyName;
                    }
                }
                model.Draw = draw;

                var tmp = from r in _context.CodeRanges
                              where r.BrokerId == brokerId
                              select r;

                model.NumberOfRows = await tmp.CountAsync();
                if (searchValue != null && searchValue.Length > 0)
                {
                    int codeRange = 0;
                    float rewards = 0;
                    decimal cost = 0;
                    int.TryParse(searchValue, out codeRange);
                    float.TryParse(searchValue, out rewards);
                    decimal.TryParse(searchValue, out cost);

                    tmp = tmp.Where(w => w.CodeRangeId == codeRange
                        || w.CodeType.Contains(searchValue)
                        || w.Points == rewards
                        || (w.PreAlphaCharacters + w.StartNumber.ToString() + w.PostAlphaCharacters).Contains(searchValue)
                        || (w.PreAlphaCharacters + w.EndNumber.ToString() + w.PostAlphaCharacters).Contains(searchValue)
                        || w.Cost == cost);
                }
                model.RecordsFiltered = tmp.Count();
                switch (sortColumn)
                {
                    case "charge_amount":
                        if (sortDirection.ToUpper() == "ASC")
                            tmp.OrderBy(o => o.Cost);
                        else
                            tmp.OrderByDescending(o => o.Cost);
                        break;
                    case "quantity":
                        if (sortDirection.ToUpper() == "ASC")
                            tmp.OrderBy(o => ((o.EndNumber - o.StartNumber) / o.IncrementByNumber) + 1);
                        else
                            tmp.OrderByDescending(o => ((o.EndNumber - o.StartNumber) / o.IncrementByNumber) + 1);
                        break;
                    case "start_code":
                        if (sortDirection.ToUpper() == "ASC")
                            tmp.OrderBy(o => o.PreAlphaCharacters + o.StartNumber.ToString() + o.EndNumber.ToString());
                        else
                            tmp.OrderByDescending(o => o.PreAlphaCharacters + o.StartNumber.ToString() + o.EndNumber.ToString());
                        break;
                    case "end_code":
                        if (sortDirection.ToUpper() == "ASC")
                            tmp.OrderBy(o => o.PreAlphaCharacters + o.EndNumber.ToString() + o.EndNumber.ToString());
                        else
                            tmp.OrderByDescending(o => o.PreAlphaCharacters + o.StartNumber.ToString() + o.EndNumber.ToString());
                        break;
                    case "number_of_activations":
                        if (sortDirection.ToUpper() == "ASC")
                            tmp.OrderBy(o => o.NumberOfUses);
                        else
                            tmp.OrderByDescending(o => o.NumberOfUses);
                        break;
                    case "points_on_card":
                        if (sortDirection.ToUpper() == "ASC")
                            tmp.OrderBy(o => o.Points);
                        else
                            tmp.OrderByDescending(o => o.Points);
                        break;
                    case "card_type":
                        if (sortDirection.ToUpper() == "ASC")
                            tmp.OrderBy(o => o.CodeType);
                        else
                            tmp.OrderByDescending(o => o.CodeType);
                        break;
                    case "code_range_id":
                        if (sortDirection.ToUpper() == "ASC")
                            tmp.OrderBy(o => o.CodeRangeId);
                        else
                            tmp.OrderByDescending(o => o.CodeRangeId);
                        break;
                    default:
                        if (sortDirection.ToUpper() == "ASC")
                            tmp.OrderBy(o => o.CreationDate);
                        else
                            tmp.OrderByDescending(o => o.CreationDate);
                        break;
                }

                model.Data = await (from r in tmp
                                    select new CodeListViewModel
                                    {
                                        Points = r.Points,
                                        ChargeAmount = r.Cost,
                                        CreationDate = r.CreationDate,
                                        CardType = r.CodeType,
                                        CodeRangeId = r.CodeRangeId,
                                        EndAlpha = r.PostAlphaCharacters,
                                        EndNumber = r.EndNumber,
                                        IncrementBy = r.IncrementByNumber,
                                        NumberOfActivations = r.NumberOfUses,
                                        StartAlpha = r.PreAlphaCharacters,
                                        StartNumber = r.StartNumber
                                    }).Skip(startRowIndex).Take(numberOfRows).ToArrayAsync();
            }
            catch (Exception ex)
            {
                if (model == null)
                    model = new DataTableViewModel<CodeListViewModel>();

                model.Message = $"Error: {ex.Message}";
            }

            return model;
        }

        public async Task<DataTableViewModel<AgentListViewModel>> GetAgents(int draw, int brokerId, int startRowIndex = 0, int numberOfRows = 10, string searchValue = null, int? campaignId = null, string sortColumn = "DEFAULT", string sortDirection = "ASC")
        {
            DataTableViewModel<AgentListViewModel> model = new DataTableViewModel<AgentListViewModel>();

            try
            {
                model.RoleName = "Agent";
                var broker = await GetBrokerById(brokerId);
                if (broker != null && broker.BrokerId > 0)
                {
                    if ((broker.BrokerFirstName != null && broker.BrokerFirstName.Length > 0) || (broker.BrokerLastName != null && broker.BrokerLastName.Length > 0))
                    {
                        if (broker.BrokerFirstName != null && broker.BrokerFirstName.Length > 0)
                            model.HeaderValue += broker.BrokerFirstName;

                        if (broker.BrokerLastName != null && broker.BrokerLastName.Length > 0)
                        {
                            if (model.HeaderValue.Length > 0)
                                model.HeaderValue += " ";

                            model.HeaderValue += broker.BrokerLastName;
                        }
                        else
                            model.HeaderValue = broker.CompanyName;
                    }
                }
                model.Draw = draw;
                var tmp = from a in _context.Agents where a.BrokerId == brokerId select a;
                model.NumberOfRows = await tmp.CountAsync();

                if (campaignId.GetValueOrDefault(0) > 0)
                    tmp = tmp.Where(t => t.CampaignAgents.Any(a => a.CampaignId == campaignId));

                if (!string.IsNullOrEmpty(searchValue))
                {
                    int? c = null;

                    int c1 = 0;
                    if (int.TryParse(searchValue, out c1))
                        c = c1;

                    tmp = tmp.Where(x => x.AgentId == c
                        || x.CompanyName.Contains(searchValue)
                        || x.Email.Contains(searchValue)
                        || x.AgentFirstName.Contains(searchValue)
                        || x.AgentLastName.Contains(searchValue)
                        || x.MobilePhone.Contains(searchValue)
                        || x.OfficePhone.Contains(searchValue)
                        || x.Fax.Contains(searchValue)
                        || x.ApplicationReference.Contains(searchValue));
                }
                model.RecordsFiltered = tmp.Count();
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
                                tmp = tmp.OrderBy(o => o.OfficePhone);
                            else
                                tmp = tmp.OrderByDescending(o => o.OfficePhone);
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
                                tmp = tmp.OrderBy(o => o.AgentFirstName);
                            else
                                tmp = tmp.OrderByDescending(o => o.AgentFirstName);
                            break;
                    }
                }

                var ct = tmp;

                model.RecordsFiltered = ct.Count();

                model.Data = await (from t in tmp
                                     select new AgentListViewModel
                                     {
                                         AccountId = t.ApplicationReference,
                                         ActivationDate = t.CreationDate,
                                         AgentId = t.AgentId,
                                         Clients = t.Clients.Count(),
                                         CommissionRate = t.CommissionRate,
                                         Company = t.CompanyName,
                                         DeactivationDate = t.DeactivationDate,
                                         Email = t.Email,
                                         FirstName = t.AgentFirstName,
                                         LastName = t.AgentLastName,
                                         MiddleName = t.AgentMiddleName,
                                         Extension = t.MobilePhone != null && t.MobilePhone.Length > 0 ? "" : t.OfficeExtension,
                                         Phone = t.MobilePhone != null && t.MobilePhone.Length > 0 ? t.MobilePhone.FormatPhone() : t.OfficePhone.FormatPhone()
                                     }).Skip(startRowIndex).Take(numberOfRows).ToArrayAsync();

                model.Message = "Success";
            }
            catch (Exception ex)
            {
                if (model == null)
                    model = new DataTableViewModel<AgentListViewModel>();

                model.Message = $"Error: {ex.Message}";
            }

            return model;
        }

        public async Task<BrokerViewModel> GetBrokerById(int brokerId)
        {
            BrokerViewModel broker = new BrokerViewModel();

            try
            {
                BrokerModel b = await _context.Brokers.FirstOrDefaultAsync(x => x.BrokerId == brokerId);
                if (b != null && b.BrokerId > 0)
                {
                    broker = new BrokerViewModel()
                    {
                        Address = b.Address,
                        AgentCommissionPercentage = b.AgentCommissionPercentage,
                        BrokerCommissionPercentage = b.BrokerCommissionPercentage,
                        BrokerFirstName = b.BrokerFirstName,
                        BrokerId = b.BrokerId,
                        BrokerLastName = b.BrokerLastName,
                        City = b.City,
                        ClientCommissionPercentage = b.ClientCommissionPercentage,
                        CompanyName = b.CompanyName,
                        Country = b.Country,
                        CreationDate = b.CreationDate,
                        CreatorIP = b.CreatorIP,
                        DeactivationDate = b.DeactivationDate,
                        DeactivationReason = b.DeactivationReason,
                        EIN = b.EIN,
                        Email = b.Email,
                        Fax = b.Fax,
                        FaxExtension = b.FaxExtension,
                        IsActive = b.IsActive,
                        Message = "Success",
                        MobilePhone = b.MobilePhone,
                        OfficeExtension = b.OfficeExtension,
                        OfficePhone = b.OfficePhone,
                        PhysicalCardsPercentOfFaceValue1000 = b.PhysicalCardsPercentOfFaceValue1000,
                        PhysicalCardsPercentOfFaceValue10000 = b.PhysicalCardsPercentOfFaceValue10000,
                        PhysicalCardsPercentOfFaceValue100000 = b.PhysicalCardsPercentOfFaceValue100000,
                        PhysicalCardsPercentOfFaceValue25000 = b.PhysicalCardsPercentOfFaceValue25000,
                        PhysicalCardsPercentOfFaceValue5000 = b.PhysicalCardsPercentOfFaceValue5000,
                        PhysicalCardsPercentOfFaceValue50000 = b.PhysicalCardsPercentOfFaceValue50000,
                        PostalCode = b.PostalCode,
                        State = b.State,
                        TimeframeBetweenCapInHours = b.TimeframeBetweenCapInHours,
                        VirtualCardCap = b.VirtualCardCap,
                        ApplicationReference = b.ApplicationReference,
                        BrokerMiddleName = b.BrokerMiddleName,
                        ParentBrokerId = b.ParentBrokerId
                    };
                }
                else
                    broker.Message = $"Error (GetBrokerById): Broker not found: {brokerId}";
            }
            catch (Exception ex)
            {
                if (broker == null)
                    broker = new BrokerViewModel();

                broker.Message = $"Error: {ex.Message}";
            }

            return broker;
        }
        public async Task<BrokerViewModel> GetBrokerByAccountId(string accountId)
        {
            BrokerViewModel broker = new BrokerViewModel();

            try
            {
                BrokerModel b = await _context.Brokers.FirstOrDefaultAsync(x => x.ApplicationReference == accountId);
                if (b != null && b.BrokerId > 0)
                {
                    broker = new BrokerViewModel()
                    {
                        Address = b.Address,
                        AgentCommissionPercentage = b.AgentCommissionPercentage,
                        BrokerCommissionPercentage = b.BrokerCommissionPercentage,
                        BrokerFirstName = b.BrokerFirstName,
                        BrokerId = b.BrokerId,
                        BrokerLastName = b.BrokerLastName,
                        City = b.City,
                        ClientCommissionPercentage = b.ClientCommissionPercentage,
                        CompanyName = b.CompanyName,
                        Country = b.Country,
                        CreationDate = b.CreationDate,
                        CreatorIP = b.CreatorIP,
                        DeactivationDate = b.DeactivationDate,
                        DeactivationReason = b.DeactivationReason,
                        EIN = b.EIN,
                        Email = b.Email,
                        Fax = b.Fax,
                        FaxExtension = b.FaxExtension,
                        IsActive = b.IsActive,
                        Message = "Success",
                        MobilePhone = b.MobilePhone,
                        OfficeExtension = b.OfficeExtension,
                        OfficePhone = b.OfficePhone,
                        PhysicalCardsPercentOfFaceValue1000 = b.PhysicalCardsPercentOfFaceValue1000,
                        PhysicalCardsPercentOfFaceValue10000 = b.PhysicalCardsPercentOfFaceValue10000,
                        PhysicalCardsPercentOfFaceValue100000 = b.PhysicalCardsPercentOfFaceValue100000,
                        PhysicalCardsPercentOfFaceValue25000 = b.PhysicalCardsPercentOfFaceValue25000,
                        PhysicalCardsPercentOfFaceValue5000 = b.PhysicalCardsPercentOfFaceValue5000,
                        PhysicalCardsPercentOfFaceValue50000 = b.PhysicalCardsPercentOfFaceValue50000,
                        PostalCode = b.PostalCode,
                        State = b.State,
                        TimeframeBetweenCapInHours = b.TimeframeBetweenCapInHours,
                        VirtualCardCap = b.VirtualCardCap,
                        ApplicationReference = b.ApplicationReference,
                        BrokerMiddleName = b.BrokerMiddleName,
                        ParentBrokerId = b.ParentBrokerId,
                        DocumentW9Id = b.DocumentW9Id
                    };
                }
                else
                    broker.Message = $"Error (GetBrokerByAccountId): Broker not found: {accountId}";
            }
            catch (Exception ex)
            {
                if (broker == null)
                    broker = new BrokerViewModel();

                broker.Message = $"Error: {ex.Message}";
            }

            return broker;
        }

        public async Task<DataTableViewModel<BrokerListViewModel>> GetBrokers(int draw, int startRowIndex = 0, int numberOfRows = 10, string searchValue = null, string sortColumn = "DEFAULT", string sortDirection = "ASC", bool onlyActive = false)
        {
            DataTableViewModel<BrokerListViewModel> model = new DataTableViewModel<BrokerListViewModel>();

            try
            {
                model.RoleName = "Broker";

                model.Draw = draw;
                var tmp = from b in _context.Brokers select b;
                model.NumberOfRows = tmp.Count();
                if (!string.IsNullOrEmpty(searchValue))
                {
                    int? c = null;

                    int c1 = 0;
                    if (int.TryParse(searchValue, out c1))
                        c = c1;

                    tmp = tmp.Where(x => x.BrokerId == c
                        || x.CompanyName.Contains(searchValue)
                        || x.Email.Contains(searchValue)
                        || x.BrokerFirstName.Contains(searchValue)
                        || x.BrokerLastName.Contains(searchValue)
                        || x.MobilePhone.Contains(searchValue)
                        || x.OfficePhone.Contains(searchValue)
                        || x.Fax.Contains(searchValue)
                        || x.ApplicationReference.Contains(searchValue));

                    if (onlyActive)
                        tmp = tmp.Where(x => x.IsActive);
                }
                model.RecordsFiltered = tmp.Count();
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
                                tmp = tmp.OrderBy(o => o.OfficePhone);
                            else
                                tmp = tmp.OrderByDescending(o => o.OfficePhone);
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
                                tmp = tmp.OrderBy(o => o.BrokerFirstName);
                            else
                                tmp = tmp.OrderByDescending(o => o.BrokerFirstName);
                            break;
                    }
                    //tmp = tmp.OrderBy(search + " " + sortDirection);
                }

                var ct = tmp;

                model.RecordsFiltered = ct.Count();

                model.Data = await (from t in tmp
                                    select new BrokerListViewModel
                                    {
                                        AccountId = t.ApplicationReference,
                                        ActivationDate = t.CreationDate,
                                        BrokerId = t.BrokerId,
                                        CommissionRate = t.BrokerCommissionPercentage,
                                        Company = t.CompanyName,
                                        DeactivationDate = t.DeactivationDate,
                                        Email = t.Email,
                                        FirstName = t.BrokerFirstName,
                                        LastName = t.BrokerLastName,
                                        MiddleName = t.BrokerMiddleName,
                                        Extension = t.MobilePhone != null && t.MobilePhone.Length > 0 ? "" : t.OfficeExtension,
                                        Phone = t.MobilePhone != null && t.MobilePhone.Length > 0 ? t.MobilePhone : t.OfficePhone
                                    }).Skip(startRowIndex).Take(numberOfRows).ToArrayAsync();
                model.Message = "Success";
            }
            catch (Exception ex)
            {
                model.Message = $"Error: {ex.Message}";
            }

            return model;
        }

        public async Task<DataTableViewModel<BrokerListViewModel>> GetBrokers()
        {
            DataTableViewModel<BrokerListViewModel> model = new DataTableViewModel<BrokerListViewModel>();

            try
            {
                model.RoleName = "Broker";
                var brokers = _context.Brokers.Where(b => b.IsActive && b.ApplicationReference != null);

                model.Data = await (from t in brokers
                                     select new BrokerListViewModel
                                     {
                                         AccountId = t.ApplicationReference,
                                         ActivationDate = t.CreationDate,
                                         BrokerId = t.BrokerId,
                                         CommissionRate = t.BrokerCommissionPercentage,
                                         Company = t.CompanyName,
                                         DeactivationDate = t.DeactivationDate,
                                         Email = t.Email,
                                         FirstName = t.BrokerFirstName,
                                         LastName = t.BrokerLastName,
                                         MiddleName = t.BrokerMiddleName,
                                         Extension = t.MobilePhone != null && t.MobilePhone.Length > 0 ? "" : t.OfficeExtension,
                                         Phone = t.MobilePhone != null && t.MobilePhone.Length > 0 ? t.MobilePhone.FormatPhone() : t.OfficePhone.FormatPhone()
                                     }).ToArrayAsync();

                model.Message = "Success";
            }
            catch (Exception ex)
            {
                model.Message = $"Error: {ex.Message}";
            }

            return model;
        }

        public async Task<BrokerViewModel> GetBrokerByReference(string referenceId)
        {
            BrokerViewModel model = new BrokerViewModel();

            try
            {
                BrokerModel b = await _context.Brokers.FirstOrDefaultAsync(x => x.ApplicationReference == referenceId);

                if (b != null && b.BrokerId > 0)
                {
                    model = new BrokerViewModel()
                    {
                        Address = b.Address,
                        AgentCommissionPercentage = b.AgentCommissionPercentage,
                        BrokerCommissionPercentage = b.BrokerCommissionPercentage,
                        BrokerFirstName = b.BrokerFirstName,
                        BrokerMiddleName = b.BrokerMiddleName,
                        BrokerId = b.BrokerId,
                        BrokerLastName = b.BrokerLastName,
                        City = b.City,
                        ClientCommissionPercentage = b.ClientCommissionPercentage,
                        CompanyName = b.CompanyName,
                        Country = b.Country,
                        CreationDate = b.CreationDate,
                        CreatorIP = b.CreatorIP,
                        DeactivationDate = b.DeactivationDate,
                        DeactivationReason = b.DeactivationReason,
                        EIN = b.EIN,
                        Email = b.Email,
                        Fax = b.Fax,
                        FaxExtension = b.FaxExtension,
                        IsActive = b.IsActive,
                        Message = "Success",
                        MobilePhone = b.MobilePhone,
                        OfficeExtension = b.OfficeExtension,
                        OfficePhone = b.OfficePhone,
                        PhysicalCardsPercentOfFaceValue1000 = b.PhysicalCardsPercentOfFaceValue1000,
                        PhysicalCardsPercentOfFaceValue10000 = b.PhysicalCardsPercentOfFaceValue10000,
                        PhysicalCardsPercentOfFaceValue100000 = b.PhysicalCardsPercentOfFaceValue100000,
                        PhysicalCardsPercentOfFaceValue25000 = b.PhysicalCardsPercentOfFaceValue25000,
                        PhysicalCardsPercentOfFaceValue5000 = b.PhysicalCardsPercentOfFaceValue5000,
                        PhysicalCardsPercentOfFaceValue50000 = b.PhysicalCardsPercentOfFaceValue50000,
                        PostalCode = b.PostalCode,
                        State = b.State,
                        TimeframeBetweenCapInHours = b.TimeframeBetweenCapInHours,
                        VirtualCardCap = b.VirtualCardCap,
                        ApplicationReference = b.ApplicationReference,
                        ParentBrokerId = b.ParentBrokerId
                    };
                }
                else
                    model.Message = $"Error (GetBrokerByReference): Broker not found: {referenceId}";
            }
            catch (Exception ex)
            {
                if (model == null)
                    model = new BrokerViewModel();

                model.Message = $"Error: {ex.Message}";
            }

            return model;
        }
        public async Task<AgentViewModel> GetAgentByReference(string referenceId)
        {
            AgentViewModel model = new AgentViewModel();

            try
            {
                AgentModel a = await _context.Agents.FirstOrDefaultAsync(x => x.ApplicationReference == referenceId);

                if (a != null && a.AgentId > 0)
                {
                    model = new AgentViewModel()
                    {
                        Address = a.Address,
                        AgentFirstName = a.AgentFirstName,
                        AgentId = a.AgentId,
                        AgentLastName = a.AgentLastName,
                        City = a.City,
                        CompanyName = a.CompanyName,
                        Country = a.Country,
                        CreationDate = a.CreationDate,
                        CreatorIP = a.CreatorIP,
                        DeactivationDate = a.DeactivationDate,
                        DeactivationReason = a.DeactivationReason,
                        EIN = a.EIN,
                        Email = a.Email,
                        Fax = a.Fax,
                        FaxExtension = a.FaxExtension,
                        IsActive = a.IsActive,
                        Message = "Success",
                        MobilePhone = a.MobilePhone,
                        OfficeExtension = a.OfficeExtension,
                        OfficePhone = a.OfficePhone,
                        PostalCode = a.PostalCode,
                        State = a.State,
                        ApplicationReference = a.ApplicationReference,
                        AgentMiddleName = a.AgentMiddleName
                    };

                    model.Broker.BrokerId = a.BrokerId;
                }
                else
                    model.Message = "Error: Agent not found";
            }
            catch (Exception ex)
            {
                if (model == null)
                    model = new AgentViewModel();

                model.Message = $"Error: {ex.Message}";
            }

            return model;
        }
        public async Task<ClientViewModel> GetClientByReference(string referenceId)
        {
            ClientViewModel model = new ClientViewModel();

            try
            {
                ClientModel c = await _context.Clients
                    .Include(x => x.Agent)
                    .FirstOrDefaultAsync(x => x.ApplicationReference == referenceId);

                if (c != null && c.ClientId > 0)
                {
                    model = new ClientViewModel()
                    {
                        Address = c.Address,
                        AgentFullName = c.Agent == null ? String.Empty : c.Agent.AgentFirstName + " " + c.Agent.AgentLastName,
                        City = c.City,
                        CompanyName = c.CompanyName,
                        Country = c.Country,
                        CreationDate = c.CreationDate,
                        CreatorIP = c.CreatorIP,
                        DeactivationDate = c.DeactivationDate,
                        DeactivationReason = c.DeactivationReason,
                        EIN = c.EIN,
                        Email = c.Email,
                        Fax = c.Fax,
                        FaxExtension = c.FaxExtension,
                        IsActive = c.IsActive,
                        Message = "Success",
                        MobilePhone = c.MobilePhone,
                        OfficeExtension = c.OfficeExtension,
                        OfficePhone = c.OfficePhone,
                        PostalCode = c.PostalCode,
                        State = c.State,
                        ClientId = c.ClientId,
                        CommissionRate = c.CommissionRate,
                        ContactFirstName = c.ContactFirstName,
                        ContactLastName = c.ContactLastName,
                        ApplicationReference = c.ApplicationReference,
                        ContactMiddleName = c.ContactMiddleName
                    };

                    model.Broker.BrokerId = c.BrokerId;
                }
                else
                    model.Message = "Error: Client not found";
            }
            catch (Exception ex)
            {
                if (model == null)
                    model = new ClientViewModel();

                model.Message = $"Error: {ex.Message}";
            }

            return model;
        }
        public async Task<List<(int agentId, string agentName)>> GetAgentsAssignedToClient(int clientId)
        {
            List<(int agentId, string agentName)> model = new List<(int agentId, string agentName)>();

            try
            {
                var returnModel = await (from agents in _context.Agents
                         join campaignAgents in _context.CampaignAgents on agents.AgentId equals campaignAgents.AgentId
                         join campaigns in _context.Campaigns on campaignAgents.CampaignId equals campaigns.CampaignId
                         join clients in _context.Clients on campaigns.ClientId equals clients.ClientId
                         where clients.ClientId == clientId
                         select new { agents.AgentId, agents.AgentFirstName, agents.AgentLastName }).ToListAsync();

                if(returnModel != null && returnModel.Count() > 0)
                {
                    foreach(var row in returnModel)
                    {
                        (int agentId, string agentName) tmp = (row.AgentId, !String.IsNullOrEmpty(row.AgentFirstName) ? row.AgentFirstName : "");

                        if (!String.IsNullOrEmpty(row.AgentLastName))
                        {
                            if (tmp.agentName.Length > 0)
                                tmp.agentName += " ";

                            tmp.agentName += row.AgentLastName;
                        }

                        model.Add(tmp);
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return model;
        }
        public async Task<(int count, string message, bool isSuccess)> GetClientCampaignCount(int clientId)
        {
            (int count, string message, bool isSuccess) model = (0, "", false);
            try
            {
                model.count = await _context.Campaigns.CountAsync(c => c.ClientId == clientId);
                model.message = "Success";
                model.isSuccess = true;
            }
            catch (Exception ex)
            {
                model.message = ex.Message;
                model.isSuccess = false;
            }

            return model;
        }
        public async Task<string> BrokerAddAppReference(int brokerId, string referenceId)
        {
            string message = "";

            try
            {
                BrokerModel model = await _context.Brokers.FirstOrDefaultAsync(x => x.BrokerId == brokerId);
                if(model != null && model.BrokerId > 0)
                {
                    model.ApplicationReference = referenceId;
                    await _context.SaveChangesAsync();
                    message = "Success";
                }
                else
                {
                    message = $"Error (BrokerAddAppReference): Broker not found: {brokerId}";
                }
            }
            catch (Exception ex)
            {
                message = $"Error: {ex.Message}";
            }

            return message;
        }
        public async Task<string> AgentAddAppReference(int agentId, string referenceId)
        {
            string message = "";

            try
            {
                AgentModel model = await _context.Agents.FirstOrDefaultAsync(x => x.AgentId == agentId);
                if (model != null && model.AgentId > 0)
                {
                    model.ApplicationReference = referenceId;
                    await _context.SaveChangesAsync();
                    message = "Success";
                }
                else
                {
                    message = "Error: Agent not found";
                }
            }
            catch (Exception ex)
            {
                message = $"Error: {ex.Message}";
            }

            return message;
        }
        public async Task<string> ClientAddAppReference(int clientId, string referenceId)
        {
            string message = "";

            try
            {
                ClientModel model = await _context.Clients.FirstOrDefaultAsync(x => x.ClientId == clientId);
                if (model != null && model.ClientId > 0)
                {
                    model.ApplicationReference = referenceId;
                    await _context.SaveChangesAsync();
                    message = "Success";
                }
                else
                {
                    message = "Error: Client not found";
                }
            }
            catch (Exception ex)
            {
                message = $"Error: {ex.Message}";
            }

            return message;
        }
        public async Task<CodeActivityModel> GetActivityByRSIId(int rsiId)
        {
            CodeActivityModel model = new CodeActivityModel();
            try
            {
                model = await _context.CodeActivities.FirstOrDefaultAsync(x => x.RSIId == rsiId);

                if (model == null || model.CodeActivityId < 1)
                {
                    model = new CodeActivityModel
                    {
                        Message = "Error: CodeActivity not found"
                    };

                }
                else
                    model.Message = "Success";
            }
            catch (Exception ex)
            {
                model.Message = $"Error: {ex.Message}";
            }

            return model;
        }
        public async Task<string> GetCodeByRSIId(int rsiId)
        {
            string code = "";

            try
            {
                CodeActivityModel model = await _context.CodeActivities.FirstOrDefaultAsync(x => x.RSIId == rsiId);
                if (model != null && model.CodeActivityId > 0)
                    code = model.ActivationCode.ToUpper();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                code = $"Error: {ex.Message}";
            }

            return code;
        }
        public async Task<int> GetRSIIdByActivationCode(string activationCode, int issuer)
        {
            int i = 0;

            try
            {
                CodeActivityModel m = await _context.CodeActivities.FirstOrDefaultAsync(x => x.ActivationCode == activationCode && x.Issuer == issuer.ToString());
                if (m != null && m.CodeActivityId > 0)
                {
                    if (m.EmailVerifiedDate != null && m.RSIId < 1)
                    {
                        i = -1;
                    }
                    else if (m.EmailVerifiedDate == null)
                    {
                        i = -2;
                    }
                    else
                    {
                        i = m.RSIId;
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
               i = 0;
            }

            return i;
        }
        public async Task<CodeModel> GetCodeByActivationCode(string activationCode, string issuer)
        {
            CodeModel model = new CodeModel();

            try
            {
                model = _context.Codes.FirstOrDefault(x => x.Code.ToUpper() == activationCode.ToUpper() && x.Issuer == issuer);
                model.Message = "Success";
            }
            catch (Exception ex)
            {

                model.Message = $"Error: {ex.Message}";
                _logger.LogError(ex.Message);

            }

            return await Task.FromResult(model);
        }
        public async Task<CodeActivityModel> GetCodeActivityByCodeAsync(string activationCode, string issuer)
        {
            CodeActivityModel model = new CodeActivityModel();

            try
            {
                model = _context.CodeActivities.FirstOrDefault(x => x.ActivationCode.ToUpper() == activationCode.ToUpper() && x.Issuer == issuer);

                if (model == null)
                {
                    model = new CodeActivityModel();
                    model.Message = "Error: Activation Code not found";
                }
            }
            catch (Exception ex)
            {
                model.Message = $"Error: {ex.Message}";
                _logger.LogError(ex.Message);
            }

            return await Task.FromResult(model);
        }
        public async Task<CodeActivityModel> GetCodeActivityByEmailAsync(string email, string issuer)
        {
            CodeActivityModel model = new CodeActivityModel();

            try
            {
                model = _context.CodeActivities.FirstOrDefault(x => x.Email.ToUpper() == email.ToUpper() && x.Issuer == issuer);

                if (model == null)
                {
                    model = new CodeActivityModel();
                    model.Message = "Error: Email not found";
                }
            }
            catch (Exception ex)
            {
                model.Message = $"Error: {ex.Message}";
                _logger.LogError(ex.Message);
            }

            return await Task.FromResult(model);
        }
        public CodeActivityModel GetCodeActivityByUsernamePassword(string username, string password, string issuer)
        {
            CodeActivityModel model = new CodeActivityModel();

            try
            {
                model = _context.CodeActivities.FirstOrDefault(x => x.Username == username && x.Password == password && x.Issuer == issuer);

                if (model == null)
                {
                    model = new CodeActivityModel();
                    model.Message = "Error: Username and/or password not found";
                }

            }
            catch (Exception ex)
            {
                model.Message = $"Error: {ex.Message}";
                _logger.LogError(ex.Message);
            }

            return model;
        }
        public async Task<CodeActivityModel> GetCodeActivityByUsernamePasswordAsync(string username, string password, string issuer)
        {
            CodeActivityModel model = new CodeActivityModel();

            try
            {
                model = _context.CodeActivities.FirstOrDefault(x => x.Username == username && x.Password == password && x.Issuer == issuer);

                if (model == null)
                {
                    model = new CodeActivityModel();
                    model.Message = "Error: Username and/or password not found";
                }

            }
            catch (Exception ex)
            {
                model.Message = $"Error: {ex.Message}";
                _logger.LogError(ex.Message);
            }

            return await Task.FromResult(model);
        }
        public async Task<CodeActivityModel> GetCodeActivityByUsernameAsync(string username, string issuer)
        {
            CodeActivityModel model = new CodeActivityModel();

            try
            {
                model = _context.CodeActivities.FirstOrDefault(x => x.Username == username && x.Issuer == issuer);
            }
            catch (Exception ex)
            {
                model.Message = $"Error: {ex.Message}";
                _logger.LogError(ex.Message);
            }

            return await Task.FromResult(model);
        }
        public ActivationsViewModel GetActivations(string issuer, string[] codes = null, int startRowIndex = 0, int numberOfRows = 10
            , DateTime? startDate = null, DateTime? endDate = null, string status = "", string dateSerchType = "")
        {
            ActivationsViewModel model = new ActivationsViewModel();

            try
            {
                var tmp = from a in _context.Codes where a.Issuer == issuer && a.CodeActivities.Count > 0 select a;
                if (status == "ACTIVE")
                {
                    if (startDate != null)
                        tmp = from t in tmp where t.CreationDate >= startDate.GetValueOrDefault() select t;


                    if (endDate != null)
                        tmp = from t in tmp where t.CreationDate <= endDate.GetValueOrDefault() select t;
                }
                else if (dateSerchType == null || dateSerchType.Length < 1 || dateSerchType == "CREATIONDATE")
                {
                    if (startDate != null)
                        tmp = from t in tmp where t.CodeActivities.Any(a => a.CreationDate >= startDate.GetValueOrDefault()) select t;


                    if (endDate != null)
                        tmp = from t in tmp where t.CodeActivities.Any(a => a.CreationDate <= endDate.GetValueOrDefault()) select t;
                }
                else
                {
                    if (startDate != null)
                        tmp = from t in tmp where t.CodeActivities.Any(a => a.EmailVerifiedDate >= startDate.GetValueOrDefault()) select t;


                    if (endDate != null)
                        tmp = from t in tmp where t.CodeActivities.Any(a => a.EmailVerifiedDate <= endDate.GetValueOrDefault()) select t;
                }

                if (codes != null && codes.Length > 0)
                    tmp = from t in tmp where codes.Contains(t.Code) select t;

                if (status != null && status.Length > 0)
                {
                    if (status == "COMPLETE")
                    {
                        tmp = from t in tmp where t.CodeActivities.Any(a => a.EmailVerifiedDate != null && a.RSIId > 0) select t;
                    }
                    if (status == "EMAILVERIFY")
                    {
                        tmp = from t in tmp where t.CodeActivities.Any(a => (a.FirstName != null && a.FirstName.Length > 0) && a.EmailVerifiedDate == null) select t;
                    }
                    else if (status == "ERROR")
                    {
                        tmp = from t in tmp where t.CodeActivities.Any(a => a.EmailVerifiedDate != null && a.RSIId == 0) select t;
                    }
                    else if (status == "ACTIVE")
                    {
                        tmp = from t in tmp where t.NumberOfUses == 0 || t.NumberOfUses > t.CodeActivities.Count() select t;
                    }
                }

                model.Activations = (from t in tmp
                                     select new ActivationViewModel
                                     {
                                         Code = t,
                                         Activities = t.CodeActivities
                                     }).Skip(startRowIndex).Take(numberOfRows).ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                model.Message = $"Error: {ex.Message}";
            }

            return model;
        }
        public async Task<ActivationsViewModel> GetActivationsAsync(string issuer, string[] codes = null, int startRowIndex = 0, int numberOfRows = 10
            , DateTime? startDate = null, DateTime? endDate = null, string status = "", string dateSerchType = "")
        {
            ActivationsViewModel model = new ActivationsViewModel();

            try
            {
                var tmp = from a in _context.Codes where a.Issuer == issuer && a.CodeActivities.Count > 0 select a;
                if(status == "ACTIVE")
                {
                    if (startDate != null)
                        tmp = from t in tmp where t.CreationDate >= startDate.GetValueOrDefault() select t;


                    if (endDate != null)
                        tmp = from t in tmp where t.CreationDate <= endDate.GetValueOrDefault() select t;
                }
                else if (dateSerchType == null || dateSerchType.Length < 1 || dateSerchType == "CREATIONDATE")
                {
                    if (startDate != null)
                        tmp = from t in tmp where t.CodeActivities.Any(a => a.CreationDate >= startDate.GetValueOrDefault()) select t;


                    if (endDate != null)
                        tmp = from t in tmp where t.CodeActivities.Any(a => a.CreationDate <= endDate.GetValueOrDefault()) select t;
                }
                else
                {
                    if (startDate != null)
                        tmp = from t in tmp where t.CodeActivities.Any(a => a.EmailVerifiedDate >= startDate.GetValueOrDefault()) select t;


                    if (endDate != null)
                        tmp = from t in tmp where t.CodeActivities.Any(a => a.EmailVerifiedDate <= endDate.GetValueOrDefault()) select t;
                }

                if (codes != null && codes.Length > 0)
                    tmp = from t in tmp where codes.Contains(t.Code) select t;

                if(status != null && status.Length > 0)
                {
                    if(status == "COMPLETE")
                    {
                        tmp = from t in tmp where t.CodeActivities.Any(a => a.EmailVerifiedDate != null && a.RSIId > 0) select t;
                    }
                    if(status == "EMAILVERIFY")
                    {
                        tmp = from t in tmp where t.CodeActivities.Any(a => (a.FirstName != null && a.FirstName.Length > 0) && a.EmailVerifiedDate == null) select t;
                    }
                    else if (status == "ERROR")
                    {
                        tmp = from t in tmp where t.CodeActivities.Any(a => a.EmailVerifiedDate != null && a.RSIId == 0) select t;
                    }
                    else if(status == "ACTIVE")
                    {
                        tmp = from t in tmp where t.NumberOfUses == 0 || t.NumberOfUses > t.CodeActivities.Count() select t;
                    }
                }

                model.Activations = (from t in tmp
                                     select new ActivationViewModel
                                     {
                                         Code = t,
                                         Activities = t.CodeActivities
                                     }).Skip(startRowIndex).Take(numberOfRows).ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                model.Message = $"Error: {ex.Message}";
            }

            return await Task.FromResult(model);
        }
        public async Task<ActivationLineItemsViewModel> GetActivationLineItemsAsync(string issuer, string[] codes = null, int startRowIndex = 0, int numberOfRows = 10
            , DateTime? startDate = null, DateTime? endDate = null, string status = "", string dateSerchType = "")
        {
            ActivationLineItemsViewModel model = new ActivationLineItemsViewModel();

            try
            {
                var tmp = from a in _context.Codes where a.Issuer == issuer && a.CodeActivities.Count > 0 select a;

                if (codes != null && codes.Length > 0)
                    tmp = from t in tmp where codes.Contains(t.Code) select t;

                if (status == "ACTIVE")
                {
                    if (startDate != null)
                        tmp = from t in tmp where t.CreationDate >= startDate.GetValueOrDefault() select t;


                    if (endDate != null)
                        tmp = from t in tmp where t.CreationDate <= endDate.GetValueOrDefault() select t;
                }
                else
                {
                    if (dateSerchType == null || dateSerchType.Length < 1 || dateSerchType == "CREATIONDATE")
                    {
                        if (startDate != null)
                            tmp = from t in tmp where t.CodeActivities.Any(a => a.CreationDate >= startDate.GetValueOrDefault()) select t;


                        if (endDate != null)
                            tmp = from t in tmp where t.CodeActivities.Any(a => a.CreationDate <= endDate.GetValueOrDefault()) select t;
                    }
                    else
                    {
                        if (startDate != null)
                            tmp = from t in tmp where t.CodeActivities.Any(a => a.EmailVerifiedDate >= startDate.GetValueOrDefault()) select t;


                        if (endDate != null)
                            tmp = from t in tmp where t.CodeActivities.Any(a => a.EmailVerifiedDate <= endDate.GetValueOrDefault()) select t;
                    }
                }

                if (status != null && status.Length > 0)
                {
                    if (status == "COMPLETE")
                    {
                        tmp = from t in tmp where t.CodeActivities.Any(a => a.EmailVerifiedDate != null && a.RSIId > 0) select t;
                    }
                    if (status == "EMAILVERIFY")
                    {
                        tmp = from t in tmp where t.CodeActivities.Any(a => (a.FirstName != null && a.FirstName.Length > 0) && a.EmailVerifiedDate == null) select t;
                    }
                    else if (status == "ERROR")
                    {
                        tmp = from t in tmp where t.CodeActivities.Any(a => a.EmailVerifiedDate != null && a.RSIId == 0) select t;
                    }
                    else if (status == "ACTIVE")
                    {
                        tmp = from t in tmp where t.NumberOfUses == 0 || t.NumberOfUses > t.CodeActivities.Count() select t;
                    }
                }

                var countTmp = tmp;

                model.TotalRecords = countTmp.Count();

                model.Results = (from t in tmp
                                 select new ActivationLineItemViewModel
                                 {
                                     Address1 = t.CodeActivities.Any() ? t.CodeActivities.FirstOrDefault().Address1 : "",
                                     Address2 = t.CodeActivities.Any() ? t.CodeActivities.FirstOrDefault().Address2 : "",
                                     ChargeAmount = t.ChargeAmount,
                                     City = t.CodeActivities.Any() ? t.CodeActivities.FirstOrDefault().City : "",
                                     Code = t.Code,
                                     CodeActivityId = t.CodeActivities.Any() ? t.CodeActivities.FirstOrDefault().CodeActivityId : 0,
                                     CodeCreationDate = t.CreationDate,
                                     CodeEndDate = t.EndDate,
                                     CodeId = t.CodeId,
                                     CodeIsActive = t.IsActive,
                                     CodeStartDate = t.StartDate,
                                     CondoRewards = t.CondoRewards,
                                     CountryCode = t.CodeActivities.Any() ? t.CodeActivities.FirstOrDefault().CountryCode : "",
                                     Email = t.CodeActivities.Any() ? t.CodeActivities.FirstOrDefault().Email : "",
                                     EmailVerifiedDate = t.CodeActivities.Any() ? t.CodeActivities.FirstOrDefault().EmailVerifiedDate : null,
                                     FirstName = t.CodeActivities.Any() ? t.CodeActivities.FirstOrDefault().FirstName : "",
                                     HotelPoints = decimal.Parse(t.HotelPoints.ToString()),
                                     Issuer = t.Issuer,
                                     IssuerReference = t.IssuerReference,
                                     LastName = t.CodeActivities.Any() ? t.CodeActivities.FirstOrDefault().LastName : "",
                                     MiddleName = t.CodeActivities.Any() ? t.CodeActivities.FirstOrDefault().MiddleName : "",
                                     NumberOfUses = t.NumberOfUses,
                                     PackageId = t.PackageId,
                                     Phone1 = t.CodeActivities.Any() ? t.CodeActivities.FirstOrDefault().Phone1 : "",
                                     Phone2 = t.CodeActivities.Any() ? t.CodeActivities.FirstOrDefault().Phone2 : "",
                                     PostalCode = t.CodeActivities.Any() ? t.CodeActivities.FirstOrDefault().PostalCode : "",
                                     RSIId = t.CodeActivities.Any() ? t.CodeActivities.FirstOrDefault().RSIId : 0,
                                     StateCode = t.CodeActivities.Any() ? t.CodeActivities.FirstOrDefault().StateCode : "",
                                     Username = t.CodeActivities.Any() ? t.CodeActivities.FirstOrDefault().Username : "",
                                 }).Skip(startRowIndex).Take(numberOfRows).ToList();

                model.Message = "Success";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                model.Message = $"Error: {ex.Message}";
            }

            return await Task.FromResult(model);
        }
        public string GetBulkCodesCallbackById(int bulkCodeAuditId, string issuer)
        {
            CodesCallbackReturnViewModel model = new CodesCallbackReturnViewModel();

            try
            {
                BulkCodeAuditModel tmp = _context.BulkCodeAudits.FirstOrDefault(x => x.BulkCodeAuditId == bulkCodeAuditId && x.Issuer == issuer);

                if (tmp != null)
                {
                    model = new CodesCallbackReturnViewModel()
                    {
                        CallbackId = tmp.BulkCodeAuditId,
                        ErrorCodes = tmp.Errors.Replace("]", "") + "]",
                        Issuer = tmp.Issuer,
                        Status = tmp.FinishDate != null ? "Finished" : "Started",
                        TotalFailed = tmp.TotalFailed,
                        TotalProcessed = tmp.TotalProcessed,
                        TotalSent = tmp.TotalSent,
                        TotalSucceeded = tmp.TotalSucceeded,

                    };
                }
                else
                {

                    model.ErrorCodes.Replace("]", "");

                    if (model.ErrorCodes.Length > 1)
                        model.ErrorCodes += ",";

                    model.ErrorCodes = $"{{\"callbackId\":\"{bulkCodeAuditId}\",\"Error\":\"Record not found\"}}" + "]";
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);

                model.ErrorCodes.Replace("]", "");

                if (model.ErrorCodes.Length > 1)
                    model.ErrorCodes += ",";

                model.ErrorCodes = $"{{\"callbackId\":\"{bulkCodeAuditId}\",\"Error\":\"{ex.Message}\"}}" + "]";
            }

            return JsonConvert.SerializeObject(model);
        }
        public async Task<string> GetBulkCodesCallbackByIdAsync(int bulkCodeAuditId, string issuer)
        {
            CodesCallbackReturnViewModel model = new CodesCallbackReturnViewModel();

            try
            {
                BulkCodeAuditModel tmp = _context.BulkCodeAudits.FirstOrDefault(x => x.BulkCodeAuditId == bulkCodeAuditId && x.Issuer == issuer);

                if (tmp != null)
                {
                    model = new CodesCallbackReturnViewModel()
                    {
                        CallbackId = tmp.BulkCodeAuditId,
                        ErrorCodes = tmp.Errors.Replace("]", "") + "]",
                        Issuer = tmp.Issuer,
                        Status = tmp.FinishDate != null ? "Finished" : "Started",
                        TotalFailed = tmp.TotalFailed,
                        TotalProcessed = tmp.TotalProcessed,
                        TotalSent = tmp.TotalSent,
                        TotalSucceeded = tmp.TotalSucceeded,

                    };
                }
                else
                {

                    model.ErrorCodes.Replace("]", "");

                    if (model.ErrorCodes.Length > 1)
                        model.ErrorCodes += ",";

                    model.ErrorCodes = $"{{\"callbackId\":\"{bulkCodeAuditId}\",\"Error\":\"Record not found\"}}" + "]";
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);

                model.ErrorCodes.Replace("]", "");

                if (model.ErrorCodes.Length > 1)
                    model.ErrorCodes += ",";

                model.ErrorCodes = $"{{\"callbackId\":\"{bulkCodeAuditId}\",\"Error\":\"{ex.Message}\"}}" + "]";
            }

            return await Task.FromResult(JsonConvert.SerializeObject(model));
        }
        public string AddBulkCodesCallback(CodesViewModel codesToAdd)
        {
            CodesCallbackReturnViewModel model = new CodesCallbackReturnViewModel();
            int bulkCodeId = 0;

            try
            {
                string bulkCodeMessage = AddBulkCodesAudit(codesToAdd);

                if(int.TryParse(bulkCodeMessage, out bulkCodeId))
                {
                    return GetBulkCodesCallbackById(bulkCodeId, codesToAdd.Issuer);
                }
                else
                {
                    model.ErrorCodes.Replace("]", "");

                    if (model.ErrorCodes.Length > 1)
                        model.ErrorCodes += ",";

                    model.ErrorCodes = $"{{\"callbackId\":\"{bulkCodeId}\",\"Error\":\"{bulkCodeMessage}\"}}" + "]";

                    return JsonConvert.SerializeObject(model);
                }
               
            }
            catch (Exception ex)
            {
                
                _logger.LogError(ex.Message);

                model.ErrorCodes.Replace("]", "");

                if (model.ErrorCodes.Length > 1)
                    model.ErrorCodes += ",";

                model.ErrorCodes = $"{{\"callbackId\":\"{bulkCodeId}\",\"Error\":\"{ex.Message}\"}}" + "]";

                return JsonConvert.SerializeObject(model);
            }
        }
        public async Task<string> AddBulkCodesCallbackAsync(CodesViewModel codesToAdd)
        {
            CodesCallbackReturnViewModel model = new CodesCallbackReturnViewModel();
            int bulkCodeId = 0;

            try
            {
                string bulkCodeMessage = await AddBulkCodesAuditAsync(codesToAdd);

                if (int.TryParse(bulkCodeMessage, out bulkCodeId))
                {
                    return await GetBulkCodesCallbackByIdAsync(bulkCodeId, codesToAdd.Issuer);
                }
                else
                {
                    model.ErrorCodes.Replace("]", "");

                    if (model.ErrorCodes.Length > 1)
                        model.ErrorCodes += ",";

                    model.ErrorCodes = $"{{\"callbackId\":\"{bulkCodeId}\",\"Error\":\"{bulkCodeMessage}\"}}" + "]";

                    return JsonConvert.SerializeObject(model);
                }

            }
            catch (Exception ex)
            {

                _logger.LogError(ex.Message);

                model.ErrorCodes.Replace("]", "");

                if (model.ErrorCodes.Length > 1)
                    model.ErrorCodes += ",";

                model.ErrorCodes = $"{{\"callbackId\":\"{bulkCodeId}\",\"Error\":\"{ex.Message}\"}}" + "]";

                return JsonConvert.SerializeObject(model);
            }
        }
        public string AddBulkCodes(CodesViewModel codesToAdd, int bulkCodeId = 0, int batchCount = 1000)
        {
            List<CodeViewModel> currentCodes = new List<CodeViewModel>();
            List<KeyValuePair<string, string>> currentErrors = new List<KeyValuePair<string, string>>();
            int currentProcessed = 0, currentSucceeded = 0, succeededTmp = 0, currentFailed = 0;
            CodesReturnViewModel model = new CodesReturnViewModel();

            try
            {
                string bulkCodeMessage = "";
                if (bulkCodeId < 1)
                {
                    bulkCodeMessage = AddBulkCodesAudit(codesToAdd);
                    int.TryParse(bulkCodeMessage, out bulkCodeId);
                }

                if (bulkCodeId > 0)
                {

                    model.Issuer = codesToAdd.Issuer;
                    model.TotalSent = codesToAdd.Codes.Count();

                    foreach (CodeViewModel code in codesToAdd.Codes)
                    {
                        ++model.TotalProcessed;
                        ++currentProcessed;

                        ActivationViewModel activationCheck = GetActivationInfo(code.Code, codesToAdd.Issuer);

                        if (activationCheck.CodeStatus.Key)
                        {
                            model.ErrorCodes.Add(new KeyValuePair<string, string>(code.Code, activationCheck.CodeStatus.Value));
                            currentErrors.Add(new KeyValuePair<string, string>(code.Code, activationCheck.CodeStatus.Value));
                            ++model.TotalFailed;
                            ++currentFailed;
                        }
                        else
                        {
                            currentCodes.Add(code);

                            CodeModel cm = new CodeModel()
                            {
                                StartDate = code.StartDate,
                                HotelPoints = code.Points,
                                NumberOfUses = code.NumberOfUses,
                                Issuer = codesToAdd.Issuer,
                                ChargeAmount = code.ChargeAmount,
                                Code = code.Code,
                                CreationDate = DateTime.Now,
                                EndDate = code.EndDate,
                                IsActive = true
                            };

                            _context.Codes.Add(cm);
                            ++succeededTmp;

                            if (model.TotalProcessed % batchCount == 0)
                            {
                                _context.SaveChanges();
                                model.TotalSucceeded += succeededTmp;
                                currentSucceeded = succeededTmp;
                                UpdateBulkCodesAudit(bulkCodeId, currentProcessed, currentSucceeded, currentFailed, currentErrors);

                                currentFailed = 0;
                                currentProcessed = 0;
                                currentSucceeded = 0;
                                succeededTmp = 0;

                                currentErrors = new List<KeyValuePair<string, string>>();
                                currentCodes = new List<CodeViewModel>();
                            }
                        }

                    }
                    _context.SaveChanges();

                    model.TotalSucceeded += succeededTmp;
                    currentSucceeded = succeededTmp;
                    UpdateBulkCodesAudit(bulkCodeId, currentProcessed, currentSucceeded, currentFailed, currentErrors);

                    FinishBulkCodesAudit(bulkCodeId);
                }
                else
                {
                    model.ErrorCodes.Add(new KeyValuePair<string, string>("Bulk Code Audit Failed", bulkCodeMessage));
                    currentErrors.Add(new KeyValuePair<string, string>("Bulk Code Audit Failed", bulkCodeMessage));
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                model.TotalFailed += currentCodes.Count();

                List<KeyValuePair<string, string>> errors = (from e in currentCodes
                                                             select new KeyValuePair<string, string>(e.Code, ex.Message)
                                                            ).ToList();

                model.ErrorCodes.AddRange(errors);
                currentErrors.AddRange(errors);
            }

            return JsonConvert.SerializeObject(model);
        }
        public async Task<string> AddBulkCodesAsync(CodesViewModel codesToAdd, int bulkCodeId, int batchCount = 1000)
        {
            List<CodeViewModel> currentCodes = new List<CodeViewModel>();
            List<KeyValuePair<string, string>> currentErrors = new List<KeyValuePair<string, string>>();
            int currentProcessed = 0, currentSucceeded = 0, succeededTmp = 0, currentFailed = 0;
            CodesReturnViewModel model = new CodesReturnViewModel();
            
            try
            {
                string bulkCodeMessage = "";
                if (bulkCodeId < 1)
                {
                    bulkCodeMessage = await AddBulkCodesAuditAsync(codesToAdd);
                    int.TryParse(bulkCodeMessage, out bulkCodeId);
                }

                if (bulkCodeId > 0)
                {

                    model.Issuer = codesToAdd.Issuer;
                    model.TotalSent = codesToAdd.Codes.Count();

                    foreach (CodeViewModel code in codesToAdd.Codes)
                    {
                        ++model.TotalProcessed;
                        ++currentProcessed;

                        ActivationViewModel activationCheck = await GetActivationInfoAsync(code.Code, codesToAdd.Issuer);

                        if (activationCheck.CodeStatus.Key)
                        {
                            model.ErrorCodes.Add(new KeyValuePair<string, string>(code.Code, activationCheck.CodeStatus.Value));
                            currentErrors.Add(new KeyValuePair<string, string>(code.Code, activationCheck.CodeStatus.Value));
                            ++model.TotalFailed;
                            ++currentFailed;
                        }
                        else
                        {
                            currentCodes.Add(code);

                            CodeModel cm = new CodeModel()
                            {
                                StartDate = code.StartDate,
                                HotelPoints = code.Points,
                                NumberOfUses = code.NumberOfUses,
                                Issuer = codesToAdd.Issuer,
                                ChargeAmount = code.ChargeAmount,
                                Code = code.Code,
                                CreationDate = DateTime.Now,
                                EndDate = code.EndDate,
                                IsActive = true
                            };

                            _context.Codes.Add(cm);
                            ++succeededTmp;
                            
                            if (model.TotalProcessed % batchCount == 0)
                            {
                                _context.SaveChanges();
                                model.TotalSucceeded += succeededTmp;
                                currentSucceeded = succeededTmp;
                                await UpdateBulkCodesAuditAsync(bulkCodeId, currentProcessed,currentSucceeded, currentFailed, currentErrors);

                                currentFailed = 0;
                                currentProcessed = 0;
                                currentSucceeded = 0;
                                succeededTmp = 0;

                                currentErrors = new List<KeyValuePair<string, string>>();
                                currentCodes = new List<CodeViewModel>();
                            }
                        }

                    }

                    await _context.SaveChangesAsync();
                    
                    model.TotalSucceeded += succeededTmp;

                    currentSucceeded = succeededTmp;
                    await UpdateBulkCodesAuditAsync(bulkCodeId, currentProcessed, currentSucceeded, currentFailed, currentErrors);

                    await FinishBulkCodesAuditAsync(bulkCodeId);
                }
                else
                {
                    model.ErrorCodes.Add(new KeyValuePair<string, string>("Bulk Code Audit Failed", bulkCodeMessage));
                    currentErrors.Add(new KeyValuePair<string, string>("Bulk Code Audit Failed", bulkCodeMessage));
                }
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message);
                model.TotalFailed += currentCodes.Count();

                List<KeyValuePair<string, string>> errors = (from e in currentCodes
                                                             select new KeyValuePair<string, string>(e.Code, ex.Message)
                                                            ).ToList();
                
                model.ErrorCodes.AddRange(errors);
                currentErrors.AddRange(errors);
            }

            return JsonConvert.SerializeObject(model);
        }
        public string AddRangeCodes(string startRange, string endRange, string issuer, int hotelPoints, int condoRewards = 0, int numberOfUses = 1 
            , decimal chargeAmount = 0, DateTime? startDate = null, DateTime? endDate = null)
        {
            char[] alphabet = new char[] { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z' };
            CodesViewModel codesToAdd = new CodesViewModel();

            CodesReturnViewModel model = new CodesReturnViewModel();

            try
            {
                string[] splitStart = Regex.Split(startRange, @"(?<=\p{L})(?=\p{N})");
                string[] splitEnd = Regex.Split(endRange, @"(?<=\p{L})(?=\p{N})");

                char[] startChar = splitStart[0].ToCharArray();
                char[] endChar = splitEnd[0].ToCharArray();

                int startCharCount = startChar.Count();
                int endCharCount = endChar.Count();
                
                int startNum = int.Parse(splitStart[1]);
                int endNum = int.Parse(splitEnd[1]);
                
                if (splitStart[0] == splitEnd[0])
                {
                    for(int i = startNum; i <= endNum; i++)
                    {
                        codesToAdd.Codes.Add(new CodeViewModel()
                        {
                            ChargeAmount = chargeAmount,
                            Code = $"{splitStart[0]}{i.ToString().PadLeft(5, '0')}",
                            CondoRewards = condoRewards,
                            EndDate = endDate,
                            IssuerReference = issuer,
                            NumberOfUses = numberOfUses,
                            Points = hotelPoints,
                            StartDate = startDate
                        });
                        
                    }

                    int bulkCodeId = 0;
                    codesToAdd.Issuer = issuer;
                    string bulkCodeMessage = AddBulkCodesAudit(codesToAdd);
                    int.TryParse(bulkCodeMessage, out bulkCodeId);

                    string returnString = AddBulkCodes(codesToAdd, bulkCodeId);

                    model = JsonConvert.DeserializeObject<CodesReturnViewModel>(returnString);
                }
                else if(endCharCount >= startCharCount)
                {
                    int startLevel = 0;
                    int endLevel = endCharCount - 1;
                    int startIndex = 0;
                    int currentIndex = 0;
                    int currentLevel = 0;
                    int currentCount = startNum;
                    int endIndex = 0;

                    endIndex = Array.IndexOf(alphabet, endChar[endCharCount - 1]);

                    for (int x = 0; x < endCharCount; x++)
                    {
                        if(startChar[x] != endChar[x])
                        {
                            startLevel = x;
                            currentLevel = x;
                            startIndex = Array.IndexOf(alphabet, startChar[x]);
                            currentIndex = startIndex;
                            
                            break;
                        }
                    }

                    string startCodeDefault = "";

                    for (int p = 0; p < startLevel; p++)
                    {
                        startCodeDefault += startChar[p];
                    }

                    do
                    {
                        string code = $"{startCodeDefault}{alphabet[currentIndex]}{currentCount.ToString().PadLeft(5, '0')}";

                        codesToAdd.Codes.Add(new CodeViewModel()
                        {
                            ChargeAmount = chargeAmount,
                            Code = code,
                            CondoRewards = condoRewards,
                            EndDate = endDate,
                            IssuerReference = issuer,
                            NumberOfUses = numberOfUses,
                            Points = hotelPoints,
                            StartDate = startDate
                        });

                        if(currentCount < 99999)
                        {
                            currentCount++;

                        }
                        else
                        {
                            if (currentIndex == 25)
                            {
                                startCodeDefault = $"{startCodeDefault}{alphabet[currentIndex]}";
                                currentIndex = 0;
                                currentLevel++;
                                
                            }
                            else
                            {
                                currentIndex++;
                            }
                            currentCount = 1;
                        }

                    } while ((endLevel >= currentLevel && endIndex > currentIndex) || endNum >= currentCount || currentLevel < endLevel);

                    int bulkCodeId = 0;

                    string bulkCodeMessage = AddBulkCodesAudit(codesToAdd);
                    int.TryParse(bulkCodeMessage, out bulkCodeId);

                    string returnString = AddBulkCodes(codesToAdd, bulkCodeId);

                    model = JsonConvert.DeserializeObject<CodesReturnViewModel>(returnString);
                }
                else
                {
                    model = new CodesReturnViewModel();
                    model.ErrorCodes.Add(new KeyValuePair<string, string>("Range", "Start and end is not correct format"));
                    model.Issuer = issuer;
                }


            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                model = new CodesReturnViewModel();
                model.ErrorCodes.Add(new KeyValuePair<string, string>("CatchAll", ex.Message));
                model.Issuer = issuer;
            }

            return JsonConvert.SerializeObject(model);
        }
        public async Task<string> AddRangeCodesAsync(string startRange, string endRange, string issuer, int hotelPoints, int condoRewards = 0, int numberOfUses = 1
            , decimal chargeAmount = 0, DateTime? startDate = null, DateTime? endDate = null)
        {
            char[] alphabet = new char[] { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z' };
            CodesViewModel codesToAdd = new CodesViewModel();

            CodesReturnViewModel model = new CodesReturnViewModel();

            try
            {
                string[] splitStart = Regex.Split(startRange, @"(?<=\p{L})(?=\p{N})");
                string[] splitEnd = Regex.Split(endRange, @"(?<=\p{L})(?=\p{N})");

                char[] startChar = splitStart[0].ToCharArray();
                char[] endChar = splitEnd[0].ToCharArray();

                int startCharCount = startChar.Count();
                int endCharCount = endChar.Count();

                int startNum = int.Parse(splitStart[1]);
                int endNum = int.Parse(splitEnd[1]);

                if (splitStart[0] == splitEnd[0])
                {
                    for (int i = startNum; i <= endNum; i++)
                    {
                        codesToAdd.Codes.Add(new CodeViewModel()
                        {
                            ChargeAmount = chargeAmount,
                            Code = $"{splitStart[0]}{i.ToString().PadLeft(5, '0')}",
                            CondoRewards = condoRewards,
                            EndDate = endDate,
                            IssuerReference = issuer,
                            NumberOfUses = numberOfUses,
                            Points = hotelPoints,
                            StartDate = startDate
                        });

                    }

                    int bulkCodeId = 0;
                    codesToAdd.Issuer = issuer;
                    string bulkCodeMessage = await AddBulkCodesAuditAsync(codesToAdd);
                    int.TryParse(bulkCodeMessage, out bulkCodeId);

                    string returnString = await AddBulkCodesAsync(codesToAdd, bulkCodeId);

                    model = JsonConvert.DeserializeObject<CodesReturnViewModel>(returnString);
                }
                else if (endCharCount >= startCharCount)
                {
                    int startLevel = 0;
                    int endLevel = endCharCount - 1;
                    int startIndex = 0;
                    int currentIndex = 0;
                    int currentLevel = 0;
                    int currentCount = startNum;
                    int endIndex = 0;

                    endIndex = Array.IndexOf(alphabet, endChar[endCharCount - 1]);

                    for (int x = 0; x < endCharCount; x++)
                    {
                        if (startChar[x] != endChar[x])
                        {
                            startLevel = x;
                            currentLevel = x;
                            startIndex = Array.IndexOf(alphabet, startChar[x]);
                            currentIndex = startIndex;

                            break;
                        }
                    }

                    string startCodeDefault = "";

                    for (int p = 0; p < startLevel; p++)
                    {
                        startCodeDefault += startChar[p];
                    }

                    do
                    {
                        string code = $"{startCodeDefault}{alphabet[currentIndex]}{currentCount.ToString().PadLeft(5, '0')}";

                        codesToAdd.Codes.Add(new CodeViewModel()
                        {
                            ChargeAmount = chargeAmount,
                            Code = code,
                            CondoRewards = condoRewards,
                            EndDate = endDate,
                            IssuerReference = issuer,
                            NumberOfUses = numberOfUses,
                            Points = hotelPoints,
                            StartDate = startDate
                        });

                        if (currentCount < 99999)
                        {
                            currentCount++;

                        }
                        else
                        {
                            if (currentIndex == 25)
                            {
                                startCodeDefault = $"{startCodeDefault}{alphabet[currentIndex]}";
                                currentIndex = 0;
                                currentLevel++;

                            }
                            else
                            {
                                currentIndex++;
                            }
                            currentCount = 0;
                        }

                    } while ((endLevel >= currentLevel && endIndex > currentIndex) || endNum >= currentCount || currentLevel < endLevel);

                    int bulkCodeId = 0;
                    codesToAdd.Issuer = issuer;
                    string bulkCodeMessage = await AddBulkCodesAuditAsync(codesToAdd);
                    int.TryParse(bulkCodeMessage, out bulkCodeId);

                    string returnString = await AddBulkCodesAsync(codesToAdd, bulkCodeId);

                    model = JsonConvert.DeserializeObject<CodesReturnViewModel>(returnString);
                }
                else
                {
                    model = new CodesReturnViewModel();
                    model.ErrorCodes.Add(new KeyValuePair<string, string>("Range", "Start and end is not correct format"));
                    model.Issuer = issuer;
                }


            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                model = new CodesReturnViewModel();
                model.ErrorCodes.Add(new KeyValuePair<string, string>("CatchAll", ex.Message));
                model.Issuer = issuer;
            }

            return JsonConvert.SerializeObject(model);
        }
        public ActivationViewModel GetActivationInfoByCodeActivityId(int codeActivityId)
        {
            ActivationViewModel model = new ActivationViewModel();

            try
            {
                model.SelectedActivity = _context.CodeActivities.FirstOrDefault(x => x.CodeActivityId == codeActivityId);

                if(model.SelectedActivity != null)
                {
                    model.Code = _context.Codes.FirstOrDefault(x => x.CodeId == model.SelectedActivity.CodeId);

                    if(model.Code != null)
                    {
                        model.Activities = _context.CodeActivities.Where(x => x.CodeId == model.Code.CodeId).ToList();
                    }
                    else
                    {
                        model.Message = "Error: Code is not found.";
                    }
                }
                else
                {
                    model.Message = "Error: Code Activity not found.";
                }
                
            }
            catch (Exception ex)
            {
                model.Message = $"Error: {ex.Message}";
                _logger.LogError(ex.Message);
            }

            return model;
        }
        public async Task<ActivationViewModel> GetActivationInfoByCodeActivityIdAsync(int codeActivityId)
        {
            ActivationViewModel model = new ActivationViewModel();

            try
            {
                model.SelectedActivity = _context.CodeActivities.FirstOrDefault(x => x.CodeActivityId == codeActivityId);

                if (model.SelectedActivity != null)
                {
                    model.Code = _context.Codes.FirstOrDefault(x => x.CodeId == model.SelectedActivity.CodeId);

                    if (model.Code != null)
                    {
                        model.Activities = _context.CodeActivities.Where(x => x.CodeId == model.Code.CodeId).ToList();
                    }
                    else
                    {
                        model.Message = "Error: Code is not found.";
                    }
                }
                else
                {
                    model.Message = "Error: Code Activity not found.";
                }

            }
            catch (Exception ex)
            {
                model.Message = $"Error: {ex.Message}";
                _logger.LogError(ex.Message);
            }

            return await Task.FromResult(model);
        }
        public ActivationViewModel GetActivationInfo(string code, string issuer)
        {
            ActivationViewModel model = new ActivationViewModel();

            try
            {
                model.Code = _context.Codes.FirstOrDefault(x => x.Code == code && x.Issuer == issuer);

                if(model.Code != null && model.Code.Code != null && model.Code.Code.Length > 0)
                {
                    model.Activities = _context.CodeActivities.Where(x => x.CodeId == model.Code.CodeId && x.IsActive).ToList();
                }
                else
                {
                    model.Message = "Error: Code not found";

                }
            }
            catch (Exception ex)
            {
                model.Message = $"Error: {ex.Message}";
                _logger.LogError(ex.Message);
            }

            return model;
        }
        public async Task<ActivationViewModel> GetActivationInfoAsync(string code, string issuer)
        {
            ActivationViewModel model = new ActivationViewModel();

            try
            {
                model.Code = _context.Codes.FirstOrDefault(x => x.Code == code && x.Issuer == issuer);

                if (model.Code != null && model.Code.Code != null && model.Code.Code.Length > 0)
                {
                    model.Activities = _context.CodeActivities.Where(x => x.CodeId == model.Code.CodeId && x.IsActive).ToList();
                }
                else
                {
                    model.Message = "Error: Code not found";

                }
            }
            catch (Exception ex)
            {
                model.Message = $"Error: {ex.Message}";
                _logger.LogError(ex.Message);
            }

            return await Task.FromResult(model);
        }
        public string UpdateActivityRSIId(int codeActivityId, int rsiId)
        {
            string message = "";

            try
            {
                if (rsiId > 0)
                {
                    CodeActivityModel model = _context.CodeActivities.FirstOrDefault(x => x.CodeActivityId == codeActivityId);
                    if (model != null)
                    {
                        if (model.RSIId < 1)
                        {
                            model.RSIId = rsiId;
                            _context.SaveChanges();

                            message = "Success";
                        }
                        else
                        {
                            message = $"Error: RSIId already set to ({model.RSIId})";
                        }
                    }
                    else
                    {
                        message = "Error: Activity not found";
                    }
                }
                else
                {
                    message = "Error: Invalid RSI Id";
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                message = $"Error: {ex.Message}";
            }

            return message;
        }
        public async Task<string> UpdateActivityRSIIdAsync(int codeActivityId, int rsiId)
        {
            string message = "";

            try
            {
                if (rsiId > 0)
                {
                    CodeActivityModel model = _context.CodeActivities.FirstOrDefault(x => x.CodeActivityId == codeActivityId);
                    if (model != null)
                    {
                        if (model.RSIId < 1)
                        {
                            model.RSIId = rsiId;
                            await _context.SaveChangesAsync();

                            message = "Success";
                        }
                        else
                        {
                            message = $"Error: RSIId already set to ({model.RSIId})";
                        }
                    }
                    else
                    {
                        message = "Error: Activity not found";
                    }
                }
                else
                {
                    message = "Error: Invalid RSI Id";
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                message = $"Error: {ex.Message}";
            }

            return message;
        }
        public ActivationViewModel VerifyEmail(int codeActivityId)
        {
            ActivationViewModel model = new ActivationViewModel();

            try
            {
                model.SelectedActivity = _context.CodeActivities.FirstOrDefault(x => x.CodeActivityId == codeActivityId);

                if (model.SelectedActivity != null)
                {
                    model.Code = _context.Codes.FirstOrDefault(x => x.CodeId == model.SelectedActivity.CodeId);

                    if(model.Code != null)
                    {
                        model.Activities = _context.CodeActivities.Where(x => x.CodeId == model.Code.CodeId).ToList();

                        if(model.SelectedActivity.EmailVerifiedDate == null)
                        {
                            var activity = _context.CodeActivities.FirstOrDefault(x => x.CodeActivityId == codeActivityId);

                            if (activity != null)
                            {
                                activity.EmailVerifiedDate = DateTime.Now;
                                _context.SaveChanges();

                                model.SelectedActivity.EmailVerifiedDate = activity.EmailVerifiedDate;

                                model.Message = "Success";
                            }
                            else
                            {
                                model.Message = "Error: Code activity id not found.";
                            }
                        }
                        else
                        {
                            model.Message = $"Error: Email has already been verified on {model.SelectedActivity.EmailVerifiedDate.GetValueOrDefault().ToString("MM/dd/yyyy")}";
                        }
                    }
                    else
                    {
                        model.Message = "Error: Code not found";
                    }
                }
                else
                {
                    model.Message = "Error: Code activity not found";
                }
            }
            catch (Exception ex)
            {
                if (model == null)
                    model = new ActivationViewModel();

                model.Message = $"Error; {ex.Message}";
                _logger.LogError(ex.Message);
            }

            return model;
        }
        public async Task<ActivationViewModel> VerifyEmailAsync(int codeActivityId)
        {
            ActivationViewModel model = new ActivationViewModel();

            try
            {
                model.SelectedActivity = _context.CodeActivities.FirstOrDefault(x => x.CodeActivityId == codeActivityId);

                if (model.SelectedActivity != null)
                {
                    model.Code = _context.Codes.FirstOrDefault(x => x.CodeId == model.SelectedActivity.CodeId);

                    if (model.Code != null)
                    {
                        model.Activities = _context.CodeActivities.Where(x => x.CodeId == model.Code.CodeId).ToList();

                        if (model.SelectedActivity.EmailVerifiedDate == null)
                        {
                            var activity = _context.CodeActivities.FirstOrDefault(x => x.CodeActivityId == codeActivityId);

                            if (activity != null)
                            {
                                activity.EmailVerifiedDate = DateTime.Now;
                                await _context.SaveChangesAsync();

                                model.SelectedActivity.EmailVerifiedDate = activity.EmailVerifiedDate;

                                model.Message = "Success";
                            }
                            else
                            {
                                model.Message = "Error: Code activity id not found.";
                            }
                        }
                        else
                        {
                            model.Message = $"Error: Email has already been verified on {model.SelectedActivity.EmailVerifiedDate.GetValueOrDefault().ToString("MM/dd/yyyy")}";
                        }
                    }
                    else
                    {
                        model.Message = "Error: Code not found";
                    }
                }
                else
                {
                    model.Message = "Error: Code activity not found";
                }
            }
            catch (Exception ex)
            {
                if (model == null)
                    model = new ActivationViewModel();

                model.Message = $"Error; {ex.Message}";
                _logger.LogError(ex.Message);
            }

            return model;
        }
        public string AddRSIIdToCodeActivity(int codeActivityId, int rsiId)
        {
            string message = "";

            try
            {
                if (rsiId > 0)
                {
                    CodeActivityModel model = _context.CodeActivities.FirstOrDefault(x => x.CodeActivityId == codeActivityId);

                    if (model != null)
                    {
                        if (model.RSIId < 1)
                        {
                            model.RSIId = rsiId;
                            _context.SaveChanges();
                            message = "Success";
                        }
                        else
                        {
                            message = $"Error: RSI Id is already added to this record ({model.RSIId} is the current RSI Id on the record and you were trying to add {rsiId})";
                        }
                    }
                    else
                    {
                        message = "Error: Code id not found.";
                    }
                }
                else
                {
                    message = "Error: Code activity id has to be greater than 0";
                }
            }
            catch (Exception ex)
            {
                message = $"Error: {ex.Message}";
                _logger.LogError(ex.Message);
            }

            return message;
        }
        public async Task<string> AddRSIIdToCodeActivityAsync(int codeActivityId, int rsiId)
        {
            string message = "";

            try
            {
                if (rsiId > 0)
                {
                    CodeActivityModel model = _context.CodeActivities.FirstOrDefault(x => x.CodeActivityId == codeActivityId);

                    if (model != null)
                    {
                        if (model.RSIId < 1)
                        {
                            model.RSIId = rsiId;
                            await _context.SaveChangesAsync();
                            message = "Success";
                        }
                        else
                        {
                            message = $"Error: RSI Id is already added to this record ({model.RSIId} is the current RSI Id on the record and you were trying to add {rsiId})";
                        }
                    }
                    else
                    {
                        message = "Error: Code id not found.";
                    }
                }
                else
                {
                    message = "Error: Code activity id has to be greater than 0";
                }
            }
            catch (Exception ex)
            {
                message = $"Error: {ex.Message}";
                _logger.LogError(ex.Message);
            }

            return message;
        }
        public async Task<string> AddNewCode(int rsiId, string code, string creatorIP)
        {
            string message = "";

            try
            {
                CodeActivityModel origionalRecord = _context.CodeActivities.OrderBy(o => o.CodeActivityId).FirstOrDefault(x => x.RSIId == rsiId);

                if (origionalRecord != null && origionalRecord.CodeActivityId > 0)
                {
                    if (origionalRecord.EmailVerifiedDate == null)
                    {
                        message = "Error: Email not verified.";
                    }
                    else if (origionalRecord.RSIId == 0)
                    {
                        message = "Error: RSI Id is 0 on the origional code.  Please contact support.";
                    }
                    else
                    {
                        CodeModel codeModel = _context.Codes.FirstOrDefault(x => x.Code.ToUpper() == code.ToUpper());

                        if (codeModel != null && codeModel.CodeId > 0)
                        {
                            CodeActivityModel newCode = new CodeActivityModel()
                            {
                                ActivationCode = code.ToUpper(),
                                Address1 = origionalRecord.Address1,
                                Address2 = origionalRecord.Address2,
                                City = origionalRecord.City,
                                CodeId = codeModel.CodeId,
                                CountryCode = origionalRecord.CountryCode,
                                CreationDate = DateTime.Now,
                                CreatorIP = creatorIP,
                                DeactivationDate = null,
                                Email = origionalRecord.Email,
                                EmailVerifiedDate = DateTime.Now,
                                FirstName = origionalRecord.FirstName,
                                HotelPoints = codeModel.HotelPoints,
                                IsActive = true,
                                Issuer = origionalRecord.Issuer,
                                LastName = origionalRecord.LastName,
                                MiddleName = origionalRecord.MiddleName,
                                Password = origionalRecord.Password,
                                Phone1 = origionalRecord.Phone1,
                                Phone2 = origionalRecord.Phone2,
                                PostalCode = origionalRecord.PostalCode,
                                RSIId = rsiId,
                                StateCode = origionalRecord.StateCode,
                                Username = origionalRecord.Username
                            };

                            await _context.CodeActivities.AddAsync(newCode);
                            await _context.SaveChangesAsync();
                        }
                        else
                        {
                            message = "Error: Code not found";
                        }
                    }
                }
                else
                    message = "Error: rsiId not found";
            }
            catch (Exception ex)
            {
                message = $"Error: {ex.Message}";
            }

            return message;
        }
        public ActivationModel ActivateCode(ActivationModel model)
        {
            //string url = "http://api.accessrsi.com/api/codes/emailvalidation/";

            try
            {
                int ctPhone1 = 0, ctPhone2 = 0, ct = 0;
                
                int ctEmail = _context.CodeActivities.Count(c => c.IsActive && c.Code.Issuer == model.Issuer && c.Email == model.Email);
                int ctUsername = _context.CodeActivities.Count(c => c.IsActive && c.Code.Issuer == model.Issuer && c.Username == model.Username);

                if (model.Phone1 != null && model.Phone1.Length > 0)
                    ctPhone1 = _context.CodeActivities.Count(c => c.IsActive && c.Code.Issuer == model.Issuer && (c.Phone1 == model.Phone1 || c.Phone2 == model.Phone1));

                if(model.Phone2 != null && model.Phone2.Length > 0)
                    ctPhone2 = _context.CodeActivities.Count(c => c.IsActive && c.Code.Issuer == model.Issuer && (c.Phone1 == model.Phone2 || c.Phone2 == model.Phone2));

                ct = ctPhone1 + ctPhone2 + ctEmail + ctUsername;

                if (ct < 1)
                {
                    ActivationViewModel codeResults = GetActivationInfo(model.ActivationCode, model.Issuer);

                    if (codeResults.CodeStatus.Key)
                    {
                        CodeModel codeUpdate = _context.Codes.FirstOrDefault(x => x.CodeId == codeResults.Code.CodeId);

                        if (codeUpdate != null && codeUpdate.CodeId > 0)
                        {
                            CodeActivityModel codeActivity = new CodeActivityModel()
                            {
                                CodeId = codeResults.Code.CodeId,
                                CreationDate = DateTime.Now,
                                CreatorIP = model.CreatorIP,
                                IsActive = true,
                                RSIId = 0,
                                Email = model.Email,
                                ActivationCode = model.ActivationCode,
                                Address1 = model.Address1,
                                Address2 = model.Address2,
                                City = model.City,
                                StateCode = model.StateCode,
                                CountryCode = model.CountryCode,
                                DeactivationDate = null,
                                EmailVerifiedDate = null,
                                FirstName = model.FirstName,
                                Issuer = model.Issuer,
                                LastName = model.LastName,
                                MiddleName = model.MiddleName,
                                Password = model.Password,
                                Phone1 = model.Phone1,
                                Phone2 = model.Phone2,
                                PostalCode = model.PostalCode,
                                Username = model.Username,
                                HotelPoints = codeUpdate.HotelPoints
                            };

                            codeUpdate.CodeActivities.Add(codeActivity);
                            _context.SaveChanges();
                            model.Message = codeActivity.CodeActivityId.ToString();
                        }
                        else
                        {
                            model.Message = "Error: Code not found.";
                        }
                    }
                    else
                    {
                        model.Message = $"Error: {codeResults.CodeStatus.Value}";
                    }
                }
                else
                {
                    if (ctPhone1 > 0)
                        model.Message += model.Message.Length > 0 ? " | " : "Error: " + "Phone1 is already in the system.";

                    if (ctPhone2 > 0)
                        model.Message += model.Message.Length > 0 ? " | " : "Error: " + "Phone2 is already in the system.";

                    if (ctEmail > 0)
                        model.Message += model.Message.Length > 0 ? " | " : "Error: " + "Email is already in the system.";

                    if (ctUsername > 0)
                        model.Message += model.Message.Length > 0 ? " | " : "Error: " + "Username is already in the system.";
                }
            }
            catch (Exception ex)
            {
                model.Message = $"Error: {ex.Message}";
                _logger.LogError(ex.Message);
            }

            return model;
        }
        public async Task<ActivationModel> ActivateCodeAsync(ActivationModel model)
        {
            //string url = "http://api.accessrsi.com/api/codes/emailvalidation/";

            try
            {
                int ctPhone1 = 0, ctPhone2 = 0, ct = 0;

                int ctEmail = _context.CodeActivities.Count(c => c.IsActive && c.Code.Issuer == model.Issuer && c.Email == model.Email);
                int ctUsername = _context.CodeActivities.Count(c => c.IsActive && c.Code.Issuer == model.Issuer && c.Username == model.Username);

                if (model.Phone1 != null && model.Phone1.Length > 0)
                    ctPhone1 = _context.CodeActivities.Count(c => c.IsActive && c.Code.Issuer == model.Issuer && (c.Phone1 == model.Phone1 || c.Phone2 == model.Phone1));

                if (model.Phone2 != null && model.Phone2.Length > 0)
                    ctPhone2 = _context.CodeActivities.Count(c => c.IsActive && c.Code.Issuer == model.Issuer && (c.Phone1 == model.Phone2 || c.Phone2 == model.Phone2));

                ct = ctPhone1 + ctPhone2 + ctEmail + ctUsername;

                if (ct < 1)
                {
                    ActivationViewModel codeResults = await GetActivationInfoAsync(model.ActivationCode, model.Issuer);

                    if (codeResults.CodeStatus.Key)
                    {
                        CodeModel codeUpdate = _context.Codes.FirstOrDefault(x => x.CodeId == codeResults.Code.CodeId);

                        if (codeUpdate != null && codeUpdate.CodeId > 0)
                        {
                            CodeActivityModel codeActivity = new CodeActivityModel()
                            {
                                CodeId = codeResults.Code.CodeId,
                                CreationDate = DateTime.Now,
                                CreatorIP = model.CreatorIP,
                                IsActive = true,
                                RSIId = 0,
                                Email = model.Email,
                                ActivationCode = model.ActivationCode,
                                Address1 = model.Address1,
                                Address2 = model.Address2,
                                City = model.City,
                                StateCode = model.StateCode,
                                CountryCode = model.CountryCode,
                                DeactivationDate = null,
                                EmailVerifiedDate = null,
                                FirstName = model.FirstName,
                                Issuer = model.Issuer,
                                LastName = model.LastName,
                                MiddleName = model.MiddleName,
                                Password = model.Password,
                                Phone1 = model.Phone1,
                                Phone2 = model.Phone2,
                                PostalCode = model.PostalCode,
                                Username = model.Username
                            };

                            codeUpdate.CodeActivities.Add(codeActivity);
                            await _context.SaveChangesAsync();
                            model.Message = codeActivity.CodeActivityId.ToString();
                        }
                        else
                        {
                            model.Message = "Error: Code not found.";
                        }
                    }
                    else
                    {
                        model.Message = $"Error: {codeResults.CodeStatus.Value}";
                    }
                }
                else
                {
                    if (ctPhone1 > 0)
                        model.Message += model.Message.Length > 0 ? " | " : "Error: " + "Phone1 is already in the system.";

                    if (ctPhone2 > 0)
                        model.Message += model.Message.Length > 0 ? " | " : "Error: " + "Phone2 is already in the system.";

                    if (ctEmail > 0)
                        model.Message += model.Message.Length > 0 ? " | " : "Error: " + "Email is already in the system.";

                    if (ctUsername > 0)
                        model.Message += model.Message.Length > 0 ? " | " : "Error: " + "Username is already in the system.";
                }
            }
            catch (Exception ex)
            {
                model.Message = $"Error: {ex.Message}";
                _logger.LogError(ex.Message);
            }

            return model;
        }
        public string AddBulkCodesAudit(CodesViewModel codesToAdd)
        {
            string message = "";

            try
            {
                BulkCodeAuditModel model = new BulkCodeAuditModel();
                model.Issuer = codesToAdd.Issuer;
                model.TotalSent = codesToAdd.Codes.Count();
                model.CreationDate = DateTime.Now;
                model.OrigionalFileSent = JsonConvert.SerializeObject(codesToAdd.Codes);
                model.Errors = "[";
                _context.BulkCodeAudits.Add(model);
                _context.SaveChanges();
                message = $"{model.BulkCodeAuditId}";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                message = $"Error: {ex.Message}";
            }

            return message;
        }
        public async Task<string> AddBulkCodesAuditAsync(CodesViewModel codesToAdd)
        {
            string message = "";

            try
            {
                BulkCodeAuditModel model = new BulkCodeAuditModel();
                model.Issuer = codesToAdd.Issuer;
                model.TotalSent = codesToAdd.Codes.Count();
                model.CreationDate = DateTime.Now;
                model.OrigionalFileSent = JsonConvert.SerializeObject(codesToAdd.Codes);
                model.Errors = "[";
                _context.BulkCodeAudits.Add(model);
                await _context.SaveChangesAsync();
                message = $"{model.BulkCodeAuditId}";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                message = $"Error: {ex.Message}";
            }

            return message;
        }
        public string UpdateBulkCodesAudit(int bulkCodeAuditId, int numberProcessed, int numberSucceeded, int numberFailed, List<KeyValuePair<string, string>> errors)
        {
            string message = "";

            try
            {
                BulkCodeAuditModel model = _context.BulkCodeAudits.FirstOrDefault(x => x.BulkCodeAuditId == bulkCodeAuditId);

                if(model != null)
                {
                    model.TotalProcessed += numberProcessed;
                    model.TotalSucceeded += numberSucceeded;
                    model.TotalFailed += numberFailed;

                    string errorsString = "";

                    foreach(KeyValuePair<string, string> error in errors)
                    {
                        if (errorsString.Length > 0)
                            errorsString += ",";

                        errorsString += $"{{\"Code\":\"{error.Key}\",\"Error\":\"{error.Value}\"}}";
                    }

                    model.Errors += errorsString;

                    _context.SaveChanges();

                    message = "Success";
                }
                else
                {
                    message = $"Error: Code identifier not found. ({bulkCodeAuditId})";
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                message = $"Error: {ex.Message}";
            }

            return message;
        }
        public async Task<string> UpdateBulkCodesAuditAsync(int bulkCodeAuditId, int numberProcessed, int numberSucceeded, int numberFailed, List<KeyValuePair<string, string>> errors)
        {
            string message = "";

            try
            {
                BulkCodeAuditModel model = _context.BulkCodeAudits.FirstOrDefault(x => x.BulkCodeAuditId == bulkCodeAuditId);

                if (model != null)
                {
                    model.TotalProcessed += numberProcessed;
                    model.TotalSucceeded += numberSucceeded;
                    model.TotalFailed += numberFailed;

                    string errorsString = "";

                    foreach (KeyValuePair<string, string> error in errors)
                    {
                        if (errorsString.Length > 0)
                            errorsString += ",";

                        errorsString += $"{{\"Code\":\"{error.Key}\",\"Error\":\"{error.Value}\"}}";
                    }

                    model.Errors += errorsString;

                    await _context.SaveChangesAsync();

                    message = "Success";
                }
                else
                {
                    message = $"Error: Code identifier not found. ({bulkCodeAuditId})";
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                message = $"Error: {ex.Message}";
            }

            return message;
        }
        public string FinishBulkCodesAudit(int bulkCodeAuditId)
        {
            string message = "";

            try
            {
                BulkCodeAuditModel model = _context.BulkCodeAudits.FirstOrDefault(x => x.BulkCodeAuditId == bulkCodeAuditId);

                if (model != null)
                {
                    model.FinishDate = DateTime.Now;
                    model.Errors += "]";
                    _context.SaveChanges();

                    message = "Success";
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                message = $"Error: {ex.Message}";
            }

            return message;
        }
        public async Task<string> FinishBulkCodesAuditAsync(int bulkCodeAuditId)
        {
            string message = "";

            try
            {
                BulkCodeAuditModel model = _context.BulkCodeAudits.FirstOrDefault(x => x.BulkCodeAuditId == bulkCodeAuditId);

                if (model != null)
                {
                    model.FinishDate = DateTime.Now;
                    model.Errors += "]";
                    await _context.SaveChangesAsync();

                    message = "Success";
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                message = $"Error: {ex.Message}";
            }

            return message;
        }
        public CodeLookupViewModel LookupCodeOrRSIId(CodeLookupViewModel model)
        {
            try
            {
                CodeActivityModel tmp = new CodeActivityModel();

                if (model.RSIId > 0)
                {
                    tmp = _context.CodeActivities.FirstOrDefault(x => x.RSIId == model.RSIId);

                    if (tmp != null)
                    {
                        model.RSIId = tmp.RSIId;
                        model.EmailVerificationDate = tmp.EmailVerifiedDate;
                        if (model.EmailVerificationDate == null)
                            model.Status = "Email not verified yet.";
                        else if (model.EmailVerificationDate != null && model.RSIId < 1)
                            model.Status = "There was an error activating the card.  Please call IT";
                        else
                            model.Status = "Email was successfully verified";
                    }
                }
                else if(model.Code != null && model.Code.Length > 0)
                {
                    tmp = _context.CodeActivities.FirstOrDefault(x => x.ActivationCode.ToUpper() == model.Code.ToUpper());

                    if (tmp != null)
                    {
                        model.RSIId = tmp.RSIId;
                        model.EmailVerificationDate = tmp.EmailVerifiedDate;
                        if (model.EmailVerificationDate == null)
                            model.Status = "Email not verified yet.";
                        else if (model.EmailVerificationDate != null && model.RSIId < 1)
                            model.Status = "There was an error activating the card.  Please call IT";
                        else
                            model.Status = "Email was successfully verified";
                    }

                }
                else
                {
                    model.Message = "Error: Missing code or rsiId to lookup";
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                model.Message = $"Error: {ex.Message}";
            }

            return model;
        }
        public async Task<CodeLookupViewModel> LookupCodeOrRSIIdAsync(CodeLookupViewModel model)
        {
            try
            {
                CodeActivityModel tmp = new CodeActivityModel();

                if (model.RSIId > 0)
                {
                    tmp = _context.CodeActivities.FirstOrDefault(x => x.RSIId == model.RSIId);

                    if (tmp != null)
                    {
                        model.Message = "Success";
                        model.RSIId = tmp.RSIId;
                        model.Code = tmp.ActivationCode;
                        model.EmailVerificationDate = tmp.EmailVerifiedDate;
                        if (model.EmailVerificationDate == null)
                            model.Status = "Email not verified yet.";
                        else if (model.EmailVerificationDate != null && model.RSIId < 1)
                            model.Status = "There was an error activating the card.  Please call IT";
                        else
                            model.Status = "Email was successfully verified";
                    }
                }
                else if (model.Code != null && model.Code.Length > 0)
                {
                    tmp = _context.CodeActivities.FirstOrDefault(x => x.ActivationCode.ToUpper() == model.Code.ToUpper());

                    if (tmp != null)
                    {
                        model.Message = "Success";
                        model.RSIId = tmp.RSIId;
                        model.Code = tmp.ActivationCode;
                        model.EmailVerificationDate = tmp.EmailVerifiedDate;
                        if (model.EmailVerificationDate == null)
                            model.Status = "Email not verified yet.";
                        else if (model.EmailVerificationDate != null && model.RSIId < 1)
                            model.Status = "There was an error activating the card.  Please call IT";
                        else
                            model.Status = "Email was successfully verified";
                    }

                }
                else
                {
                    model.Message = "Error: Missing code or rsiId to lookup";
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                model.Message = $"Error: {ex.Message}";
            }

            return await Task.FromResult(model);
        }
        public async Task<CodeLookupViewModel> LookupRSIIdByCode(string code, int issuer)
        {
            CodeLookupViewModel model = new CodeLookupViewModel();
            CodeActivityModel tmp = new CodeActivityModel();

            try
            {
                tmp = await _context.CodeActivities.FirstOrDefaultAsync(x => x.ActivationCode.ToUpper() == code.ToUpper() && x.Code.Issuer == issuer.ToString());

                if (tmp != null)
                {
                    model.Message = "Success";
                    model.RSIId = tmp.RSIId;
                    model.Code = tmp.ActivationCode;
                    model.EmailVerificationDate = tmp.EmailVerifiedDate;
                    if (model.EmailVerificationDate == null)
                        model.Status = "Email not verified yet.";
                    else if (model.EmailVerificationDate != null && model.RSIId < 1)
                        model.Status = "There was an error activating the card.  Please call IT";
                    else
                        model.Status = "Email was successfully verified";
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                model.Message = $"Error: {ex.Message}";
            }

            return model;
        }
    }
}
