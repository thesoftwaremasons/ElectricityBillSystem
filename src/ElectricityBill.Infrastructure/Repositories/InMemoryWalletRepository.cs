using ElectricityBill.Domain.Repositories;
using ElectricityBill.Domain.Models;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectricityBill.Infrastructure.Repositories
{
    public class InMemoryWalletRepository : IWalletRepository
    {
        private readonly ConcurrentDictionary<Guid, Wallet> _wallets = new();

        public Task<Wallet> GetByIdAsync(Guid id)
        {
            return Task.FromResult(_wallets.TryGetValue(id, out var wallet) ? wallet : null);
        }

        public Task AddAsync(Wallet wallet)
        {
            _wallets.TryAdd(wallet.Id, wallet);
            return Task.CompletedTask;
        }

        public Task UpdateAsync(Wallet wallet)
        {
            _wallets.TryUpdate(wallet.Id, wallet, _wallets[wallet.Id]);
            return Task.CompletedTask;
        }
    }
}
