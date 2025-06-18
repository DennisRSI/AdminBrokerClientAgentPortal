using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Codes1.Service.ViewModels
{
    public class AgentListViewModel : _BaseListViewModel
    {
        private float _commissionRate = 0;

        public AgentListViewModel()
        {

        }

        public AgentListViewModel(int agentId, string accountId, string firstName, string middleName, string lastName, string phone, string extension
            , string email, string company, int clients, float commissionRate, DateTime activationDate, DateTime? deactivationDate)
            :base(accountId, firstName, middleName, lastName, phone, extension, email, company, activationDate, deactivationDate)
        {
            AgentId = agentId;
            Clients = clients;
            CommissionRate = commissionRate;
        }

        [JsonProperty(PropertyName = "agent_id")]
        public int AgentId { get; set; }
        
        [JsonProperty(PropertyName = "number_of_clients")]
        public int Clients { get; set; } = 0;
        [JsonProperty(PropertyName = "commission_rate")]
        public float CommissionRate
        {
            get
            {
                return _commissionRate;
            }
            set
            {
                _commissionRate = value;
            }
        }

        [JsonProperty(PropertyName = "commission_rate_percentage")]
        public string CommissionRatePercentageString
        {
            get
            {
                var comm = CommissionRate * 100;
                return $"{comm}%";
            }
        }

        public string PrimaryAgent { get; set; }
    }
}
