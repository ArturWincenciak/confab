using System.Threading.Tasks;
using Confab.Shared.Abstractions.Queries;

namespace Confab.Shared.Abstractions.Modules;

public interface IModuleClient
{
    Task<TResult> SendAsync<TResult>(string path, IModuleRequest request) where TResult : class, IModuleResponse;
    Task PublishAsync(object message);
}