using Codes1.Service.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Codes1.Service.Models
{
    [Table("Campaigns")]
    public class CampaignModel : _BaseModel
    {
        public CampaignModel()
        {
        }

        public CampaignModel(CampaignViewModel model)
        {
            CampaignId = model.CampaignId;
            CampaignName = model.CampaignName;
            CampaignType = model.CampaignType;
            PackageId = model.PackageId;
            StartDateTime = model.StartDateTime;
            EndDateTime = model.EndDateTime;
            CampaignDescription = model.CampaignDescription;
            GoogleAnalyticsCode = model.GoogleAnalyticsCode;
            CustomCSS = model.CustomCSS;
            DeactivationReason = model.DeactivationReason;
            ClientId = model.Client != null && model.Client.ClientId > 0 ? model.Client.ClientId : 0;
            BrokerId = model.Broker != null && model.Broker.BrokerId > 0 ? model.Broker.BrokerId : 0;

            IsActive = model.IsActive;
            CreationDate = model.CreationDate;
            CreatorIP = model.CreatorIP;
            DeactivationDate = model.DeactivationDate;
        }

        [Key, Required]
        public int CampaignId { get; set; } = 0;

        [Required, StringLength(100)]
        public string CampaignName { get; set; }

        [Required, StringLength(50)]
        public string CampaignType { get; set; } = "Virtual";

        [Required]
        public int CardQuantity { get; set; }

        public DateTime? StartDateTime { get; set; } = null;
        public DateTime? EndDateTime { get; set; } = null;
        public string CampaignDescription { get; set; }
        public string GoogleAnalyticsCode { get; set; } = null;
        public string CustomCSS { get; set; } = null;
        public string CustomCssPost { get; set; } = null;

        public string DeactivationReason { get; set; } = null;

        [Required]
        public int PackageId { get; set; }

        [Required]
        public int NumberOfUses { get; set; } = 1;

        [Required]
        public float Points { get; set; } = 0;

        public DateTime? StartDate { get; set; } = null;
        public DateTime? EndDate { get; set; } = null;

        [Required]
        public decimal Cost { get; set; } = 0;

        [Required]
        public bool VerifyEmail { get; set; } = true;

        public int? ClientId { get; set; }

        [ForeignKey("ClientId")]
        public ClientModel Client { get; set; }

        public int BrokerId { get; set; }

        [ForeignKey("BrokerId")]
        public BrokerModel Broker { get; set; }

        public int? PreLoginVideoId { get; set; }

        [ForeignKey("PreLoginVideoId")]
        public virtual VideoModel PreLoginVideo { get; set; }

        public int? PostLoginVideoId { get; set; }

        [ForeignKey("PostLoginVideoId")]
        public virtual VideoModel PostLoginVideo { get; set; }

        public ICollection<CampaignAgentModel> CampaignAgents { get; set; }
        public ICollection<UnusedCodeModel> UnusedCodes { get; set; }
        public ICollection<UsedCodeModel> UsedCodes { get; set; }
        public ICollection<PendingCodeModel> PendingCodes { get; set; }
        public ICollection<CampaignCodeRangeModel> CampaignCodeRange { get; set; }
    }
}
