namespace Confab.Shared.Abstractions.Queries
{
    public interface IQuery
    {
    }

    public interface IQuery<TResult> : IQuery where TResult : class, IQueryResult
    {
    }
}