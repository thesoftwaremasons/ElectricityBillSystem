using ElectricityBill.Application.Notifications;
using ElectricityBill.Common.Events;
using ElectricityBill.Domain.Events;
using ElectricityBill.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectricityBill.Application.Event
{
   
    public class WalletCreatedEventHandler : IEventHandler<WalletCreatedEvent>
    {
        private readonly INotificationService _notificationService;

        public WalletCreatedEventHandler(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        public async Task HandleAsync(WalletCreatedEvent @event)
        {
            // Send an email, SMS, or any other notification to the user
            // about their new wallet creation
            await _notificationService.SendAsync(new WalletCreatedNotification(@event.OwnerUserName, @event.PhoneNumber));
           
        }
    }
}
