using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Codes.Service.ViewModels
{
    public class BrokerListViewModel : _BaseListViewModel
    {
        private float _commissionRate = 0;

        public BrokerListViewModel()
        {

        }

        public BrokerListViewModel(int brokerId, string accountId, string firstName, string middleName, string lastName, string phone, string extension
            , string email, string company, float commissionRate, DateTime activationDate, DateTime? deactivationDate) 
            : base(accountId, firstName, middleName, lastName, phone, extension, email, company, activationDate, deactivationDate)
            
        {
            BrokerId = brokerId;
            CommissionRate = commissionRate;
        }

        [JsonProperty(PropertyName = "broker_id")]
        public int BrokerId { get; set; }
        
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

    }
}
