using System;
using System.Collections.Generic;
using System.Text;

namespace Codes1.Service.ViewModels
{
    public class CardsTotalViewModel
    {
        public int AssignedPhysical { get; set; } = 0;
        public int AssignedVirtual { get; set; } = 0;
        public int InCampaignsPhysical { get; set; } = 0;
        public int InCampaignsVirtual { get; set; } = 0;
        public int AvailablePhysical { get; set; } = 0;
        public int AvailableVirtual { get; set; } = 0;
    }
}
