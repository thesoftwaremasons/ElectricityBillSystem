using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectricityBill.Infrastructure.Services
{
    public interface IElectricityProvider
    {
        Task<string> GenerateTokenAsync(string validationReference, decimal amount);
    }

}
