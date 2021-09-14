using Confab.Shared.Abstractions.Messaging;

namespace Confab.Shared.Abstractions.Queries
{
    public interface IQuery : IMessage
    {
    }

    public interface IQuery<TResult> : IQuery where TResult : class, IQueryResult
    {
    }

    public interface IQueryResult
    {
    }
}