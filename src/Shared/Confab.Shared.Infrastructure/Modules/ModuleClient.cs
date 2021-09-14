using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Confab.Shared.Abstractions.Modules;
using Microsoft.Extensions.Logging;

namespace Confab.Shared.Infrastructure.Modules
{
    internal class ModuleClient : IModuleClient
    {
        private readonly ILogger<ModuleClient> _logger;
        private readonly IModuleRegistry _moduleRegistry;
        private readonly IModuleSerializer _moduleSerializer;

        public ModuleClient(IModuleRegistry moduleRegistry, IModuleSerializer moduleSerializer,
            ILogger<ModuleClient> logger)
        {
            _moduleRegistry = moduleRegistry;
            _moduleSerializer = moduleSerializer;
            _logger = logger;
        }

        public async Task PublishAsync(object message)
        {
            try
            {
                _logger.LogTrace($"Publishing message: '{message}'.");

                var key = message.GetType().Name;
                var registrations = _moduleRegistry.GetBroadcastRegistrations(key);

                var tasks = new List<Task>();
                foreach (var registration in registrations)
                    try
                    {
                        _logger.LogTrace($"Invoking: '{registration} - {message}'.");
                        var receiverMessage = TranslateType(message, registration.RegistrationType);
                        var task = registration.Action(receiverMessage);
                        tasks.Add(task);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, $"{ex.Message}; Registration: '{registration} - '{message}'.");
                        throw;
                    }

                await Task.WhenAll(tasks);
                _logger.LogTrace($"Message has been published: '{message}'.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{ex.Message}");
                throw;
            }
        }

        private object TranslateType(object fromValue, Type toType)
        {
            if (fromValue.GetType() == toType)
                return fromValue;

            var serialized = _moduleSerializer.Serialize(fromValue);
            var deserialized = _moduleSerializer.Deserialize(serialized, toType);
            return deserialized;
        }
    }
}