using System.Collections.Generic;
using System.Threading.Tasks;
using Chronicle;
using Confab.Modules.Saga.Messages;

namespace Confab.Modules.Saga.InviteSpeaker
{
    internal class InviteSpeakerSaga : Saga<InviteSpeakerSaga.SagaData>,
        ISagaStartAction<SignedUp>, ISagaAction<SpeakerCreated>, ISagaAction<SignedIn>
    {
        public Task HandleAsync(SignedUp message, ISagaContext context)
        {
            var (userId, email) = message;
            if (Data.InvitedSpeaker.TryGetValue(email, out var name))
            {
                Data.Email = email;
                Data.FullName = name;
            }

            return Task.CompletedTask;
        }

        public Task HandleAsync(SpeakerCreated message, ISagaContext context)
        {
            throw new System.NotImplementedException();
        }

        public Task HandleAsync(SignedIn message, ISagaContext context)
        {
            throw new System.NotImplementedException();
        }

        public Task CompensateAsync(SignedUp message, ISagaContext context)
        {
            return Task.CompletedTask;
        }

        public Task CompensateAsync(SpeakerCreated message, ISagaContext context)
        {
            return Task.CompletedTask;
        }

        public Task CompensateAsync(SignedIn message, ISagaContext context)
        {
            return Task.CompletedTask;
        }

        internal class SagaData
        {
            public string Email { get; set; }
            public string FullName { get; set; }
            public bool SpeakerCreated { get; set; }

            public readonly Dictionary<string, string> InvitedSpeaker = new()
            {
                {"testspeaker1@confab.io", "John Smith"},
                {"testspeaker2@confab.io", "Mark Sim"}
            };
        }
    }
}