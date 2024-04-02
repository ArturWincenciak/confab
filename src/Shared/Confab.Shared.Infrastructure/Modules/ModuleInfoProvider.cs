using System.Collections.Generic;

namespace Confab.Shared.Infrastructure.Modules;

internal class ModuleInfoProvider
{
    public IEnumerable<ModuleInfo> Modules { get; }

    public ModuleInfoProvider(IEnumerable<ModuleInfo> modules) =>
        Modules = modules;
}