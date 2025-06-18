using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClientPortal.Services
{
    public interface IEmail1Sender
    {
        Task SendEmailAsync(string email, string subject, string message);
    }
}
