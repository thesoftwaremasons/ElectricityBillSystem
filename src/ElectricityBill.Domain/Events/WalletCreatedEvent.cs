using ElectricityBill.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectricityBill.Domain.Events
{
    public class WalletCreatedEvent: IEvent
    {
        public Guid Id { get; set; }
        public string OwnerUserName { get; set; }
        public string PhoneNumber { get; set; }
        public decimal Balance { get; set; }
        public DateTime Timestamp { get; set; }

        public WalletCreatedEvent(string ownerUserName, string phoneNumber, decimal balance)
        {
            Id = Guid.NewGuid();
            Timestamp = DateTime.UtcNow;
            OwnerUserName = ownerUserName;
            PhoneNumber = phoneNumber;
            Balance = balance;
            
        }
    }
}
