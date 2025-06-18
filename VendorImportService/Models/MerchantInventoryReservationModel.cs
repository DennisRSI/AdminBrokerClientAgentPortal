using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace VendorImport.Service.Models
{
    [Table("MerchantInventoryReservations")]
    public class MerchantInventoryReservationModel : _SheetBase2Model
    {
        [JsonProperty(PropertyName = "grossSaleWithTax"), Column(TypeName = "decimal(18,2)")]
        public decimal GrossSaleWithTax { get; set; } = 0;
        [JsonProperty(PropertyName = "costOfHotelWithTax"), Column(TypeName = "decimal(18,2)")]
        public decimal CostOfHotelWithTax { get; set; } = 0;
        [JsonProperty(PropertyName = "cardProcessingFees"), Column(TypeName = "decimal(18,2)")]
        public decimal CardProcessingFees { get; set; } = 0;
        [JsonProperty(PropertyName = "netProfit"), Column(TypeName = "decimal(18,2)")]
        public decimal NetProfit { get; set; } = 0;
        [JsonProperty(PropertyName = "affilitecommissionpercentage"), Column(TypeName = "decimal(18,2)")]
        public decimal AffiliteCommissionPercentage { get; set; } = 0;
        [JsonProperty(PropertyName = "arnTransactionFee"), Column(TypeName = "decimal(18,2)")]
        public decimal ARNTransactionFee { get; set; } = 0;
    }
}
