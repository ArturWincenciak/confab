using System;
using System.Collections.Generic;
using System.Linq;
using Confab.Modules.Agendas.Domain.Submissions.Constants;
using Confab.Modules.Agendas.Domain.Submissions.Events;
using Confab.Modules.Agendas.Domain.Submissions.Exceptions;
using Confab.Shared.Kernel.Types;
using Confab.Shared.Kernel.Types.Base;

namespace Confab.Modules.Agendas.Domain.Submissions.Entities
{
    public sealed class Submission : AggregateRoot
    {
        public ConferenceId ConferenceId { get; private set; }
        public string Title { get; private set; }
        public string Description { get; private set; }
        public int Level { get; private set; }
        public string Status { get; private set; }
        public IEnumerable<string> Tags { get; private set; }
        public ICollection<Speaker> Speakers { get; private set; }

        public static Submission Create(ConferenceId conferenceId, string title, string description,
            int level, IEnumerable<string> tags, IEnumerable<Speaker> speakers)
        {
            var id = Guid.NewGuid();
            return Create(id, conferenceId, title, description, level, tags, speakers);
        }

        private static Submission Create(AggregateId id, ConferenceId conferenceId, string title, string description,
            int level, IEnumerable<string> tags, IEnumerable<Speaker> speakers)
        {
            var submission = new Submission
            {
                Id = id,
                ConferenceId = conferenceId
            };
            submission.ChangeTitle(title);
            submission.ChangeDescription(description);
            submission.ChangeLevel(level);
            submission.Status = SubmissionStatus.Pending;
            submission.Tags = tags;
            submission.ChangeSpeakers(speakers);

            submission.ClearEvents();
            submission.AddEvent(new SubmissionAdded(submission));

            return submission;
        }

        public void ChangeTitle(string title)
        {
            if (string.IsNullOrWhiteSpace(title))
                throw new EmptySubmissionTitleException(Id);

            Title = title;
            IncrementVersion();
        }

        public void ChangeDescription(string description)
        {
            if (string.IsNullOrWhiteSpace(description))
                throw new EmptySubmissionDescriptionException(Id);

            Description = description;
            IncrementVersion();
        }

        public void ChangeLevel(int level)
        {
            var isNotInRange = level is < 1 or > 6;
            if (isNotInRange)
                throw new InvalidSubmissionLevelException(Id);

            Level = level;
            IncrementVersion();
        }

        public void ChangeSpeakers(IEnumerable<Speaker> speakers)
        {
            if (speakers is null || !speakers.Any())
                throw new MissingSubmissionSpeakersException(Id);

            Speakers = speakers.ToList();
            IncrementVersion();
        }

        public void Approve()
        {
            if (Status is SubmissionStatus.Rejected)
                throw new InvalidSubmissionStatusException(Id, SubmissionStatus.Approved, Status);

            Status = SubmissionStatus.Approved;
            AddEvent(new SubmissionApproved(this));
        }

        public void Reject()
        {
            if (Status is SubmissionStatus.Approved)
                throw new InvalidSubmissionStatusException(Id, SubmissionStatus.Rejected, Status);

            Status = SubmissionStatus.Rejected;
            AddEvent(new SubmissionRejected(Id));
        }
    }
}