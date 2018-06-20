using System.ComponentModel.DataAnnotations;

namespace ClientPortal.Models._ViewModels
{
    public class AddUserViewModel
    {
        public string UserType { get; set; }
        public int BrokerId { get; set; }

        [DisplayFormat(DataFormatString = "{0:0.00}", ApplyFormatInEditMode = true)]
        public decimal CommissionRate { get; set; }
    }
}
