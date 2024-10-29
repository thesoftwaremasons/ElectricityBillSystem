namespace ElectricityBill.Api.Models
{
    public class ProcessPaymentRequest
    {
        public Guid WalletId { get; set; }
        public string PhoneNumber { get; set; }

    }
}
