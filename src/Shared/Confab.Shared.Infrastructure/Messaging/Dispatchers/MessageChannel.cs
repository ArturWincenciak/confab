using System.Threading.Channels;
using Confab.Shared.Abstractions.Messaging;

namespace Confab.Shared.Infrastructure.Messaging.Dispatchers
{
    internal class MessageChannel : IMessageChannel
    {
        private readonly Channel<IMessage> _channel = Channel.CreateUnbounded<IMessage>();

        public ChannelReader<IMessage> Reader => _channel.Reader;
        public ChannelWriter<IMessage> Writer => _channel.Writer;
    }
}