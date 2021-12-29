using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Confab.Shared.Infrastructure.Modules
{
    public class ModuleRegistry : IModuleRegistry
    {
        private readonly List<ModuleBroadcastRegistration> _broadcastRegistrations = new();
        private readonly Dictionary<string, ModuleRequestRegistration> _requestRegistrations = new();

        public IEnumerable<ModuleBroadcastRegistration> GetBroadcastRegistrations(string key)
        {
            return _broadcastRegistrations.Where(x => x.Key == key);
        }

        public ModuleRequestRegistration GetRequestRegistration(string path)
        {
            return _requestRegistrations.TryGetValue(path, out var registration) ? registration : null;
        }

        public void AddBroadcastAction(Type requestType, Func<object, Task> action)
        {
            if (string.IsNullOrWhiteSpace(requestType.Namespace))
                throw new InvalidOperationException("Missing namespace.");

            var registration = new ModuleBroadcastRegistration(requestType, action);
            _broadcastRegistrations.Add(registration);
        }

        public void AddRequestAction(string path, Type requestType, Type responseType, Func<object, Task<object>> action)
        {
            if (path is null)
                throw new InvalidOperationException("Request path cannot be null.");

            var registration = new ModuleRequestRegistration(requestType, responseType, action);
            _requestRegistrations.Add(path, registration);
        }
    }
}