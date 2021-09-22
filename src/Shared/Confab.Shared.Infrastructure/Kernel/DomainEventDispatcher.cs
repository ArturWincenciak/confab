using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Confab.Shared.Abstractions.Kernel;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Confab.Shared.Infrastructure.Kernel
{
    internal sealed class DomainEventDispatcher : IDomainEventDispatcher
    {
        private readonly ILogger<DomainEventDispatcher> _logger;
        private readonly IServiceProvider _serviceProvider;

        public DomainEventDispatcher(IServiceProvider serviceProvider, ILogger<DomainEventDispatcher> logger)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
        }

        public async Task SendAsync(params IDomainEvent[] events)
        {
            var eventsLog = string.Join(";", events ?? Enumerable.Empty<IDomainEvent>());

            try
            {
                _logger.LogTrace($"Dispatching domain events: '{eventsLog}'.");

                using var scope = _serviceProvider.CreateScope();

                foreach (var @event in events)
                {
                    var eventType = @event.GetType();
                    var handlerType = typeof(IDomainEventHandler<>).MakeGenericType(eventType);
                    var handlers = scope.ServiceProvider.GetServices(handlerType);
                    if (!handlers.Any())
                    {
                        _logger.LogTrace($"There are no handlers for event: '{@event}'.");
                        continue;
                    }

                    var tasks = new List<Task>();
                    foreach (var handler in handlers)
                    {
                        var method = handlerType.GetMethod(nameof(IDomainEventHandler<IDomainEvent>.HandleAsync));
                        _logger.LogTrace($"Executing handler: '{handler}', with event: '{@event}'.");
                        var task = (Task) method.Invoke(handler, new[] {@event});
                        tasks.Add(task);
                    }

                    await Task.WhenAll(tasks);
                    _logger.LogTrace($"Event has been dispatched to all handlers: '{@event}'.");
                }

                _logger.LogTrace($"All events has been dispatched to all handlers: '{eventsLog}'.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{ex.Message}; Event: '{eventsLog}'.");
                throw;
            }
        }
    }
}