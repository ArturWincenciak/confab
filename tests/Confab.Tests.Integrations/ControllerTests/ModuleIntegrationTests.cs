using System;
using Confab.Tests.Integrations.Builder;

namespace Confab.Tests.Integrations.ControllerTests;

public abstract class ModuleIntegrationTests : IDisposable
{
    protected readonly TestBuilder TestBuilder;

    public ModuleIntegrationTests() =>
        TestBuilder = new();

    public void Dispose()
    {
        TestBuilder.Dispose();
    }
}