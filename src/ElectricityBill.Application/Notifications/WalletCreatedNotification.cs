using ElectricityBill.Domain.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectricityBill.Application.Notifications
{
    // ElectricityBill.Application/Common/Notifications/WalletCreatedNotification.cs
    public class WalletCreatedNotification : Notification
    {
        public WalletCreatedNotification(string ownerName, string phoneNumber)
        {
            Recipient = phoneNumber;

            Subject = "Your new Wallet has been created";

            Body = $@"Hello {ownerName},

Your new Wallet has been successfully created! Here are the details:

Wallet Id: {Id}
Owner Name: {ownerName}
Phone Number: {phoneNumber}
Initial Balance: {Balance:C2}

Thank you for using our services!

Best regards,
Electricity Billing Team";
        }
    }
}
