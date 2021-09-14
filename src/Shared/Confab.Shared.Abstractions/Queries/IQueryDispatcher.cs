using System.Threading.Tasks;

namespace Confab.Shared.Abstractions.Queries
{
    public interface IQueryDispatcher
    {
        Task<TResult> QueryAsync<TResult>(IQuery<TResult> query) where TResult : class, IQueryResult;
    }
}