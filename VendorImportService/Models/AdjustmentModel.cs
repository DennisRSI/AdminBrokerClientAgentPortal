using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace VendorImport.Service.Models
{
    [Table("Adjustments")]
    public class AdjustmentModel : _SheetBase1Model
    {
        [JsonProperty(PropertyName = "commissionAdjustment"), Column(TypeName = "decimal(18,2)")]
        public decimal CommissionAdjustment { get; set; }
        [JsonProperty(PropertyName = "Notes")]
        public string Notes { get; set; }
    }
}
