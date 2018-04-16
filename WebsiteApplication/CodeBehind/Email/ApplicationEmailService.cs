using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;

namespace WebsiteApplication.CodeBehind.Email
{
    public class ApplicationEmailService : IIdentityMessageService
    {
        public Task SendAsync(IdentityMessage message)
        {
            var fromAddress = new MailAddress("rafal.palej.mgr@gmail.com", "Rafał Palej");
            var toAddress = new MailAddress(message.Destination);
            const string fromPassword = "qwer7410";

            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
            };

            try
            {
                using (var emailMessage = new MailMessage(fromAddress, toAddress)
                {
                    Subject = message.Subject,
                    Body = message.Body,
                    IsBodyHtml = true
                })
                {
                    smtp.Send(emailMessage);
                    return Task.FromResult(true);
                }
            }
            catch
            {
                return Task.FromResult(false);
            }
        }
    }
}