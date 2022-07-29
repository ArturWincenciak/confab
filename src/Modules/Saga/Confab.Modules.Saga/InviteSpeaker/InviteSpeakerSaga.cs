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

        public InviteSpeakerSaga(IModuleClient moduleClient, IMessageBroker messageBroker)
        {
            _moduleClient = moduleClient;
            _messageBroker = messageBroker;
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
            if (InvitedSpeakersStub.InvitedSpeaker.TryGetValue(email, out var fullName))
            {
                Data.Email = email;
                Data.FullName = fullName;

                //todo:aw: consider use here message broker
                await _moduleClient.SendAsync<Null>("speakers/create",
                    new SpeakerDto(userId, email, fullName, "Lorem ipsum."));

                return;
            }

            await CompensateAsync(message, context);
        }

        public async Task HandleAsync(SignedIn message, ISagaContext context)
        {
            if (Data.SpeakerCreated)
            {
                await _messageBroker.PublishAsync(new SendWelcomeMessage(Data.Email, Data.FullName));
                await CompensateAsync(message, context);
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