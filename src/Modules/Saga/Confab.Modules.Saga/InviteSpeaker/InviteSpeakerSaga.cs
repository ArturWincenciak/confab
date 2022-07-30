using System;
using System.Threading.Tasks;
using Chronicle;
using Confab.Modules.Saga.Messages;
using Confab.Modules.Saga.Services;
using Confab.Shared.Abstractions.Messaging;
using Confab.Shared.Abstractions.Modules;
using Confab.Shared.Abstractions.Queries;

namespace Confab.Modules.Saga.InviteSpeaker
{
    internal class InviteSpeakerSaga : Saga<InviteSpeakerSaga.SagaData>,
        ISagaStartAction<SignedUp>, ISagaAction<SpeakerCreated>, ISagaAction<SignedIn>
    {
        private readonly IModuleClient _moduleClient;
        private readonly IMessageBroker _messageBroker;
        private readonly InvitationSpeakersStub _invitedSpeakers;

        public InviteSpeakerSaga(IModuleClient moduleClient, IMessageBroker messageBroker,
            InvitationSpeakersStub invitedSpeakers)
        {
            _moduleClient = moduleClient;
            _messageBroker = messageBroker;
            _invitedSpeakers = invitedSpeakers;
        }

        public override SagaId ResolveId(object message, ISagaContext context) =>
            message switch
            {
                SignedUp signedUp => signedUp.UserId.ToString(),
                SignedIn signedIn => signedIn.UserId.ToString(),
                SpeakerCreated created => created.Id.ToString(),
                _ => throw new ArgumentOutOfRangeException(message.GetType().FullName)
            };

        public async Task HandleAsync(SignedUp message, ISagaContext context)
        {
            var (userId, email) = message;
            if (_invitedSpeakers.IsInvited(email))
            {
                var fullName = _invitedSpeakers.FullName(email);

                Data.Email = email;
                Data.FullName = fullName;

                var bio = _invitedSpeakers.SpeakerBio(email);

                await _moduleClient.SendAsync<Null>("speakers/create",
                    new SpeakerDto(userId, email, Data.FullName, bio));

                return;
            }

            await CompleteAsync();
        }

        public async Task HandleAsync(SignedIn message, ISagaContext context)
        {
            if (Data.SpeakerCreated)
            {
                await _messageBroker.PublishAsync(new SendWelcomeMessage(Data.Email, Data.FullName));
                await CompleteAsync();
            }
        }

        public Task HandleAsync(SpeakerCreated message, ISagaContext context)
        {
            Data.SpeakerCreated = true;
            return Task.CompletedTask;
        }

        public Task CompensateAsync(SignedUp message, ISagaContext context) =>
            Task.CompletedTask;

        public Task CompensateAsync(SignedIn message, ISagaContext context) =>
            Task.CompletedTask;

        public Task CompensateAsync(SpeakerCreated message, ISagaContext context) =>
            Task.CompletedTask;

        internal class SagaData
        {
            public string Email { get; set; }
            public string FullName { get; set; }
            public bool SpeakerCreated { get; set; }
        }

        private record SpeakerDto(Guid UserId, string Email, string FullName, string Bio) : IModuleRequest;
    }
}