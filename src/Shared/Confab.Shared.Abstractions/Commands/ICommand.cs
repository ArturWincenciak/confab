using Confab.Shared.Abstractions.Messaging;

namespace Confab.Shared.Abstractions.Commands;

public interface ICommand : IMessage
{
}

public interface ICommand<TResult> : ICommand where TResult : class, ICommandResult
{
}