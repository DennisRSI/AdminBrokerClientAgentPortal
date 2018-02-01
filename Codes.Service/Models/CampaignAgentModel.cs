using Codes.Service.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Codes.Service.Models
{
    [Table("CampaignAgents")]
    public class CampaignAgentModel : _BaseModel
    {
        public CampaignAgentModel()
        {

        }

        public CampaignAgentModel(CampaignAgentViewModel model)
        {
            CampaignId = model.CampaignId;
            AgentId = model.AgentId;

            IsActive = model.IsActive;
            CreationDate = model.CreationDate;
            CreatorIP = model.CreatorIP;
            DeactivationDate = model.DeactivationDate;
        }

        public int CampaignId { get; set; }
        public CampaignModel Campaign { get; set; }

        public int AgentId { get; set; }
        public AgentModel Agent { get; set; }
    }
}
