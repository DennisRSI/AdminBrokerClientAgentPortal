using System;

namespace ClientPortal.Models._ViewModels
{
    public class LoggedInViewModel
    {
        public string Id { get; set; } = "";
        public string FirstName { get; set; } = "";
        public string MiddleName { get; set; } = "";
        public string LastName { get; set; } = "";

        public string FullName
        {
            get
            {
                var middle = " ";

                if (!String.IsNullOrWhiteSpace(MiddleName))
                {
                    middle = $" {MiddleName} ";
                }

                return $"{FirstName}{middle}{LastName}";
            }
        }

        public string ShortName
        {
            get
            {
                string name = "";
                if (FirstName.Length > 0)
                    name += FirstName.Substring(0, 1).ToLower();
                
                name += LastName.ToLower();

                return name;

            }
        }


    }
}
