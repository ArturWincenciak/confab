using System.Threading.Tasks;

namespace Confab.Shared.Abstractions.Queries
{
    public interface IQueryHandler<in TQuery, TResult>
        where TQuery : class, IQuery<TResult>
        where TResult : class, IQueryResult
    {
        Task<TResult> HandleAsync(TQuery query);
    }
}