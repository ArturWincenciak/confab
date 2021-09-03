using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Confab.Shared.Abstractions.Modules;

namespace Confab.Shared.Infrastructure.Modules
{
    internal class ModuleClient : IModuleClient
    {
        private readonly IModuleRegistry _moduleRegistry;
        private readonly IModuleSerializer _moduleSerializer;

        public ModuleClient(IModuleRegistry moduleRegistry, IModuleSerializer moduleSerializer)
        {
            _moduleRegistry = moduleRegistry;
            _moduleSerializer = moduleSerializer;
        }

        public async Task PublishAsync(object message)
        {
            var key = message.GetType().Name;
            var registrations = _moduleRegistry.GetBroadcastRegistrations(key);

            var tasks = new List<Task>();
            foreach (var registration in registrations)
            {
                var receiverMessage = TranslateType(message, registration.RegistrationType);
                var task = registration.Action(receiverMessage);
                tasks.Add(task);
            }

            await Task.WhenAll(tasks);
        }

        private object TranslateType(object fromValue, Type toType)
        {
            var serialized = _moduleSerializer.Serialize(fromValue);
            var deserialized = _moduleSerializer.Deserialize(serialized, toType);
            return deserialized;
        }
    }
}