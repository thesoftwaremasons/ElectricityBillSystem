using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectricityBill.Domain.Events
{
    public class PaymentCompletedEvent : IEvent
    {
        public Guid Id { get; private set; }
        public DateTime Timestamp { get; private set; }
        public Guid BillId { get; private set; }
        public Guid WalletId { get; private set; }
        public decimal Amount { get; private set; }
        public string Token { get; private set; }

        public PaymentCompletedEvent(Guid billId, Guid walletId, decimal amount, string token)
        {
            Id = Guid.NewGuid();
            Timestamp = DateTime.UtcNow;
            BillId = billId;
            WalletId = walletId;
            Amount = amount;
            Token = token;
        }
    }
}
