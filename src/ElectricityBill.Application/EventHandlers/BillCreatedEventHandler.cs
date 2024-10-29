
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
    public class BillCreatedEventHandler:IEventHandler<BillCreatedEvent>
    {
        private readonly ISmsService _smsService;
        private readonly ILogger<BillCreatedEventHandler> _logger;

        public BillCreatedEventHandler(ISmsService smsService, ILogger<BillCreatedEventHandler> logger)
        {
            _smsService = smsService;
            _logger = logger;
        }

        public async Task HandleAsync(BillCreatedEvent @event)
        {
            var message = $"Your electricity bill validation reference is: {@event.ValidationReference}. " +
                          $"Amount: {@event.Amount:C}";

            await _smsService.SendAsync("PHONE_NUMBER", message);
            _logger.LogInformation("Bill creation SMS sent for reference: {@event.ValidationReference}");
        }
    }
}
