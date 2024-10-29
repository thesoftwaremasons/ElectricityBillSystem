using ElectricityBill.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectricityBill.Application.Commands
{
    // ElectricityBill.Application/Commands/AddWallet/AddWalletCommand.cs
    public class AddWalletCommand : ICommand<Guid>
    {
        [Required]
        public required string OwnerName { get; set; }

        [Required]
        [Phone]
        public required string PhoneNumber { get; set; }

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Initial balance must be greater than 0")]
        public decimal InitialBalance { get; set; }
    }
}
