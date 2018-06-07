using System;

namespace Codes.Service.ViewModels
{
    public class ActivationReportViewModel
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int? AgentId { get; set; }
        public int? ClientId { get; set; }
        public int? BrokerId { get; set; }
        public string CampaignStatus { get; set; }
        public bool IsCardUsed { get; set; }
    }
}
