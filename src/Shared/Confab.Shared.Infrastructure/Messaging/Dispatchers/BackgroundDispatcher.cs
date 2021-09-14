using System;
using System.Threading;
using System.Threading.Tasks;
using Confab.Shared.Abstractions.Modules;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Confab.Shared.Infrastructure.Messaging.Dispatchers
{
    internal sealed class BackgroundDispatcher : BackgroundService
    {
        private readonly ILogger<BackgroundDispatcher> _logger;
        private readonly IMessageChannel _messageChannel;
        private readonly IModuleClient _moduleClient;

        public BackgroundDispatcher(IMessageChannel messageChannel, IModuleClient moduleClient,
            ILogger<BackgroundDispatcher> logger)
        {
            _logger = logger;
            _messageChannel = messageChannel;
            _moduleClient = moduleClient;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            try
            {
                _logger.LogInformation("Running the background dispatcher...");

                await foreach (var message in _messageChannel.Reader.ReadAllAsync(stoppingToken))
                    try
                    {
                        _logger.LogTrace($"Publishing new oncoming message (using module client): '{message}'.");
                        await _moduleClient.PublishAsync(message);
                        _logger.LogTrace($"Message has been published (using module client): '{message}'.");
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, $"{ex.Message}; Message: '{message}'.");
                    }

                _logger.LogInformation("Finished running the background dispatcher.");
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex, $"{ex.Message}");
                throw;
            }
        }
    }
}