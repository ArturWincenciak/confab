using System.Threading.Tasks;
using Chronicle;
using Confab.Modules.Saga.Messages;
using Confab.Shared.Abstractions.Events;

namespace Confab.Modules.Saga
{
    internal class Handler : IEventHandler<SpeakerCreated>, IEventHandler<SignedIn>, IEventHandler<SignedUp>
    {
        private readonly ISagaCoordinator _sagaCoordinator;

        public Handler(ISagaCoordinator sagaCoordinator)
        {
            _sagaCoordinator = sagaCoordinator;
        }

        public Task HandleAsync(SpeakerCreated @event)
        {
            return _sagaCoordinator.ProcessAsync(@event, SagaContext.Empty);
        }

        public Task HandleAsync(SignedIn @event)
        {
            return _sagaCoordinator.ProcessAsync(@event, SagaContext.Empty);
        }

        public Task HandleAsync(SignedUp @event)
        {
            return _sagaCoordinator.ProcessAsync(@event, SagaContext.Empty);
        }
    }
}