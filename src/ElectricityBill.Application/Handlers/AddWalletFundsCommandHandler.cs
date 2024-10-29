using ElectricityBill.Application.Commands;

using ElectricityBill.Common;
using ElectricityBill.Common.Exceptions;
using ElectricityBill.Domain.EventBus;
using ElectricityBill.Domain.Repositories;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectricityBill.Application.Handlers
{
    public class AddWalletFundsCommandHandler : ICommandHandler<AddWalletFundsCommand,Unit>
    {
        private readonly IWalletRepository _walletRepository;
        private readonly IEventBus _eventBus;
        private readonly ILogger<AddWalletFundsCommandHandler> _logger;

        public AddWalletFundsCommandHandler(
            IWalletRepository walletRepository,
            IEventBus eventBus,
            ILogger<AddWalletFundsCommandHandler> logger)
        {
            _walletRepository = walletRepository;
            _eventBus = eventBus;
            _logger = logger;
        }

        public async Task<Unit> HandleAsync(AddWalletFundsCommand command)
        {
            var wallet = await _walletRepository.GetByIdAsync(command.WalletId);
            if (wallet == null)
                throw new NotFoundException("Wallet not found");

            wallet.AddFunds(command.Amount);
            await _walletRepository.UpdateAsync(wallet);

            _logger.LogInformation("Funds added to wallet: {WalletId}", command.WalletId);
            return Unit.Value;
        }
    }
}
