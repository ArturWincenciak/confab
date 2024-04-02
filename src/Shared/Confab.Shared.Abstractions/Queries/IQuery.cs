namespace Confab.Shared.Abstractions.Queries;

public interface IRequestMessage
{
}

public interface IRequestMessage<TResult> : IRequestMessage where TResult : class, IResponseMessage
{
}