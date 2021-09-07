using System.Threading.Channels;
using Confab.Shared.Abstractions.Messaging;

namespace Confab.Shared.Infrastructure.Messaging.Dispatchers
{
    internal interface IMessageChannel
    {
        public ChannelReader<IMessage> Reader { get; }
        public ChannelWriter<IMessage> Writer { get; }
    }
}