﻿using System;
using System.IO;
using System.Threading.Tasks;
using ClientPortal.Models;
using ClientPortal.Services._Interfaces;
using Codes1.Service.Interfaces;
using Codes1.Service.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Codes1.Service.Models;
using ClientPortal.Models._ViewModels;
using ClientPortal.ViewComponents;

namespace ClientPortal.Controllers.APIs
{
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private readonly IUserService _context;
        private readonly ICode1Service _codeService;
        private readonly IDocument1Service _documentService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IAccount1Service _accountService;

        public UserController(IUserService context, ICode1Service codeService, IDocument1Service documentService, UserManager<ApplicationUser> userManager,
            IAccount1Service accountService)
        {
            _context = context;
            _codeService = codeService;
            _userManager = userManager;
            _documentService = documentService;
            _accountService = accountService;
        }

        [HttpGet("{UserType}/{BrokerId}/{ClientId?}")]
        public IActionResult Get(string userType, int brokerId, int clientId = 0)
        {
            return ViewComponent(typeof(ListViewComponent), new { userType = userType, brokerId = brokerId, clientId = clientId });
        }

        [HttpPost("{userType}")]
        public async Task<JObject> Post(string userType, [FromBody]dynamic obj)
        {
            dynamic returnObj = new JObject();
            string companyName = "", ein = "", firstName = "", lastName = "", address = "", city = "", state = "", postalCode = "", country = "";
            string mobilePhone = "", workPhone = "", fax = "", email = "", username = "";

            try
            {
                var companyNameTmp = obj.company_name;
                if (companyNameTmp != null)
                    companyName = companyNameTmp.ToString();

                var einTmp = obj.ein;
                if (einTmp != null)
                    ein = einTmp.ToString();

                var firstNameTmp = obj.first_name;
                if (firstNameTmp != null)
                    firstName = firstNameTmp.ToString();

                var lastNameTmp = obj.last_name;
                if (lastNameTmp != null)
                    lastName = lastNameTmp.ToString();

                var addressTmp = obj.address;
                if (addressTmp != null)
                    address = addressTmp.ToString();

                var cityTmp = obj.city;
                if (cityTmp != null)
                    city = cityTmp.ToString();

                var stateTmp = obj.state;
                if (stateTmp != null)
                {
                    if (!stateTmp.ToString().Contains("Select"))
                    {
                        state = stateTmp.ToString();
                    }
                }

                var postalCodeTmp = obj.postal_code;
                if (postalCodeTmp != null)
                    postalCode = postalCodeTmp.ToString();

                var countryTmp = obj.country;
                if (countryTmp != null)
                {
                    if (!countryTmp.ToString().Contains("Select"))
                    {
                        country = countryTmp.ToString();
                    }
                }

                var mobilePhoneTmp = obj.mobile_phone;
                if (mobilePhoneTmp != null)
                    mobilePhone = mobilePhoneTmp.ToString();

                var workPhoneTmp = obj.work_phone;
                if (workPhoneTmp != null)
                    workPhone = workPhoneTmp.ToString();

                var faxTmp = obj.fax;
                if (faxTmp != null)
                    fax = faxTmp.ToString();

                var emailTmp = obj.email;
                if (emailTmp != null)
                    email = emailTmp.ToString();

                var usernameTmp = obj.username;
                if (usernameTmp != null)
                    username = usernameTmp.ToString();

                switch (userType)
                {
                    case "Administrator":
                    case "Super Administrator":
                        AdminViewModel adminModel = new AdminViewModel()
                        {
                            Address = address,
                            ApplicationReference = "",
                            City = city,
                            CompanyName = companyName,
                            Country = country,
                            CreationDate = DateTime.Now,
                            CreatorIP = "127.0.0.1",
                            DeactivationDate = null,
                            DeactivationReason = "",
                            EIN = ein,
                            Email = email,
                            Fax = fax,
                            FaxExtension = "",
                            FirstName = firstName,
                            IsActive = true,
                            LastName = lastName,
                            Message = "Success",
                            MiddleName = "",
                            MobilePhone = mobilePhone,
                            OfficeExtension = "",
                            OfficePhone = workPhone,
                            PostalCode = postalCode,
                            State = state,
                            Username = username
                        };

                        AdminViewModel adminReturn = new AdminViewModel();

                        if (userType == "Super Administrator")
                        {
                            adminReturn = await _context.SuperAdminAdd(adminModel, "Chang3M3#");
                        }
                        else
                        {
                            adminReturn = await _context.AdminAdd(adminModel, "Chang3M3#");
                        }

                        returnObj.account_id = adminReturn.ApplicationReference;
                        returnObj.is_success = adminReturn.IsSuccess;
                        returnObj.message = adminReturn.Message;

                        break;
                    case "Broker":
                        float brokerCommissionPercentage = 0, agentCommissionPercentage = 0, clientCommissionPercentage = 0;
                        float cards1000 = 0, cards5000 = 0, cards10000 = 0, cards25000 = 0, cards50000 = 0, cards100000 = 0;
                        int virtualCap = 0, timeframe = 0;

                        var brokerCommissionPercentageTmp = obj.broker_commission_percentage;
                        if (brokerCommissionPercentageTmp != null)
                            brokerCommissionPercentage = Convert.ToSingle(brokerCommissionPercentageTmp.ToString());

                        var agentCommissionPercentageTmp = obj.agent_commission_percentage;
                        if (agentCommissionPercentageTmp != null)
                            agentCommissionPercentage = Convert.ToSingle(agentCommissionPercentageTmp.ToString());

                        var clientCommissionPercentageTmp = obj.client_commission_percentage;
                        if (clientCommissionPercentageTmp != null)
                            clientCommissionPercentage = Convert.ToSingle(clientCommissionPercentageTmp.ToString());

                        var cards1000Tmp = obj.cards_1000;
                        if (cards1000Tmp != null)
                            cards1000 = Convert.ToSingle(cards1000Tmp.ToString());

                        var cards5000Tmp = obj.cards_5000;
                        if (cards5000Tmp != null)
                            cards5000 = Convert.ToSingle(cards5000Tmp.ToString());

                        var cards10000Tmp = obj.cards_10000;
                        if (cards10000Tmp != null)
                            cards10000 = Convert.ToSingle(cards10000Tmp.ToString());

                        var cards25000Tmp = obj.cards_25000;
                        if (cards25000Tmp != null)
                            cards25000 = Convert.ToSingle(cards25000Tmp.ToString());

                        var cards50000Tmp = obj.cards_50000;
                        if (cards50000Tmp != null)
                            cards50000 = Convert.ToSingle(cards50000Tmp.ToString());

                        var cards100000Tmp = obj.cards_100000;
                        if (cards100000Tmp != null)
                            cards100000 = Convert.ToSingle(cards100000Tmp.ToString());

                        var virtualCapTmp = obj.virtual_cap;
                        if (virtualCapTmp != null && virtualCapTmp.ToString().Length > 0)
                            int.TryParse(virtualCapTmp.ToString(), out virtualCap);

                        var timeframeTmp = obj.timeframe;
                        if (timeframeTmp != null && timeframeTmp.ToString().Length > 0)
                            int.TryParse(timeframeTmp.ToString(), out timeframe);

                        BrokerViewModel brokerModel = new BrokerViewModel()
                        {
                            Address = address,
                            ApplicationReference = "",
                            City = city,
                            CompanyName = companyName,
                            Country = country,
                            CreationDate = DateTime.Now,
                            CreatorIP = "127.0.0.1",
                            DeactivationDate = null,
                            DeactivationReason = "",
                            EIN = ein,
                            Email = email,
                            Fax = fax,
                            FaxExtension = "",
                            BrokerFirstName = firstName,
                            IsActive = true,
                            BrokerLastName = lastName,
                            Message = "Success",
                            BrokerMiddleName = "",
                            MobilePhone = mobilePhone,
                            OfficeExtension = "",
                            OfficePhone = workPhone,
                            PostalCode = postalCode,
                            State = state,
                            AgentCommissionPercentage = agentCommissionPercentage,
                            BrokerCommissionPercentage = brokerCommissionPercentage,
                            BrokerId = 0,
                            ClientCommissionPercentage = clientCommissionPercentage,
                            PhysicalCardsPercentOfFaceValue1000 = cards1000,
                            PhysicalCardsPercentOfFaceValue10000 = cards10000,
                            PhysicalCardsPercentOfFaceValue100000 = cards100000,
                            PhysicalCardsPercentOfFaceValue25000 = cards25000,
                            PhysicalCardsPercentOfFaceValue5000 = cards5000,
                            PhysicalCardsPercentOfFaceValue50000 = cards50000,
                            TimeframeBetweenCapInHours = timeframe,
                            VirtualCardCap = virtualCap,
                            ParentBrokerId = 0,
                            Username = username
                        };

                        BrokerViewModel brokerReturn = await _context.BrokerAdd(brokerModel, "Chang3M3#");
                        returnObj.account_id = brokerReturn.ApplicationReference;
                        returnObj.broker_id = brokerReturn.BrokerId;
                        returnObj.is_success = brokerReturn.IsSuccess;
                        returnObj.message = brokerReturn.Message;
                        break;

                    case "Client":
                    case "Agent":
                        int brokerId = 0;
                        float commissionRate = 0;
                        var brokerIdTmp = obj.broker_id;
                        if(brokerIdTmp != null && int.TryParse(brokerIdTmp.ToString(), out brokerId))
                        {
                            var commissionRateTmp = obj.commission_rate;
                            if (commissionRateTmp != null)
                                float.TryParse(commissionRateTmp.ToString(), out commissionRate);

                            if (commissionRate < 1)
                            {
                                BrokerViewModel broker = await _codeService.GetBrokerById(brokerId);

                                if(broker != null)
                                {
                                    if (userType == "Client")
                                        commissionRate = broker.ClientCommissionPercentage;
                                    else
                                        commissionRate = broker.AgentCommissionPercentage;
                                }
                            }


                            if (userType == "Client")
                            {
                                ClientViewModel clientModel = new ClientViewModel()
                                {
                                    Address = address,
                                    ApplicationReference = "",
                                    City = city,
                                    CompanyName = companyName,
                                    Country = country,
                                    CreationDate = DateTime.Now,
                                    CreatorIP = "127.0.0.1",
                                    DeactivationDate = null,
                                    DeactivationReason = "",
                                    EIN = ein,
                                    Email = email,
                                    Fax = fax,
                                    FaxExtension = "",
                                    ContactFirstName = firstName,
                                    IsActive = true,
                                    ContactLastName = lastName,
                                    Message = "Success",
                                    ContactMiddleName = "",
                                    MobilePhone = mobilePhone,
                                    OfficeExtension = "",
                                    OfficePhone = workPhone,
                                    PostalCode = postalCode,
                                    State = state,
                                    CommissionRate = commissionRate,
                                    ClientId = 0,
                                    Username = username
                                };

                                clientModel.Broker.BrokerId = brokerId;

                                ClientViewModel clientReturn = await _context.ClientAdd(clientModel, "Travel@2019");
                                returnObj.client_id = clientReturn.ClientId;
                                returnObj.account_id = clientReturn.ApplicationReference;
                                returnObj.broker_id = clientReturn.Broker.BrokerId;
                                returnObj.is_success = clientReturn.IsSuccess;
                                returnObj.message = clientReturn.Message;
                            }
                            else
                            {
                                int parentAgentId = 0;
                                var parentAgentIdField = obj.parentAgentId;

                                if (parentAgentIdField != null)
                                {
                                    string val = parentAgentIdField.ToString();

                                    if (!val.Contains("Select"))
                                    {
                                        parentAgentId = Convert.ToInt32(val);
                                    }
                                }

                                AgentViewModel agentModel = new AgentViewModel()
                                {
                                    Address = address,
                                    ApplicationReference = "",
                                    City = city,
                                    CompanyName = companyName,
                                    Country = country,
                                    CreationDate = DateTime.Now,
                                    CreatorIP = "127.0.0.1",
                                    DeactivationDate = null,
                                    DeactivationReason = "",
                                    EIN = ein,
                                    Email = email,
                                    Fax = fax,
                                    FaxExtension = "",
                                    AgentFirstName = firstName,
                                    IsActive = true,
                                    AgentLastName = lastName,
                                    Message = "Success",
                                    AgentMiddleName = "",
                                    MobilePhone = mobilePhone,
                                    OfficeExtension = "",
                                    OfficePhone = workPhone,
                                    PostalCode = postalCode,
                                    State = state,
                                    CommissionRate = commissionRate,
                                    AgentId = 0,
                                    ParentAgentId = parentAgentId,
                                    Username = username
                                };

                                agentModel.Broker.BrokerId = brokerId;

                                AgentViewModel agentReturn = await _context.AgentAdd(agentModel, "Chang3M3#");
                                returnObj.agent_id = agentReturn.AgentId;
                                returnObj.account_id = agentReturn.ApplicationReference;
                                returnObj.broker_id = agentReturn.Broker.BrokerId;
                                returnObj.is_success = agentReturn.IsSuccess;
                                returnObj.message = agentReturn.Message;
                            }
                        }
                        else
                        {
                            returnObj.is_success = false;
                            returnObj.message = "broker_id is required";
                        }

                        break;

                }

            }
            catch (Exception ex)
            {
                returnObj.is_success = false;
                returnObj.message = ex.Message;
            }

            return returnObj;
        }

