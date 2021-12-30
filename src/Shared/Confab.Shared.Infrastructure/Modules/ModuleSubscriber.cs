using System;
using System.Threading.Tasks;
using Confab.Shared.Abstractions.Modules;
using Confab.Shared.Abstractions.Queries;
using Microsoft.Extensions.DependencyInjection;

namespace Confab.Shared.Infrastructure.Modules
{
    internal sealed class ModuleSubscriber : IModuleSubscriber
    {
        private readonly IModuleRegistry _registry;
        private readonly IServiceProvider _serviceProvider;

        public ModuleSubscriber(IModuleRegistry registry, IServiceProvider serviceProvider)
        {
            _registry = registry;
            _serviceProvider = serviceProvider;
        }

        public IModuleSubscriber MapRequest<TRequest, TResponse>(string path)
            where TRequest : class, IQuery<TResponse>, IModuleRequest
            where TResponse : class, IQueryResult, IModuleResponse
        {
            return Subscribe<TRequest, TResponse>(path,
                (request, sp) => sp.GetRequiredService<IQueryDispatcher>().QueryAsync(request));
        }

        private IModuleSubscriber Subscribe<TRequest, TResponse>(string path,
            Func<TRequest, IServiceProvider, Task<TResponse>> action)
            where TRequest : class, IQuery<TResponse>, IModuleRequest
            where TResponse : class, IQueryResult, IModuleResponse
        {
            _registry.AddRequestAction(path, typeof(TRequest), typeof(TResponse),
                async request => await action((TRequest) request, _serviceProvider));

            return this;
        }
    }
}