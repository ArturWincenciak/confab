using System.Collections.Generic;

namespace Confab.Shared.Infrastructure.Modules
{
    internal class ModuleInfoProvider
    {
        public ModuleInfoProvider(IEnumerable<ModuleInfo> modules)
        {
            Modules = modules;
        }

        public IEnumerable<ModuleInfo> Modules { get; }
    }
}