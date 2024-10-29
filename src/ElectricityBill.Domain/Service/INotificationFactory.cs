using ElectricityBill.Domain.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectricityBill.Domain.Service
{
    public interface INotificationFactory
    {
        Notification CreateWalletCreatedNotification(WalletCreatedEvent @event);
    }
}
