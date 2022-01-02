using System;
using System.Threading.Tasks;
using Confab.Shared.Abstractions.Commands;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Confab.Shared.Infrastructure.Postgres.Decorators
{
    //[Decorator]
    public class TransactionalCommandHandlerDecorator<TCommand, TTransactional> : ICommandHandler<TCommand>
        where TCommand : class, ICommand
        where TTransactional : IUnitOfWork
    {
        private readonly ICommandHandler<TCommand> _handler;
        private readonly IServiceProvider _serviceProvider;
        //private readonly PostgresUnitOfWorkTypeRegistry _unitOfWorkTypeRegistry;

        public TransactionalCommandHandlerDecorator(ICommandHandler<TCommand> handler, IServiceProvider serviceProvider
            //, PostgresUnitOfWorkTypeRegistry unitOfWorkTypeRegistry
            )
        {
            _handler = handler;
            _serviceProvider = serviceProvider;
            //_unitOfWorkTypeRegistry = unitOfWorkTypeRegistry;
        }

        public Task HandleAsync(TCommand command)
        {
            //var unitOfWorkType = _unitOfWorkTypeRegistry.Resolve<T>();
            //if(unitOfWorkType is null)
            //    return _handler.HandleAsync(command);

            //var unitOfWorkService = (IUnitOfWork) _serviceProvider.GetRequiredService(unitOfWorkType);
            var unitOfWorkService = _serviceProvider.GetRequiredService<TTransactional>();
            return unitOfWorkService.ExecuteAsync(() => _handler.HandleAsync(command));
        }
    }

    //todo: add also implementation command handlers returns result, consider also impl for integration events
}