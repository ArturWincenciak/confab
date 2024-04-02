using Confab.Shared.Abstractions.Queries;

namespace Confab.Shared.Abstractions.Modules;

public interface IModuleSubscriber
{
    public IModuleSubscriber MapRequest<TRequest, TResponse>(string path)
        where TRequest : class, IRequestMessage<TResponse>, IModuleRequest
        where TResponse : class, IResponseMessage, IModuleResponse;
}

public record Null : IResponseMessage,
    IModuleResponse;