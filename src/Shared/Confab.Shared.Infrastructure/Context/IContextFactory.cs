using Confab.Shared.Abstractions.Contexts;

namespace Confab.Shared.Infrastructure.Context
{
    internal interface IContextFactory
    {
        IContext Create();
    }
}