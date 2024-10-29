using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectricityBill.Infrastructure.Services
{
    public class ElectricityProviderFactory
    {
        private readonly IServiceProvider _serviceProvider;

        public ElectricityProviderFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public IElectricityProvider GetProvider(string validationReference)
        {
            // Simple round-robin provider selection
            return validationReference.Sum(c => (int)c) % 2 == 0
                ? _serviceProvider.GetRequiredService<TestProviderA>()
                : _serviceProvider.GetRequiredService<TestProviderB>();
        }
    }
}
