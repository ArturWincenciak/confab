using System.Threading.Tasks;

namespace Confab.Shared.Abstractions.Queries
{
    public interface IQueryDispatcher
    {
        Task<TResult> QueryAsync<TResult>(IRequestMessage<TResult> query) where TResult : class, IResponseMessage;
    }
}