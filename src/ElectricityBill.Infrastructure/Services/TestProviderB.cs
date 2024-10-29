using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectricityBill.Infrastructure.Services
{
    public class TestProviderB : IElectricityProvider
    {
        public Task<string> GenerateTokenAsync(string validationReference, decimal amount)
        {
            
             Task.Delay(500);
            return Task.FromResult($"B-{Guid.NewGuid().ToString("N").Substring(0, 8).ToUpper()}");
        }
    }
}
