namespace ClientPortal.Models
{
    public enum Role
    {
        SuperAdministrator,
        Administrator,
        Broker,
        Agent,
        Client
    }

    public static class RoleExtensions
    {
        public static string GetName(this Role role)
        {
            if (role == Role.SuperAdministrator)
            {
                return "Super Administrator";
            }

            return role.ToString();
        }
    }
}
