
using ElectricityBill.Domain.Events;
using ElectricityBill.Domain.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ElectricityBill.Application.Notifications;

namespace ElectricityBill.Application.Services
{
    public class NotificationFactory : INotificationFactory
    {
        public Notification CreateWalletCreatedNotification(WalletCreatedEvent @event)
        {
            return new WalletCreatedNotification(@event.OwnerUserName, @event.PhoneNumber);
        }
    }
}
