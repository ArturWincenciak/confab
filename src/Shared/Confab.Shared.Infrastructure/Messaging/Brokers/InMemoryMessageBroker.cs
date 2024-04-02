using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Confab.Shared.Abstractions.Messaging;
using Confab.Shared.Abstractions.Modules;
using Confab.Shared.Infrastructure.Messaging.Dispatchers;
using Microsoft.Extensions.Logging;

namespace Confab.Shared.Infrastructure.Messaging.Brokers;

internal class InMemoryMessageBroker : IMessageBroker
{
    private readonly IAsyncMessageDispatcher _asyncMessageDispatcher;
    private readonly ILogger<InMemoryMessageBroker> _logger;
    private readonly MessagingOptions _messagingOptions;
    private readonly IModuleClient _moduleClient;

    public InMemoryMessageBroker(IModuleClient moduleClient, ILogger<InMemoryMessageBroker> logger,
        IAsyncMessageDispatcher asyncMessageDispatcher, MessagingOptions messagingOptions)
    {
        _asyncMessageDispatcher = asyncMessageDispatcher;
        _logger = logger;
        _messagingOptions = messagingOptions;
        _moduleClient = moduleClient;
    }

    public async Task PublishAsync(params IMessage[] messages)
    {
        try
        {
            if (messages is null)
            {
                _logger.LogWarning("There is no messages to publish.");
                return;
            }

            messages = messages.Where(x => x is not null).ToArray();

            if (!messages.Any())
            {
                _logger.LogWarning("There is no any not null message to publish.");
                return;
            }

            if (_messagingOptions.UseBackgroundDispatcher)
                await PublishByBackgroundDispatcherAsync(messages);
            else
                await PublishByModuleClientAsync(messages);

            _logger.LogTrace(
                $"All message has been published '{string.Join(separator: ';', values: messages.AsEnumerable())}'.");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, message: $"{ex.Message}");
            throw;
        }
    }

    private async Task PublishByBackgroundDispatcherAsync(IMessage[] messages)
    {
        foreach (var message in messages)
        {
            try
            {
                _logger.LogTrace($"Publishing message by async dispatcher: '{message}'.");
                await _asyncMessageDispatcher.PublishAsync(message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, message: $"{ex.Message}; Message: '{message}'.");
                throw;
            }
        }
    }

    private async Task PublishByModuleClientAsync(IMessage[] messages)
    {
        var tasks = new List<Task>();
        foreach (var message in messages)
        {
            try
            {
                _logger.LogTrace($"Publishing message by module client: '{message}'.");
                var task = _moduleClient.PublishAsync(message);
                tasks.Add(task);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, message: $"{ex.Message}; Message: '{message}'.");
                throw;
            }
        }

        await Task.WhenAll(tasks);
    }
}