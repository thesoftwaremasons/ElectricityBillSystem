using ElectricityBill.Domain.Service;
using ElectricityBill.Domain.Services;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace ElectricityBill.Infrastructure.Services
{
    // ElectricityBill.Application/Services/NotificationService.cs
    public class NotificationService : INotificationService
    {
        private readonly SmtpClient _smtpClient;
        private readonly IMessageFormatter _messageFormatter;
        private readonly INotificationFactory _notificationFactory;

        public NotificationService(
            INotificationFactory notificationFactory,
            SmtpClient smtpClient,
            IMessageFormatter messageFormatter)
        {
            smtpClient = smtpClient ?? throw new ArgumentNullException(nameof(smtpClient));
            _messageFormatter = messageFormatter ?? throw new ArgumentNullException(nameof(messageFormatter));
            _notificationFactory = notificationFactory ?? throw new ArgumentNullException(nameof(notificationFactory));
        }

        public async Task SendAsync(Notification notification)
        {
            if (notification == null)
                throw new ArgumentNullException(nameof(notification));

            if (string.IsNullOrWhiteSpace(notification.Recipient))
                throw new ArgumentException("Recipient is required");

            if (string.IsNullOrWhiteSpace(notification.Subject))
                throw new ArgumentException("Subject is required");

            if (string.IsNullOrWhiteSpace(notification.Body))
                throw new ArgumentException("Body is required");

            var message = _messageFormatter.Format(notification.Subject, notification.Body, notification.Recipient);

            await _smtpClient.SendMailAsync(message);
        }
    }
}
