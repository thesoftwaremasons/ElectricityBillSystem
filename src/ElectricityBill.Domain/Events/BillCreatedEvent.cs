using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectricityBill.Domain.Events
{
    public class BillCreatedEvent : IEvent
    {
        public Guid Id { get; private set; }
        public DateTime Timestamp { get; private set; }
        public Guid BillId { get; private set; }
        public decimal Amount { get; private set; }
        public string ValidationReference { get; private set; }

        public BillCreatedEvent(Guid billId, decimal amount, string validationReference)
        {
            Id = Guid.NewGuid();
            Timestamp = DateTime.UtcNow;
            BillId = billId;
            Amount = amount;
            ValidationReference = validationReference;
        }
    }

}
