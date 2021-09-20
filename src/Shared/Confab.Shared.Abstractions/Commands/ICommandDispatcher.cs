using System.Threading.Tasks;

namespace Confab.Shared.Abstractions.Commands
{
    public interface ICommandDispatcher
    {
        Task SendAsync(ICommand command);
        Task<TResult> SendAsync<TResult>(ICommand<TResult> command) where TResult : class, ICommandResult;
    }
}