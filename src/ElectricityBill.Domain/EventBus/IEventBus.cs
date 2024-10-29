using ElectricityBill.Domain.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectricityBill.Domain.EventBus
{
    public interface IEventBus
    {
        Task PublishAsync<T>(T @event) where T : IEvent;
        void Subscribe<T>(Action<T> handler) where T : IEvent;
    }

}
