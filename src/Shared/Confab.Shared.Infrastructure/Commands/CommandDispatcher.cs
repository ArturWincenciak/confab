using System;
using System.Threading.Tasks;
using Confab.Shared.Abstractions.Commands;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Confab.Shared.Infrastructure.Commands
{
    internal sealed class CommandDispatcher : ICommandDispatcher
    {
        private readonly ILogger<CommandDispatcher> _logger;
        private readonly IServiceProvider _serviceProvider;

        public CommandDispatcher(IServiceProvider serviceProvider, ILogger<CommandDispatcher> logger)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
        }

        public async Task<TResult> SendAsync<TResult>(ICommand<TResult> command)
        {
            try
            {
                _logger.LogTrace($"Dispatching command: '{command}'.");

                if (command is null)
                {
                    _logger.LogWarning("Command is null. Skipping call handler for the command.");
                    return default;
                }

                using var scope = _serviceProvider.CreateScope();
                var handlerType = typeof(ICommandHandler<,>).MakeGenericType(command.GetType(), typeof(TResult));
;               var handler = scope.ServiceProvider.GetRequiredService(handlerType);

                var handleMethod = handlerType.GetMethod(nameof(ICommandHandler<ICommand<TResult>, TResult>.HandleAsync));
                if (handleMethod is null)
                {
                    _logger.LogWarning($"Cannot get command handler method from handler type: '{handlerType}'.");
                    return default;
                }

                var result = handleMethod.Invoke(handler, new[] {command});
                return await (result as Task<TResult>);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{ex.Message}; Command: '{command}'.");
                throw;
            }
        }
    }
}