using System.Threading.Tasks;
using Confab.Shared.Abstractions.Commands;

namespace Confab.Shared.Infrastructure.Postgres.Decorators
{
    //[Decorator]
    public class TransactionalCommandHandlerDecorator<TCommand, TTransactional> : ICommandHandler<TCommand>
        where TCommand : class, ICommand
        where TTransactional : IUnitOfWork
    {
        private readonly ICommandHandler<TCommand> _handler;
        private readonly TTransactional _unitOfWorkService;

        public TransactionalCommandHandlerDecorator(ICommandHandler<TCommand> handler, TTransactional unitOfWorkService)
        {
            _handler = handler;
            _unitOfWorkService = unitOfWorkService;
        }

        public Task HandleAsync(TCommand command)
        {
            return _unitOfWorkService.ExecuteAsync(() => _handler.HandleAsync(command));
        }
    }

    //todo: add also implementation command handlers returns result, consider also impl for integration events
}