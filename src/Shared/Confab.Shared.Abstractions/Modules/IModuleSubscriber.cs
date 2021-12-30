using System;
using Confab.Shared.Abstractions.Queries;

namespace Confab.Shared.Abstractions.Modules
{
    public interface IModuleSubscriber
    {
        public IModuleSubscriber MapRequest<TRequest, TResponse>(string path)
            where TRequest : class, IQuery<TResponse>, IModuleRequest
            where TResponse : class, IQueryResult, IModuleResponse;
    }
}