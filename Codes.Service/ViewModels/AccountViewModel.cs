namespace Codes1.Service.ViewModels
{
    //
    // A generic model that works for all account types
    //
    public class AccountViewModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string CompanyName { get; set; }

        public string FullName
        {
            get { return $"{FirstName} {LastName}";  }
        }
    }
}
