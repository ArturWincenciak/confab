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

        public async Task SendAsync<TCommand>(TCommand command) where TCommand : class, ICommand
        {
            try
            {
                if (command is null)
                {
                    _logger.LogWarning("Command is null. Skipping call handler for the command.");
                    return;
                }

                using var scope = _serviceProvider.CreateScope();
                var handler = _serviceProvider.GetRequiredService<ICommandHandler<TCommand>>();
                await handler.HandleAsync(command);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{ex.Message}; Command: '{command}'.");
                throw;
            }
        }
    }
}