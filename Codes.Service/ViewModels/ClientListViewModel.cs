using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Codes1.Service.ViewModels
{
    public class ClientListViewModel : _BaseListViewModel
    {
        private float _commissionRate = 0;

        public ClientListViewModel()
        {

        }

        public ClientListViewModel(int clientId, string accountId, string firstName, string middleName, string lastName, string phone, string extension
            , string email, string company, int cardQuantity, DateTime activationDate, DateTime? deactivationDate, float commissionRate)
            : base(accountId,firstName, middleName, lastName, phone, extension, email, company, activationDate, deactivationDate)
        {
          
            ClientId = clientId;
            CardQuantity = cardQuantity;
            CommissionRate = commissionRate;
        }
        [JsonProperty(PropertyName = "client_id")]
        public int ClientId { get; set; }
        
        [JsonProperty(PropertyName = "card_quantity")]
        public int CardQuantity { get; set; } = 0;
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
