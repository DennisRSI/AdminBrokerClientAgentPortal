using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClientPortal.Models;
using ClientPortal.Services._Interfaces;
using Codes.Service.Interfaces;
using Codes.Service.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ClientPortal.Controllers.APIs
{
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private readonly IUserService _context;
        private readonly ICodeService _codeService;
        private readonly UserManager<ApplicationUser> _userManager;

        public UserController(IUserService context, ICodeService codeService, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _codeService = codeService;
            _userManager = userManager;
        }

        // GET: api/<controller>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        [HttpPost("{userType}")]
        public async Task<JObject> Post(string userType, [FromBody]dynamic obj)
        {
            dynamic returnObj = new JObject();
            string companyName = "", ein = "", firstName = "", lastName = "", address = "", city = "", state = "", postalCode = "", country = "";
            string mobilePhone = "", workPhone = "", fax = "", email = "";

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
                    state = stateTmp.ToString();

                var postalCodeTmp = obj.postal_code;
                if (postalCodeTmp != null)
                    postalCode = postalCodeTmp.ToString();

                var countryTmp = obj.country;
                if (countryTmp != null)
                    country = countryTmp.ToString();

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
                            State = state
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
                            brokerCommissionPercentage = brokerCommissionPercentageTmp.ToString();

                        var agentCommissionPercentageTmp = obj.agent_commission_percentage;
                        if (agentCommissionPercentageTmp != null)
                            agentCommissionPercentage = agentCommissionPercentageTmp.ToString();

                        var clientCommissionPercentageTmp = obj.client_commission_percentage;
                        if (clientCommissionPercentageTmp != null)
                            clientCommissionPercentage = clientCommissionPercentageTmp.ToString();

                        var cards1000Tmp = obj.cards_1000;
                        if (cards1000Tmp != null)
                            cards1000 = cards1000Tmp.ToString();

                        var cards5000Tmp = obj.cards_5000;
                        if (cards5000Tmp != null)
                            cards5000 = cards5000Tmp.ToString();

                        var cards10000Tmp = obj.cards_10000;
                        if (cards10000Tmp != null)
                            cards10000 = cards10000Tmp.ToString();

                        var cards25000Tmp = obj.cards_25000;
                        if (cards25000Tmp != null)
                            cards25000 = cards25000Tmp.ToString();

                        var cards50000Tmp = obj.cards_50000;
                        if (cards50000Tmp != null)
                            cards50000 = cards50000Tmp.ToString();

                        var cards100000Tmp = obj.cards_100000;
                        if (cards100000Tmp != null)
                            cards100000 = cards100000Tmp.ToString();

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
                            ParentBrokerId = 0
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
                                    ClientId = 0
                                };

                                clientModel.Broker.BrokerId = brokerId;

                                ClientViewModel clientReturn = await _context.ClientAdd(clientModel, "Chang3M3#");
                                returnObj.client_id = clientReturn.ClientId;
                                returnObj.account_id = clientReturn.ApplicationReference;
                                returnObj.broker_id = clientReturn.Broker.BrokerId;
                                returnObj.is_success = clientReturn.IsSuccess;
                                returnObj.message = clientReturn.Message;
                            }
                            else
                            {
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
                                    AgentId = 0
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

        // PUT api/<controller>/5
        [HttpPut("{userType}")]
        public async Task<JObject> Put(string userType, [FromBody]dynamic obj)
        {
            dynamic returnObj = new JObject();
            string companyName = "NA", ein = "NA", firstName = "NA", lastName = "NA", address = "NA", city = "NA", state = "NA", postalCode = "NA", country = "NA";
            string mobilePhone = "NA", workPhone = "NA", fax = "NA", email = "NA", accountId = "NA";

            try
            {
                var accountIdTmp = obj.account_id;
                if (accountIdTmp != null && accountIdTmp.ToString().Length)
                {
                    accountId = accountIdTmp.ToString();

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
                        state = stateTmp.ToString();

                    var postalCodeTmp = obj.postal_code;
                    if (postalCodeTmp != null)
                        postalCode = postalCodeTmp.ToString();

                    var countryTmp = obj.country;
                    if (countryTmp != null)
                        country = countryTmp.ToString();

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

                    switch (userType)
                    {
                        case "Administrator":
                        case "Super Administrator":
                            ApplicationUser adminModel = await _userManager.FindByIdAsync(accountId);

                            if (companyName != "NA")
                                adminModel.CompanyName = companyName;
                            if (ein != "NA")
                                adminModel.EIN = ein;
                            if (firstName != "NA")
                                adminModel.FirstName = firstName;
                            if (lastName != "NA")
                                adminModel.LastName = lastName;
                            if (address != "NA")
                                adminModel.Address = address;
                            if (city != "NA")
                                adminModel.City = city;
                            if (state != "NA")
                                adminModel.State = state;
                            if (postalCode != "NA")
                                adminModel.PostalCode = postalCode;
                            if (country != "NA")
                                adminModel.Country = country;
                            if (mobilePhone != "NA")
                                adminModel.MobilePhone = mobilePhone;
                            if (workPhone != "NA")
                                adminModel.OfficePhone = workPhone;
                            if (fax != "NA")
                                adminModel.Fax = fax;
                            if (email != "NA")
                            {
                                adminModel.Email = email;
                                adminModel.UserName = email;
                            }

                            IdentityResult adminUpdate = await _userManager.UpdateAsync(adminModel);

                            returnObj.is_success = adminUpdate.Succeeded;

                            if (!adminUpdate.Succeeded)
                            {
                                string errors = "";

                                foreach (var error in adminUpdate.Errors)
                                {
                                    if (errors.Length > 0)
                                        errors += " | ";

                                    errors += error.Description;
                                }

                                returnObj.message = errors;
                            }
                            else
                            {
                                returnObj.accountId = accountId;
                                returnObj.message = "Success";
                            }
                            break;
                        case "Broker":
                            int brokerId = 0;
                            var brokerIdTmp = obj.broker_id;
                            if (brokerIdTmp != null && int.TryParse(brokerIdTmp.ToString(), out brokerId))
                            {
                                BrokerViewModel broker = await _codeService.GetBrokerById(brokerId);
                            }
                            else
                            {
                                returnObj.is_success = false;
                                returnObj.message = "broker_id is required";
                            }
                            break;
                        case "Client":
                            int clientId = 0;
                            var clientIdTmp = obj.client_id;
                            if (clientIdTmp != null && int.TryParse(clientIdTmp.ToString(), out clientId))
                            {
                                ClientViewModel client = await _codeService.GetClientById(clientId);
                            }
                            else
                            {
                                returnObj.is_success = false;
                                returnObj.message = "client_id is required";
                            }
                                break;
                        case "Agent":
                            int agentId = 0;
                            var agentIdTmp = obj.agent_id;
                            if (agentIdTmp != null && int.TryParse(agentIdTmp.ToString(), out agentId))
                            {
                                AgentViewModel agent = await _codeService.GetAgentById(agentId);
                            }
                            else
                            {
                                returnObj.is_success = false;
                                returnObj.message = "agent_id is required";
                            }
                            break;
                    }
                }
                else
                {
                    returnObj.is_success = false;
                    returnObj.message = "account_id is required";
                }

                
            }
            catch (Exception ex)
            {
                returnObj.is_success = false;
                returnObj.message = ex.Message;
            }

            return returnObj;
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
