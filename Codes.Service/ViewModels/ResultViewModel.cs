using System.Collections.Generic;

namespace Codes.Service.ViewModels
{
    public class ResultViewModel
    {
        public IList<string> Messages { get; set; } = new List<string>();

        public bool IsSuccess
        {
            get { return Messages.Count == 0; }
        }
    }
}
