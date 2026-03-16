using Castle.Core.Smtp;
using Ecommerce.Helper;
using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Mail;

namespace Ecommerce.Services
{
    public class SendEmailService( IOptions<EmailOptions> emailOptions) : IEmailSender
    {
        EmailOptions _emailOptions = emailOptions.Value;
        public void Send(string from, string to, string subject, string messageText)
        {
            var msg = new MailMessage();
            msg.From = new MailAddress(from);
            msg.To.Add(to);

            msg.Subject = subject;
            msg.Body = messageText;
            msg.IsBodyHtml = true;
            
            var email = new SmtpClient(_emailOptions.SmtpServer, _emailOptions.Port);
            email.Credentials = new NetworkCredential(from,_emailOptions.Password );
            email.EnableSsl = true;

              email.SendMailAsync(msg).GetAwaiter().GetResult();


        }

        public void Send(MailMessage message)
        {
            throw new NotImplementedException();
        }

        public void Send(IEnumerable<MailMessage> messages)
        {
            throw new NotImplementedException();
        }
    }
}
