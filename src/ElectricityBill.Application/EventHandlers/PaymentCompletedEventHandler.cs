using ElectricityBill.Common.Events;
using ElectricityBill.Domain.Events;
using ElectricityBill.Domain.Service;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectricityBill.Application.EventHandlers
{
    public class PaymentCompletedEventHandler: IEventHandler<PaymentCompletedEvent>
    {
        private readonly ISmsService _smsService;
        private readonly ILogger<PaymentCompletedEventHandler> _logger;

        public PaymentCompletedEventHandler(ISmsService smsService, ILogger<PaymentCompletedEventHandler> logger)
        {
            _smsService = smsService;
            _logger = logger;
        }

        public async Task HandleAsync(PaymentCompletedEvent @event)
        {
            var message = $"Your electricity token is: {@event.Token}. Amount: {@event.Amount:C}";

            await _smsService.SendAsync("PHONE_NUMBER", message);
            _logger.LogInformation("Payment completion SMS sent for bill: {@event.BillId}");
        }
    }
}
