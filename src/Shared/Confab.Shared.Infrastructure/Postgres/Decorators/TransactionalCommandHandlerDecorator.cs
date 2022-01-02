using System;
using System.Threading.Tasks;
using Confab.Shared.Abstractions.Commands;
using Microsoft.Extensions.DependencyInjection;

namespace Confab.Shared.Infrastructure.Postgres.Decorators
{
    [Decorator]
    internal class TransactionalCommandHandlerDecorator<T> : ICommandHandler<T> where T : class, ICommand
    {
        private readonly ICommandHandler<T> _handler;
        private readonly IServiceProvider _serviceProvider;
        private readonly PostgresUnitOfWorkTypeRegistry _unitOfWorkTypeRegistry;

        public TransactionalCommandHandlerDecorator(ICommandHandler<T> handler, IServiceProvider serviceProvider,
            PostgresUnitOfWorkTypeRegistry unitOfWorkTypeRegistry)
        {
            _handler = handler;
            _serviceProvider = serviceProvider;
            _unitOfWorkTypeRegistry = unitOfWorkTypeRegistry;
        }

        public Task HandleAsync(T command)
        {
            var unitOfWorkType = _unitOfWorkTypeRegistry.Resolve<T>();
            if(unitOfWorkType is null)
                return _handler.HandleAsync(command);

            var unitOfWorkService = (IUnitOfWork) _serviceProvider.GetRequiredService(unitOfWorkType);
            return unitOfWorkService.ExecuteAsync(() => _handler.HandleAsync(command));
        }
    }

    //todo: add also implementation command handlers returns result, consider also impl for integration events
}