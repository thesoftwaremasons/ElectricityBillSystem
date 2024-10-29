using ElectricityBill.Application.Commands;
using ElectricityBill.Application.CQRS;

using ElectricityBill.Common;
using ElectricityBill.Domain.EventBus;
using ElectricityBill.Domain.Events;
using ElectricityBill.Domain.Models;
using ElectricityBill.Domain.Repositories;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectricityBill.Application.Handlers
{
    public class VerifyBillCommandHandler : ICommandHandler<VerifyBillCommand,string>
    {
        private readonly IBillRepository _billRepository;
        private readonly IEventBus _eventBus;
        private readonly ILogger<VerifyBillCommandHandler> _logger;

        public VerifyBillCommandHandler(
            IBillRepository billRepository,
            IEventBus eventBus,
            ILogger<VerifyBillCommandHandler> logger)
        {
            _billRepository = billRepository;
            _eventBus = eventBus;
            _logger = logger;
        }

        public async Task<string> HandleAsync(VerifyBillCommand command)
        {
            var bill = Bill.Create(command.Amount);
            await _billRepository.AddAsync(bill);

            var @event = new BillCreatedEvent(bill.Id, bill.Amount, bill.ValidationReference);
            await _eventBus.PublishAsync(@event);
            _logger.LogInformation("Bill created with reference: {Reference}", bill.ValidationReference);
            return bill.ValidationReference;

           
        }
    }

}
