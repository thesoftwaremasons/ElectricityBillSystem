using ElectricityBill.Domain.Events;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectricityBill.Domain.EventBus
{
 

    // ElectricityBill.Infrastructure/EventBus/InMemoryEventBus.cs
    public class InMemoryEventBus : IEventBus
    {
        private readonly Dictionary<Type, List<Delegate>> _handlers = new();
        private readonly ILogger<InMemoryEventBus> _logger;

        public InMemoryEventBus(ILogger<InMemoryEventBus> logger)
        {
            _logger = logger;
        }

        public async Task PublishAsync<T>(T @event) where T : IEvent
        {
            var eventType = typeof(T);
            if (_handlers.ContainsKey(eventType))
            {
                foreach (var handler in _handlers[eventType])
                {
                    try
                    {
                        await Task.Run(() => ((Action<T>)handler)(@event));
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "Error handling event {EventType}", eventType.Name);
                    }
                }
            }
        }

        public void Subscribe<T>(Action<T> handler) where T : IEvent
        {
            var eventType = typeof(T);
            if (!_handlers.ContainsKey(eventType))
            {
                _handlers[eventType] = new List<Delegate>();
            }
            _handlers[eventType].Add(handler);
        }
    }
}
