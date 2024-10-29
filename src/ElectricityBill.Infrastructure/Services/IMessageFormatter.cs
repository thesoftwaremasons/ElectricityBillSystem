using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace ElectricityBill.Infrastructure.Services
{
    public interface IMessageFormatter
    {
        MailMessage Format(string subject, string body, string recipient);
    }
}
