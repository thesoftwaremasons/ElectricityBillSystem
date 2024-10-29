using ElectricityBill.Application.EventHandlers;
using ElectricityBill.Domain.EventBus;
using ElectricityBill.Domain.Events;


namespace ElectricityBill.Api.Services
{
    // ElectricityBill.Api/Services/EventSubscriptionService.cs
    public class EventSubscriptionService : IHostedService
    {
        private readonly IEventBus _eventBus;
        private readonly IServiceProvider _serviceProvider;

        public EventSubscriptionService(IEventBus eventBus, IServiceProvider serviceProvider)
        {
            _eventBus = eventBus;
            _serviceProvider = serviceProvider;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _eventBus.Subscribe<BillCreatedEvent>(async @event =>
            {
                using var scope = _serviceProvider.CreateScope();
                var handler = scope.ServiceProvider.GetRequiredService<BillCreatedEventHandler>();
                await handler.HandleAsync(@event);
            });

            _eventBus.Subscribe<PaymentCompletedEvent>(async @event =>
            {
                using var scope = _serviceProvider.CreateScope();
                var handler = scope.ServiceProvider.GetRequiredService<PaymentCompletedEventHandler>();
                await handler.HandleAsync(@event);
            });

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