        [HttpPost("uploadfile/{fileType}/{role}/{id}")]
        public ActionResult UploadFile(string fileType, string role, int id, IFormFile file)
        {
            var documentType = DocumentType.Unknown;

            if (fileType.ToLower() == "w9")
            {
                documentType = DocumentType.W9;
            }

            using (var memoryStream = new MemoryStream())
            {
                file.CopyTo(memoryStream);
                var bytes = memoryStream.ToArray();

                _documentService.Add(role, id, documentType, file.ContentType, bytes);
            }

            return Ok();
        }

        [HttpPost("changepassword/{id}/{password}")]
        public async Task<IActionResult> ChangePassword(string id, string password)
        {
            var result = await _context.ChangePassword(id, password);

            return Ok(result);
        }

        [HttpPost("changepasswordclient/{id}/{password}")]
        public async Task<IActionResult> ChangePasswordClient(string id, string password)
        {
            var result = await _context.ChangePassword(id, password);
            return Ok(result);
        }

        private async Task<string> ProcessUserModel(string id, ApplicationUser model)
        {
            model.Id = id;

            if (model.Country.StartsWith("Select"))
            {
                model.Country = String.Empty;
            }

            if (model.State.StartsWith("Select"))
            {
                model.State = String.Empty;
            }

            var user = await _userManager.FindByIdAsync(id);
            model.BrokerId = user.BrokerId;
            model.ClientId = user.ClientId;
            model.AgentId = user.AgentId;

            if (String.IsNullOrEmpty(model.UserName))
            {
                model.UserName = model.Email;
            }

            return user.Role;
        }

