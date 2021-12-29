using System;
using System.Threading.Tasks;
using Confab.Shared.Abstractions.Queries;

namespace Confab.Shared.Abstractions.Modules
{
    public interface IModuleSubscriber
    {
        public IModuleSubscriber Subscribe<TRequest, TResponse>(string path)
            where TRequest : class, IQuery<TResponse>, IModuleRequest
            where TResponse : class, IQueryResult, IModuleResponse;

        //todo: remove it and always use simpler version (above)
        IModuleSubscriber Subscribe<TRequest, TResponse>(string path,
            Func<TRequest, IServiceProvider, Task<TResponse>> action)
            where TRequest : class, IQuery<TResponse>, IModuleRequest
            where TResponse : class, IQueryResult, IModuleResponse;
    }
}