namespace ClientPortal.Models._ViewModels
{
    public class ProfileViewModel : ApplicationUser
    {
        public int? DocumentW9Id { get; set; }
        public decimal CommissionRate { get; set; }
        public bool IsDisabled { get; set; } = true;
        public string ParentAgentName { get; set; }
    }
}
