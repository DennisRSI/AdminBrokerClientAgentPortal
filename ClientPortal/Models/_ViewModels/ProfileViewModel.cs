namespace ClientPortal.Models._ViewModels
{
    public class ProfileViewModel : ApplicationUser
    {
        public int? DocumentW9Id { get; set; }
        public decimal CommissionRate { get; set; }
    }
}
