using System.Threading.Tasks;

namespace Confab.Shared.Abstractions.Queries
{
    public interface IQueryHandler<in TQuery, TResult>
        where TQuery : class, IRequestMessage<TResult>
        where TResult : class, IResponseMessage
    {
        Task<TResult> HandleAsync(TQuery query);
    }
}