using System;
using Confab.Shared.Abstractions.Contexts;
using Microsoft.AspNetCore.Http;

namespace Confab.Shared.Infrastructure.Contexts;

internal class Context : IContext
{
    public static IContext Empty => new Context();
    public string RequestId { get; } = $"{Guid.NewGuid():N}";
    public string TraceId { get; }
    public IIdentityContext Identity { get; }

    internal Context()
    {
    }

    internal Context(string tracedId, IIdentityContext identity)
    {
        TraceId = tracedId;
        Identity = identity;
    }

    public Context(HttpContext context)
        : this(context.TraceIdentifier, identity: new IdentityContext(context.User))
    {
    }
}