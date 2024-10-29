using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectricityBill.Infrastructure.Services
{
    public class TestProviderA : IElectricityProvider
    {
        public  Task<string> GenerateTokenAsync(string validationReference, decimal amount)
        {
            // Simulate API call delay
             Task.Delay(500);
            return Task.FromResult($"A-{Guid.NewGuid().ToString("N").Substring(0, 8).ToUpper()}");
        }
    }
}
