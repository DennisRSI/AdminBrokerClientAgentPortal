using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace VendorImport.Service.Models
{
    public class _SheetBase2Model : _SheetBase1Model
    {
        private int _siteId = 0, _affiliateId = 0;
        
        [JsonProperty(PropertyName = "affiliateId"), Required]
        public int AffiliateId { get { return _affiliateId; } set { _affiliateId = value; } }
        [JsonProperty(PropertyName = "affiliateName"), Required, StringLength(500)]
        public string AffiliateName { get; set; } = "";
        [JsonProperty(PropertyName = "siteId"), Required]
        public int SiteId { get { return _siteId; } set { _siteId = value; } }
        [JsonProperty(PropertyName = "siteName"), Required, StringLength(500)]
        public string SiteName { get; set; } = "";
        
        
        [JsonProperty(PropertyName = "checkoutDate"), Required]
        public DateTime CheckoutDate { get; set; } = new DateTime();
        [JsonProperty(PropertyName = "bookedDate"), Required]
        public DateTime BookedDate { get; set; } = new DateTime();
        [JsonProperty(PropertyName = "reservationStatus"), Required, StringLength(50)]
        public string ReservationStatus { get; set; } = "";
        [JsonProperty(PropertyName = "roomNights"), Required]
        public int RoomNights { get; set; } = 0;
        [JsonProperty(PropertyName = "flatAmount"), Column(TypeName = "decimal(18,2)")]
        public decimal FlatAmount { get; set; } = 0;
        [JsonProperty(PropertyName = "roomRevenue"), Column(TypeName = "decimal(18,2)")]
        public decimal RoomRevenue { get; set; } = 0;

        [JsonProperty(PropertyName = "arnCallCenterFee"), Column(TypeName = "decimal(18,2)")]
        public decimal ARNCallCenterFee { get; set; } = 0;
        [JsonProperty(PropertyName = "netCommission"), Column(TypeName = "decimal(18,2)")]
        public decimal NetCommission { get; set; } = 0;
        [JsonProperty(PropertyName = "subaffiliatecommission"), Column(TypeName = "decimal(18,2)")]
        public decimal SubAffiliateCommission { get; set; } = 0;
        [JsonProperty(PropertyName = "netCommissionAfterSubAffiliate"), Column(TypeName = "decimal(18,2)")]
        public decimal NetCommissionAfterSubAffiliate { get; set; } = 0;
        [JsonProperty(PropertyName = "reservationId")]
        public string ReservationId { get; set; } = "";
        [JsonProperty(PropertyName = "registrationId"), StringLength(1000)]
        public string RegistrationId { get; set; } = "";
        [JsonProperty(PropertyName = "registrationName"), StringLength(1000)]
        public string RegistrationName { get; set; } = "";
        [JsonProperty(PropertyName = "cid")]
        public string CID { get; set; } = "";


        [JsonIgnore, NotMapped]
        public string AffiliateString
        {
            set
            {
                if(!String.IsNullOrEmpty(value))
                {
                    string [] str = value.Split(" - ");

                    if(str.Length > 1)
                    {
                        if (int.TryParse(str[1], out _affiliateId))
                        {
                            AffiliateName = str[0];
                        }
                    }
                }
            }
        }
        [JsonIgnore, NotMapped]
        public string SiteString
        {
            set
            {
                if (value.Length > 0)
                {
                    string[] str = value.Split(" - ");

                    if (str.Length > 1)
                    {
                        if (int.TryParse(str[0], out _siteId))
                        {
                            SiteName = str[1];
                        }
                    }
                }
            }
        }

        


    }
}
