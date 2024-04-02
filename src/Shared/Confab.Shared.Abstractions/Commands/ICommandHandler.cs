using System.Threading.Tasks;

namespace Confab.Shared.Abstractions.Commands;

public interface ICommandHandler<in TCommand> where TCommand : class, ICommand
{
    Task HandleAsync(TCommand command);
}

public interface ICommandHandler<in TCommand, TResult>
    where TCommand : class, ICommand
    where TResult : class, ICommandResult
{
    Task<TResult> HandleAsync(TCommand command);
}