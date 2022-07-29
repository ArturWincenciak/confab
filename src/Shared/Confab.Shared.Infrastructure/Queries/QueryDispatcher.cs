using System;
using System.Threading.Tasks;
using Confab.Shared.Abstractions.Queries;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Confab.Shared.Infrastructure.Queries
{
    internal sealed class QueryDispatcher : IQueryDispatcher
    {
        private readonly ILogger<QueryDispatcher> _logger;
        private readonly IServiceProvider _serviceProvider;

        public QueryDispatcher(IServiceProvider serviceProvider, ILogger<QueryDispatcher> logger)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
        }

        public async Task<TResult> QueryAsync<TResult>(IRequestMessage<TResult> query) where TResult : class, IResponseMessage
        {
            try
            {
                _logger.LogTrace($"Dispatching query: '{query}'.");
                using var scope = _serviceProvider.CreateScope();
                var handlerType = typeof(IQueryHandler<,>).MakeGenericType(query.GetType(), typeof(TResult));
                var handler = scope.ServiceProvider.GetRequiredService(handlerType);
                var handleMethod = handlerType.GetMethod(nameof(IQueryHandler<IRequestMessage<TResult>, TResult>.HandleAsync));
                var result = handleMethod.Invoke(handler, new[] {query});
                return await (result as Task<TResult>);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{ex.Message}; Query: '{query}'.");
                throw;
            }
        }
    }
}