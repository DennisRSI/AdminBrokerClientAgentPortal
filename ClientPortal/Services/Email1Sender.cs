using System.Threading.Tasks;
using Mandrill;
using Mandrill.Model;

namespace ClientPortal.Services
{
    // This class is used by the application to send email for account confirmation and password reset.
    // For more details see https://go.microsoft.com/fwlink/?LinkID=532713
    public class Email1Sender : IEmail1Sender
    {
        public Task SendEmailAsync(string to, string subject, string body)
        {
            var mandrill = new MandrillApi("BlXciasbFAY6oGYRldmG5A");
            var message = new MandrillMessage("noreply@travnow.com", to, subject, body);

            return mandrill.Messages.SendAsync(message);
        }
    }
}
