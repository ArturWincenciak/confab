using System;
using System.Threading.Tasks;

namespace Confab.Shared.Infrastructure.Modules
{
    public sealed class ModuleBroadcastRegistration
    {
        public ModuleBroadcastRegistration(Type registrationType, Func<object, Task> action)
        {
            RegistrationType = registrationType;
            Action = action;
        }

        public Type RegistrationType { get; }
        public Func<object, Task> Action { get; }
        public string Key => RegistrationType.Name;
    }
}