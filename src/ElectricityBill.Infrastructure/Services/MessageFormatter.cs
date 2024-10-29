using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace ElectricityBill.Infrastructure.Services
{
    public class MessageFormatter : IMessageFormatter
    {
        public MailMessage Format(string subject, string body, string recipient)
        {
            var mailMessage = new MailMessage
            {
                From = new MailAddress("your-email@example.com"),
                Subject = subject,
                Body = body,
                IsBodyHtml = false
            };

            mailMessage.To.Add(recipient);

            return mailMessage;
        }
    }
}
