using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectricityBill.Domain.Models
{
    public class Bill
    {
        public Guid Id { get; private set; }
        public decimal Amount { get; private set; }
        public BillStatus Status { get; private set; }
        public string ValidationReference { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime? PaidAt { get; private set; }

        private Bill() { }

        public static Bill Create(decimal amount)
        {
            return new Bill
            {
                Id = Guid.NewGuid(),
                Amount = amount,
                Status = BillStatus.Pending,
                ValidationReference = Guid.NewGuid().ToString("N"),
                CreatedAt = DateTime.UtcNow
            };
        }

        public void MarkAsPaid()
        {
            Status = BillStatus.Paid;
            PaidAt = DateTime.UtcNow;
        }
    }

    public enum BillStatus
    {
        Pending,
        Paid
    }

}
