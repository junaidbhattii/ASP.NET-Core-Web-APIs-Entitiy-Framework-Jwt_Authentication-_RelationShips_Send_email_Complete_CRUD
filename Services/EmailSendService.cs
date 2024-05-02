using JwtAuthentication_Relations_Authorization.Interfaces;
using System.Net;
using System.Net.Mail;

namespace JwtAuthentication_Relations_Authorization.Services
{
    public class EmailSendService : IEmailSendService
    {
        public Task SendEmailToUser(string email, string subject, string message)
        {
            var client = new SmtpClient("smtp.gmail.com", 587)
            {
                EnableSsl = true,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential("junaidbhatti198@gmail.com", "Enter Your AppPassword Here")
            };

            return client.SendMailAsync(
                new MailMessage(from: "junaidbhatti198@gmail.com",
                to: email,
                subject,
                message
                ));
        }
    }
}
