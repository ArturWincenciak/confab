using System.Threading.Tasks;

namespace Confab.Shared.Abstractions.Commands
{
    public interface ICommandHandler<in TCommand, TResult> where TCommand : class, ICommand
    {
        Task<TResult> HandleAsync(TCommand command);
    }
}