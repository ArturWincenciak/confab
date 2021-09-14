using System;
using System.Linq;
using System.Threading.Tasks;
using Confab.Shared.Abstractions.Events;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Confab.Shared.Infrastructure.Events
{
    internal sealed class EventDispatcher : IEventDispatcher
    {
        private readonly ILogger<EventDispatcher> _logger;
        private readonly IServiceProvider _serviceProvider;

        public EventDispatcher(IServiceProvider serviceProvider, ILogger<EventDispatcher> logger)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
        }

        public async Task PublishAsync<TEvent>(TEvent @event) where TEvent : class, IEvent
        {
            try
            {
                _logger.LogTrace($"Dispatching event: '{@event}'.");

                using var scope = _serviceProvider.CreateScope();

                var handlers = scope.ServiceProvider.GetServices<IEventHandler<TEvent>>();
                if (!handlers.Any())
                {
                    _logger.LogTrace($"There is no handlers for event: '{@event}'.");
                    return;
                }

                var tasks = handlers.Select(x =>
                {
                    try
                    {
                        _logger.LogTrace($"Executing handler: '{x}', with event: '{@event}'.");
                        return x.HandleAsync(@event);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, $"{ex.Message}; Handler: '{x}'; Event: '{@event}'.");
                        throw;
                    }
                });

                await Task.WhenAll(tasks);
                _logger.LogTrace($"Event has been dispatched: '{@event}'.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{ex.Message}");
                throw;
            }
        }
    }
}