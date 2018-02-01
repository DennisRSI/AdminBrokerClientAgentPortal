using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
                string name = FirstName;
                if (MiddleName.Length > 0)
                    name += $" {MiddleName}";
                name += $" {LastName}";

                return name;

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
