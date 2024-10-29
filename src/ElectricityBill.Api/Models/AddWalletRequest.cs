using System.ComponentModel.DataAnnotations;

namespace ElectricityBill.Api.Models
{

    public class AddWalletRequest
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

    public class AddWalletResponse
    {
        public Guid WalletId { get; set; }
    }
}
