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
    public class InMemoryBillRepository : IBillRepository
    {
        private readonly ConcurrentDictionary<Guid, Bill> _bills = new();
        private readonly ConcurrentDictionary<string, Bill> _billsByReference = new();

        public Task<Bill> GetByIdAsync(Guid id)
        {
            return Task.FromResult(_bills.TryGetValue(id, out var bill) ? bill : null);
        }

        public Task<Bill> GetByValidationReferenceAsync(string reference)
        {
            return Task.FromResult(_billsByReference.TryGetValue(reference, out var bill) ? bill : null);
        }

        public Task AddAsync(Bill bill)
        {
            _bills.TryAdd(bill.Id, bill);
            _billsByReference.TryAdd(bill.ValidationReference, bill);
            return Task.CompletedTask;
        }

        public Task UpdateAsync(Bill bill)
        {
            _bills.TryUpdate(bill.Id, bill, _bills[bill.Id]);
            _billsByReference.TryUpdate(bill.ValidationReference, bill, _billsByReference[bill.ValidationReference]);
            return Task.CompletedTask;
        }
    }

}
