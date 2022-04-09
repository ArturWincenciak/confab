using System;
using Confab.Modules.Tests.Integrations.Builder;

namespace Confab.Modules.Tests.Integrations.ControllerTests
{
    public abstract class ModuleTests : IDisposable
    {
        protected readonly TestBuilder TestBuilder;

        public ModuleTests()
        {
            TestBuilder = new TestBuilder();
        }

        public void Dispose()
        {
            TestBuilder.Dispose();
        }
    }
}