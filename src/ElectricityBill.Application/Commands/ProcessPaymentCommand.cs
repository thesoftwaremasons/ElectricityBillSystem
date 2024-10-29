using ElectricityBill.Application.CQRS;
using ElectricityBill.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectricityBill.Application.Commands
{
    public class ProcessPaymentCommand : ICommand
    {
        public required string ValidationReference { get; set; }
        public required Guid WalletId { get; set; }
        public required string PhoneNumber { get; set; }
    }
}
