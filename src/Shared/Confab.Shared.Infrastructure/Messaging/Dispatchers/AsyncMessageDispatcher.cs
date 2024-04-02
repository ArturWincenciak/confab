using System.Threading.Tasks;
using Confab.Shared.Abstractions.Messaging;
using Microsoft.Extensions.Logging;

namespace Confab.Shared.Infrastructure.Messaging.Dispatchers;

internal sealed class AsyncMessageDispatcher : IAsyncMessageDispatcher
{
    private readonly ILogger<AsyncMessageDispatcher> _logger;
    private readonly IMessageChannel _messageChannel;

    public AsyncMessageDispatcher(IMessageChannel messageChannel, ILogger<AsyncMessageDispatcher> logger)
    {
        _messageChannel = messageChannel;
        _logger = logger;
    }

    public async Task PublishAsync<TMessage>(TMessage message) where TMessage : class, IMessage
    {
        _logger.LogTrace($"Write to channel message: '{message}'.");
        await _messageChannel.Writer.WriteAsync(message);
        _logger.LogTrace($"Message hes been written to channel: '{message}'.");
    }
}