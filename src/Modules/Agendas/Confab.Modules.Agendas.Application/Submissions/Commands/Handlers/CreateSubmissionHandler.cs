using System;
using System.Linq;
using System.Threading.Tasks;
using Confab.Modules.Agendas.Application.Submissions.Exceptions;
using Confab.Modules.Agendas.Domain.Submissions.Entities;
using Confab.Modules.Agendas.Domain.Submissions.Repositories;
using Confab.Shared.Abstractions.Commands;
using Confab.Shared.Abstractions.Kernel.Types;

namespace Confab.Modules.Agendas.Application.Submissions.Commands.Handlers
{
    internal sealed class CreateSubmissionHandler : ICommandHandler<CreateSubmission, CreateSubmission.SubmissionId>
    {
        private readonly ISpeakerRepository _speakerRepository;
        private readonly ISubmissionRepository _submissionRepository;

        public CreateSubmissionHandler(ISubmissionRepository submissionRepository, ISpeakerRepository speakerRepository)
        {
            _submissionRepository = submissionRepository;
            _speakerRepository = speakerRepository;
        }

        public async Task<CreateSubmission.SubmissionId> HandleAsync(CreateSubmission command)
        {
            var speakerIds = command.SpeakerIds.Select(x => new AggregateId(x));
            var speakers = await _speakerRepository.BrowseAsync(speakerIds);

            var allSpeakerExists = speakers.Count() == command.SpeakerIds.Count();
            if (!allSpeakerExists)
                throw new NotAllSpeakersExistsException(command.SpeakerIds, speakers.Select(x => x.Id.Value));

            var submission = Submission.Create(command.ConferenceId, command.Title, command.Title, command.Level,
                command.Tags, speakers);

            await _submissionRepository.AddAsync(submission);

            return new CreateSubmission.SubmissionId(submission.Id);
        }
    }
}