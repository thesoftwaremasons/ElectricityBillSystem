using ElectricityBill.Application.Commands;

using ElectricityBill.Common;
using ElectricityBill.Common.Exceptions;
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
    public class ProcessPaymentCommandHandler : ICommandHandler<ProcessPaymentCommand,Unit>
    {
        private readonly IBillRepository _billRepository;
        private readonly IWalletRepository _walletRepository;
        private readonly IEventBus _eventBus;
        private readonly ILogger<ProcessPaymentCommandHandler> _logger;

        public ProcessPaymentCommandHandler(
            IBillRepository billRepository,
            IWalletRepository walletRepository,
            IEventBus eventBus,
            ILogger<ProcessPaymentCommandHandler> logger)
        {
            _billRepository = billRepository;
            _walletRepository = walletRepository;
            _eventBus = eventBus;
            _logger = logger;
        }

        public async Task<Unit> HandleAsync(ProcessPaymentCommand command)
        {
            var bill = await _billRepository.GetByValidationReferenceAsync(command.ValidationReference);
            if (bill == null)
                throw new NotFoundException("Bill not found");

            if (bill.Status == BillStatus.Paid)
                throw new InvalidOperationException("Bill is already paid");

            var wallet = await _walletRepository.GetByIdAsync(command.WalletId);
            if (wallet == null)
                throw new NotFoundException("Wallet not found");

            if (!wallet.DeductFunds(bill.Amount))
                throw new InsufficientFundsException("Insufficient funds in wallet");

            bill.MarkAsPaid();

            await _walletRepository.UpdateAsync(wallet);
            await _billRepository.UpdateAsync(bill);

            var token = GenerateToken();
            var @event = new PaymentCompletedEvent(bill.Id, wallet.Id, bill.Amount, token);
            await _eventBus.PublishAsync(@event);

            _logger.LogInformation("Payment processed for bill: {Reference}", command.ValidationReference);
            return Unit.Value;
        }

        private string GenerateToken() => Guid.NewGuid().ToString("N").Substring(0, 8).ToUpper();

      
    }
}
