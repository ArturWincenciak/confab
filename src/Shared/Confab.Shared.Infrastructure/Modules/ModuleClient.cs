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

        public async Task<TResult> SendAsync<TResult>(string path, object request) where TResult : class
        {
            try
            {
                _logger.LogTrace($"Sending request to '{path}' with value '{request}'.");

                var registration = _moduleRegistry.GetRequestRegistration(path);
                if (registration is null)
                    throw new InvalidOperationException($"No action has been defined for path: '{path}'.");

                var receiverRequest = TranslateType(request, registration.RequestType);
                var result = await registration.Action(receiverRequest);
                var translatedResult = TranslateType<TResult>(result);

                _logger.LogTrace($"Request response result for '{path}' got '{translatedResult}' value.");

                return translatedResult;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{ex.Message}");
                throw;
            }
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

        private TExpectedType TranslateType<TExpectedType>(object value) where TExpectedType : class
        {
            if (value is null)
                return null;

            if (value.GetType() == typeof(TExpectedType))
                return (TExpectedType) value;

            var serialized = _moduleSerializer.Serialize(value);
            var deserialized = _moduleSerializer.Deserialize<TExpectedType>(serialized);
            return deserialized;
        }

        private object TranslateType(object value, Type expectedType)
        {
            if (value.GetType() == expectedType)
                return value;

            var serialized = _moduleSerializer.Serialize(value);
            var deserialized = _moduleSerializer.Deserialize(serialized, expectedType);
            return deserialized;
        }
    }
}