        [HttpPost("clientupdateprofile/{id}")]
        public async Task<IActionResult> ClientUpdateProfile(string id, [FromBody] ClientEditPostViewModel model)
        {
            await ProcessUserModel(id, model);
            var clientResult = await UpdateClient(model);
            return Ok(clientResult);
        }

        [HttpPost("updateprofile/{id}")]
        public async Task<IActionResult> UpdateProfile(string id, [FromBody] ProfileViewModel model)
        {
            var role = await ProcessUserModel(id, model);

            switch (role)
            {
                case "Administrator":
                    var adminResult = await UpdateAdmin(model);
                    return Ok(adminResult);

                case "Agent":
                    if (model.ParentAgentId.Value == 0)
                    {
                        model.ParentAgentId = null;
                    }

                    var agentResult = await UpdateAgent(model);
                    return Ok(agentResult);

                case "Broker":
                    var brokerResult = await UpdateBroker(model);
                    return Ok(brokerResult);
            }

            return BadRequest();
        }

        [HttpPost("deactivateclient/{clientId}/{reason}")]
        public IActionResult DeactivateClient(int clientId, string reason)
        {
            _accountService.DeactivateClient(clientId, reason);
            return Ok();
        }

        [HttpPost("deactivateagent/{agentId}/{reason}")]
        public IActionResult DeactivateAgent(int agentId, string reason)
        {
            _accountService.DeactivateAgent(agentId, reason);
            return Ok();
        }

