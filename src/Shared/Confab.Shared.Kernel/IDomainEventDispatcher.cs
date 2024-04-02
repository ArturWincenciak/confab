using System.Threading.Tasks;

namespace Confab.Shared.Kernel;

public interface IDomainEventDispatcher
{
    Task SendAsync(params IDomainEvent[] events);
}