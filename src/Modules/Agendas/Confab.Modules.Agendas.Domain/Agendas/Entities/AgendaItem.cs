using System;
using System.Collections.Generic;
using System.Linq;
using Confab.Modules.Agendas.Domain.Agendas.Exceptions;
using Confab.Modules.Agendas.Domain.Submissions.Entities;
using Confab.Modules.Agendas.Domain.Submissions.Exceptions;
using Confab.Shared.Abstractions.Kernel.Types;
using Confab.Shared.Abstractions.Kernel.Types.Base;

namespace Confab.Modules.Agendas.Domain.Agendas.Entities
{
    public class AgendaItem : AggregateRoot
    {
        public ConferenceId ConferenceId { get; private set; }
        public string Title { get; private set; }
        public string Description { get; private set; }
        public int Level { get; private set; }

        public IEnumerable<string> Tags { get; set; }

        public ICollection<Speaker> Speakers { get; private set; }

        public AgendaSlot AgendaSlot { get; }

        public static AgendaItem Create(AggregateId submissionId, ConferenceId conferenceId, string title,
            string description, int level, IEnumerable<string> tags, ICollection<Speaker> speakers)
        {
            return Build(() =>
            {
                var entity = new AgendaItem
                {
                    Id = submissionId,
                    ConferenceId = conferenceId
                };
                entity.ChangeTitle(title);
                entity.ChangeDescription(description);
                entity.ChangeLevel(level);
                entity.ChangeTags(tags);
                entity.ChangeSpeakers(speakers);
                return entity;
            });
        }

        private void ChangeSpeakers(ICollection<Speaker> speakers)
        {
            if (speakers is null || !speakers.Any())
                throw new MissingSubmissionSpeakersException(Id);

            Apply(() => Speakers = speakers);
        }

        private void ChangeTags(IEnumerable<string> tags)
        {
            if (tags is null || !tags.Any())
                throw new EmptyAgendaItemTagsException(Id);

            Apply(() => Tags = tags);
        }

        private void ChangeLevel(int level)
        {
            var isNotInRange = level is < 1 or > 6;
            if (isNotInRange)
                throw new InvalidSubmissionLevelException(Id);

            Apply(() => Level = level);
        }

        private void ChangeDescription(string description)
        {
            if (string.IsNullOrWhiteSpace(description))
                throw new EmptySubmissionDescriptionException(Id);

            Apply(() => Description = description);
        }

        private void ChangeTitle(string title)
        {
            if (string.IsNullOrWhiteSpace(title))
                throw new EmptySubmissionTitleException(Id);

            Apply(() => Title = title);
        }
    }
}