        private async Task<AdminViewModel> UpdateAdmin(ApplicationUser user)
        {
            var admin = new AdminViewModel()
            {
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                CompanyName = user.CompanyName,
                EIN = user.EIN,
                Address = user.Address,
                City = user.City,
                State = user.State,
                PostalCode = user.PostalCode,
                Country = user.Country,
                MobilePhone = user.MobilePhone,
                OfficePhone = user.OfficePhone,
                Fax = user.Fax,
                Username = user.UserName
            };

            return await _context.AdminUpdate(admin);
        }

        private async Task<AgentViewModel> UpdateAgent(ProfileViewModel user)
        {

            AgentViewModel agent = new AgentViewModel();

            try
            {
                AgentViewModel usr = await _codeService.GetAgentByAccountId(user.Id);

                if (usr != null)
                {
                    agent = new AgentViewModel()
                    {
                        AgentId = user.AgentId,
                        BrokerId = usr.BrokerId,
                        ApplicationReference = user.Id,
                        Email = user.Email,
                        AgentFirstName = user.FirstName,
                        AgentLastName = user.LastName,
                        CompanyName = user.CompanyName,
                        EIN = user.EIN,
                        Address = user.Address,
                        City = user.City,
                        State = user.State,
                        PostalCode = user.PostalCode,
                        Country = user.Country,
                        MobilePhone = user.MobilePhone,
                        OfficePhone = user.OfficePhone,
                        Fax = user.Fax,
                        ParentAgentId = user.ParentAgentId,
                        CommissionRate = (float)user.CommissionRate,
                        Username = user.UserName
                    };

                    return await _context.AgentUpdate(agent);
                }
                else
                {
                    agent.Message = "Error: Agent not found";
                }
            }
            catch (Exception ex)
            {
                if (agent == null)
                    agent = new AgentViewModel();

                agent.Message = $"Error: {ex.Message}";
            }

            return agent;
        }

