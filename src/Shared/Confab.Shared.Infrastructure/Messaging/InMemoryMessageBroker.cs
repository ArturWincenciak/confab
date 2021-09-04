using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Confab.Shared.Abstractions.Messaging;
using Confab.Shared.Abstractions.Modules;

namespace Confab.Shared.Infrastructure.Messaging
{
    internal class InMemoryMessageBroker : IMessageBroker
    {
        private readonly IModuleClient _moduleClient;

        public InMemoryMessageBroker(IModuleClient moduleClient)
        {
            _moduleClient = moduleClient;
        }

        public async Task PublishAsync(params IMessage[] messages)
        {
            if (messages is null)
                return;

            messages = messages.Where(x => x is not null).ToArray();

            var tasks = new List<Task>();
            foreach (var message in messages)
            {
                var task = _moduleClient.PublishAsync(message);
                tasks.Add(task);
            }

            await Task.WhenAll(tasks);
        }
    }
}