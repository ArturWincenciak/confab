using System;
using System.Collections.Generic;
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
        public IEnumerable<string> Tag { get; set; }
        public ICollection<Speaker> Speakers { get; private set; }
        public AgendaSlot AgendaSlot { get; }

        public static AgendaItem Create(ConferenceId conferenceId, string title, string description, int level,
            IEnumerable<string> tags, ICollection<Speaker> speakers)
        {
            var entity = new AgendaItem
            {
                Id = Guid.NewGuid(),
                ConferenceId = conferenceId
            };
            entity.ChangeTitle(title);
            entity.ChangeDescription(description);
            entity.ChangeLevel(level);
            entity.ChangeTags(tags);
            entity.ChangeSpeakers(speakers);
            return entity;
        }

        private void ChangeSpeakers(ICollection<Speaker> speakers)
        {

        }

        private void ChangeTags(IEnumerable<string> tags)
        {
            throw new NotImplementedException();
        }

        private void ChangeLevel(int level)
        {
            throw new NotImplementedException();
        }

        private void ChangeDescription(string description)
        {
            throw new NotImplementedException();
        }

        private void ChangeTitle(string title)
        {
            if (string.IsNullOrEmpty(title))
                throw new EmptySubmissionTitleException(Id);
        }
    }
}