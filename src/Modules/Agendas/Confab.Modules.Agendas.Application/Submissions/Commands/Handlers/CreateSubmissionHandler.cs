using System.Linq;
using System.Threading.Tasks;
using Confab.Modules.Agendas.Application.Submissions.Exceptions;
using Confab.Modules.Agendas.Application.Submissions.Repositories;
using Confab.Modules.Agendas.Application.Submissions.Services;
using Confab.Modules.Agendas.Domain.Submissions.Entities;
using Confab.Shared.Abstractions.Commands;
using Confab.Shared.Abstractions.Kernel;
using Confab.Shared.Abstractions.Kernel.Types;
using Confab.Shared.Abstractions.Kernel.Types.Base;
using Confab.Shared.Abstractions.Messaging;

namespace Confab.Modules.Agendas.Application.Submissions.Commands.Handlers
{
    internal sealed class CreateSubmissionHandler : ICommandHandler<CreateSubmission, CreateSubmission.SubmissionId>
    {
        private readonly IDomainEventDispatcher _domainEventDispatcher;
        private readonly IMessageBroker _messageBroker;
        private readonly IMessageMapper _messageMapper;
        private readonly ISpeakerRepository _speakerRepository;
        private readonly ISubmissionRepository _submissionRepository;

        public CreateSubmissionHandler(ISubmissionRepository submissionRepository, ISpeakerRepository speakerRepository,
            IDomainEventDispatcher domainEventDispatcher, IMessageBroker messageBroker, IMessageMapper messageMapper)
        {
            _submissionRepository = submissionRepository;
            _speakerRepository = speakerRepository;
            _domainEventDispatcher = domainEventDispatcher;
            _messageBroker = messageBroker;
            _messageMapper = messageMapper;
        }

        public async Task<CreateSubmission.SubmissionId> HandleAsync(CreateSubmission cmd)
        {
            var speakerIds = cmd.SpeakerIds.Select(x => new AggregateId(x));
            var speakers = await _speakerRepository.BrowseAsync(speakerIds);

            var allSpeakerExists = speakers.Count() == cmd.SpeakerIds.Count();
            if (!allSpeakerExists)
                throw new NotAllSpeakersExistsException(cmd.SpeakerIds, speakers.Select(x => x.Id.Value));

            var submission = Submission.Create(cmd.ConferenceId, cmd.Title, cmd.Title, cmd.Level, cmd.Tags, speakers);
            await _submissionRepository.AddAsync(submission);

            await _domainEventDispatcher.SendAsync(submission.Events.ToArray());
            var integrationEvents = _messageMapper.Map(submission.Events);
            await _messageBroker.PublishAsync(integrationEvents.ToArray());

            return new CreateSubmission.SubmissionId(submission.Id);
        }
    }
}