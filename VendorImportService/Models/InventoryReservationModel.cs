using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace VendorImport.Service.Models
{
    [Table("InventoryReservations")]
    public class InventoryReservationModel : _SheetBase2Model
    {
        [JsonProperty(PropertyName = "bookedRoomRevenue"), Column(TypeName = "decimal(18,2)")]
        public decimal BookedRoomRevenue { get; set; } = 0;
        [JsonProperty(PropertyName = "commissionReceived"), Column(TypeName = "decimal(18,2)")]
        public decimal CommissionReceived { get; set; } = 0;
        [JsonProperty(PropertyName = "commissionProcessingFee"), Column(TypeName = "decimal(18,2)")]
        public decimal ComissionProcessingFee { get; set; } = 0;
        [JsonProperty(PropertyName = "collectionExpense"), Column(TypeName = "decimal(18,2)")]
        public decimal CollectionExpense { get; set; } = 0;
    }
}
