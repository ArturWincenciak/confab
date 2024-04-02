using System.Threading.Tasks;
using Chronicle;
using Confab.Modules.Saga.Messages;
using Confab.Shared.Abstractions.Events;

namespace Confab.Modules.Saga.Handlers;

internal class WelcomeSpeakerHandler :
    IEventHandler<SpeakerCreated>,
    IEventHandler<SignedIn>,
    IEventHandler<SignedUp>
{
    private readonly ISagaCoordinator _sagaCoordinator;

    public WelcomeSpeakerHandler(ISagaCoordinator sagaCoordinator) =>
        _sagaCoordinator = sagaCoordinator;

    public Task HandleAsync(SignedIn @event) =>
        _sagaCoordinator.ProcessAsync(@event, SagaContext.Empty);

    public Task HandleAsync(SignedUp @event) =>
        _sagaCoordinator.ProcessAsync(@event, SagaContext.Empty);

    public Task HandleAsync(SpeakerCreated @event) =>
        _sagaCoordinator.ProcessAsync(@event, SagaContext.Empty);
}