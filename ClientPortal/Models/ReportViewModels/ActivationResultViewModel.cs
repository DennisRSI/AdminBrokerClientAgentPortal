using System.Collections.Generic;

namespace ClientPortal.Models
{
    public class ActivationResultViewModel
    {
        public ActivationResultViewModel()
        {
            Tables = new List<ActivationTableViewModel>();
        }

        public List<ActivationTableViewModel> Tables { get; set; }
    }
}
