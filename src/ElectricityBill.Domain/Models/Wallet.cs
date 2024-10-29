using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectricityBill.Domain.Models
{

    public class Wallet
    {
        public Guid Id { get; private set; }
       public string OwnerUserName {  get; private set; }
       public string PhoneNumber {  get;  private set; }
    
        public decimal Balance { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime UpdatedAt { get; private set; }

        private Wallet() { }

        public static Wallet Create()
        {
            return new Wallet
            {
                Id = Guid.NewGuid(),
                Balance = 0,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };
        }
        public static Wallet Create(string ownerUserName,string phoneNumber)
        {
            return new Wallet
            {
                Id = Guid.NewGuid(),
                Balance = 0,
                OwnerUserName=ownerUserName,
                PhoneNumber = phoneNumber,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };
        }

        public void AddFunds(decimal amount)
        {
            if (amount <= 0)
                throw new InvalidOperationException("Amount must be greater than zero");

            Balance += amount;
            UpdatedAt = DateTime.UtcNow;
        }

        public bool DeductFunds(decimal amount)
        {
            if (amount <= 0)
                throw new InvalidOperationException("Amount must be greater than zero");

            if (Balance < amount)
                return false;

            Balance -= amount;
            UpdatedAt = DateTime.UtcNow;
            return true;
        }
    }
}