        private async Task<BrokerViewModel> UpdateBroker(ProfileViewModel user)
        {
            var broker = new BrokerViewModel()
            {
                ApplicationReference = user.Id,
                BrokerId = user.BrokerId,
                Email = user.Email,
                BrokerFirstName = user.FirstName,
                BrokerLastName = user.LastName,
                CompanyName = user.CompanyName,
                EIN = user.EIN,
                Address = user.Address,
                City = user.City,
                State = user.State,
                PostalCode = user.PostalCode,
                Country = user.Country,
                MobilePhone = user.MobilePhone,
                OfficePhone = user.OfficePhone,
                Fax = user.Fax,
                Username = user.UserName,
                BrokerCommissionPercentage = (float)user.CommissionRate
            };

            return await _context.BrokerUpdate(broker);
        }

        private async Task<ClientViewModel> UpdateClient(ClientEditPostViewModel user)
        {
            var client = new ClientViewModel()
            {
                ApplicationReference = user.Id,
                ClientId = user.ClientId,
                Email = user.Email,
                ContactFirstName = user.FirstName,
                ContactLastName = user.LastName,
                CompanyName = user.CompanyName,
                EIN = user.EIN,
                Address = user.Address,
                City = user.City,
                State = user.State,
                PostalCode = user.PostalCode,
                Country = user.Country,
                MobilePhone = user.MobilePhone,
                OfficePhone = user.OfficePhone,
                Fax = user.Fax,
                AgentId = user.AssignedAgent == 0 ? (int?)null : user.AssignedAgent,
                CommissionRate = user.CommissionRate,
                Username = user.UserName
            };

            return await _context.ClientUpdate(client);
        }
        
    }
}
