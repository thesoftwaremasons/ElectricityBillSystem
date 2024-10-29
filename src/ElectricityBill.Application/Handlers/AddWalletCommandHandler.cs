using ElectricityBill.Application.Commands;

using ElectricityBill.Common;
using ElectricityBill.Domain.EventBus;
using ElectricityBill.Domain.Events;
using ElectricityBill.Domain.Models;
using ElectricityBill.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectricityBill.Application.Handlers
{
    // ElectricityBill.Application/Commands/AddWallet/AddWalletCommandHandler.cs
    public class AddWalletCommandHandler : ICommandHandler<AddWalletCommand, Guid>
    {
        private readonly IWalletRepository _walletRepository;
        private readonly IEventBus _eventBus;

        public AddWalletCommandHandler(IWalletRepository walletRepository, IEventBus eventBus)
        {
            _walletRepository = walletRepository ?? throw new ArgumentNullException(nameof(walletRepository));
            _eventBus = eventBus ?? throw new ArgumentNullException(nameof(eventBus));
        }

        public async Task<Guid> HandleAsync(AddWalletCommand command)
        {
            if (command == null)
                throw new ArgumentNullException(nameof(command));

            if (string.IsNullOrWhiteSpace(command.OwnerName))
                throw new ValidationException("Owner name is required");

            if (string.IsNullOrWhiteSpace(command.PhoneNumber))
                throw new ValidationException("Phone number is required");

            if (command.InitialBalance <= 0)
                throw new ValidationException("Initial balance must be greater than 0");

            var wallet =  Wallet.Create(command.OwnerName,command.PhoneNumber);

            await _walletRepository.AddAsync(wallet);
            var getEvent = new WalletCreatedEvent(wallet.OwnerUserName, wallet.PhoneNumber, wallet.Balance);
      

            await _eventBus.PublishAsync(getEvent);

            return wallet.Id;
        }
    }
}
