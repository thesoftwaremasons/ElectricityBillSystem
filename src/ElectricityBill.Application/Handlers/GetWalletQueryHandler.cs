using ElectricityBill.Application.CQRS;
using ElectricityBill.Application.DTOs;
using ElectricityBill.Application.Queries;
using ElectricityBill.Domain.Repositories;
using ElectricityBill.Common.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectricityBill.Application.Handlers
{
    public class GetWalletQueryHandler : IQueryHandler<GetWalletQuery, WalletDto>
    {
        private readonly IWalletRepository _walletRepository;

        public GetWalletQueryHandler(IWalletRepository walletRepository)
        {
            _walletRepository = walletRepository;
        }

        public async Task<WalletDto> HandleAsync(GetWalletQuery query)
        {
            var wallet = await _walletRepository.GetByIdAsync(query.WalletId);
            if (wallet == null)
                throw new NotFoundException("Wallet not found");

            return new WalletDto
            {
                Id = wallet.Id,
                Balance = wallet.Balance,
                UpdatedAt = wallet.UpdatedAt
            };
        }
    }
}
