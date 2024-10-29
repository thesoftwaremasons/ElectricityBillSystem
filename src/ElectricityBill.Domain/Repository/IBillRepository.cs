using ElectricityBill.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectricityBill.Domain.Repositories
{
    public interface IBillRepository
    {
        Task<Bill> GetByIdAsync(Guid id);
        Task<Bill> GetByValidationReferenceAsync(string reference);
        Task AddAsync(Bill bill);
        Task UpdateAsync(Bill bill);
    }

}
