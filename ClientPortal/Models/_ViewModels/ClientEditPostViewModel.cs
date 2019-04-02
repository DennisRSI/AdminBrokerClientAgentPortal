namespace ClientPortal.Models._ViewModels
{
    public class ClientEditPostViewModel : ApplicationUser
    {
        public int AssignedAgent { get; set; }
        public float CommissionRate { get; set; } = 0;
    }
}
