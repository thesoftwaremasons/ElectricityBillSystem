using ElectricityBill.Domain.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectricityBill.Domain.Services
{
    public interface INotificationService
    {
        Task SendAsync(Notification notification);
    }
}
