using System.Threading.Tasks;

namespace Confab.Shared.Abstractions.Commands
{
    public interface ICommandDispatcher
    {
        Task<TResult> SendAsync<TResult>(ICommand<TResult> command);
    }
}