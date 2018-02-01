using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Codes.Service.ViewModels
{
    public class CodeListViewModel
    {

        public CodeListViewModel()
        {

        }

        public CodeListViewModel(int codeRangeId, DateTime creationDate, string cardType, float points, int numberOfActivations
            , string startAlpha, string endAlpha, int startNumber, int endNumber, int incrementBy, decimal chargeAmount)
        {
            CodeRangeId = codeRangeId;
            CreationDate = creationDate;
            CardType = cardType;
            Points = points;
            NumberOfActivations = numberOfActivations;
            ChargeAmount = chargeAmount;
            StartAlpha = startAlpha;
            EndAlpha = endAlpha;
            StartNumber = startNumber;
            EndNumber = endNumber;
            IncrementBy = incrementBy;
        }
        [JsonIgnore]
        public string StartAlpha { get; set; }
        [JsonIgnore]
        public string EndAlpha { get; set; }
        [JsonIgnore]
        public int StartNumber { get; set; } = 0;
        [JsonIgnore]
        public int EndNumber { get; set; } = 0;
        [JsonIgnore]
        public int IncrementBy { get; set; } = 1;
        [JsonProperty(PropertyName = "code_range_id")]
        public int CodeRangeId { get; set; }
        [JsonProperty(PropertyName = "creation_date")]
        public DateTime CreationDate { get; set; }
        [JsonProperty(PropertyName = "card_type")]
        public string CardType { get; set; } = "Virtual";
        [JsonProperty(PropertyName = "points_on_card")]
        public float Points { get; set; }
        [JsonProperty(PropertyName = "quantity")]
        public int Quantity
        {
            get
            {
                return ((EndNumber - StartNumber) / IncrementBy) + 1;
            }
        }
        [JsonProperty(PropertyName = "number_of_activations")]
        public int NumberOfActivations { get; set; }
        [JsonProperty(PropertyName = "start_code")]
        public string StartCode
        {
            get
            {
                return $"{StartAlpha}{StartNumber.ToString()}{EndAlpha}";
            }
        }
        [JsonProperty(PropertyName = "end_code")]
        public string EndCode
        {
            get
            {
                return $"{StartAlpha}{EndNumber.ToString()}{EndAlpha}";
            }
        }
        [JsonProperty(PropertyName = "charge_amount")]
        public decimal ChargeAmount { get; set; }
    }
}
