using System;

namespace Codes1.Service.Domain
{
    public class CodeGeneratorOptions
    {
        public string CampaignType { get; set; }
        public string Prefix { get; set; }
        public string Suffix { get; set; }
        public int Increment { get; set; }

        public int BrokerId { get; set; }
        public int ClientId { get; set; }
        public int CampaignId { get; set; }

        public int PackageId { get; set; }
        public int Padding { get; set; }

        public int StartNumber { get; set; }
        public int EndNumber { get; set; }

        public int FaceValue { get; set; }

        public int Quantity { get; set; }
        public int ActivationsPerCode { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
