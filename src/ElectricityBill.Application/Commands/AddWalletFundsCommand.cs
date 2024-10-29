using ElectricityBill.Application.CQRS;
using ElectricityBill.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectricityBill.Application.Commands
{
    public class AddWalletFundsCommand : ICommand
    {
        public Guid WalletId { get; set; }
        public decimal Amount { get; set; }
    }
}
