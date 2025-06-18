using Codes1.Service.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Codes1.Service.Models
{
    [Table("CampaignCodeRanges")]
    public class CampaignCodeRangeModel : _BaseModel
    {
        public CampaignCodeRangeModel()
        {
        }

        public CampaignCodeRangeModel(CampaignCodeRangeViewModel model)
        {
            CampaignId = model.CampaignId;
            CodeRangeId = model.CodeRangeId;
            IsActive = model.IsActive;
            CreationDate = model.CreationDate;
            CreatorIP = model.CreatorIP;
            DeactivationDate = model.DeactivationDate;
        }

        [Key, Required]
        public int CampaignCodeRangeId { get; set; }

        [Required]
        public int CampaignId { get; set; }

        [Required]
        public int CodeRangeId { get; set; }

        [ForeignKey("CampaignId")]
        public CampaignModel Campaign { get; set; }

        [ForeignKey("CodeRangeId")]
        public CodeRangeModel CodeRange { get; set; }
    }
}
