using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Codes.Service.ViewModels
{
    public class AdminListViewModel : _BaseListViewModel
    {
        public AdminListViewModel()
        {

        }
        public AdminListViewModel(string accountId, string firstName, string middleName, string lastName, string phone, string extension
            , string email, string company, DateTime activationDate, DateTime? deactivationDate)
            : base(accountId, firstName, middleName, lastName, phone, extension, email, company, activationDate, deactivationDate) { }
    }
}
