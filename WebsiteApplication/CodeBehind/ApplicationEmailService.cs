using Microsoft.AspNet.Identity;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace WebsiteApplication.CodeBehind
{
    public class ApplicationEmailService : IIdentityMessageService
    {
        public Task SendAsync(IdentityMessage message)
        {
            var fromAddress = new MailAddress("palej@student.agh.edu.pl", "Rafał Palej");
            var toAddress = new MailAddress(message.Destination);
            const string fromPassword = "zaq!2wsx";

            var smtp = new SmtpClient
            {
                Host = "poczta.agh.edu.pl",
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
                    IsBodyHtml = true,
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