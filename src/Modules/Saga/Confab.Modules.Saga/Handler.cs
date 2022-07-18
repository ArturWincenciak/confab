using System.Threading.Tasks;
using Confab.Modules.Saga.Messages;
using Confab.Shared.Abstractions.Events;

namespace Confab.Modules.Saga
{
    internal class Handler : IEventHandler<SpeakerCreated>, IEventHandler<SignedIn>, IEventHandler<SignedUp>
    {
        public Task HandleAsync(SpeakerCreated @event)
        {
            throw new System.NotImplementedException();
        }

        public Task HandleAsync(SignedIn @event)
        {
            throw new System.NotImplementedException();
        }

        public Task HandleAsync(SignedUp @event)
        {
            throw new System.NotImplementedException();
        }
    }
}