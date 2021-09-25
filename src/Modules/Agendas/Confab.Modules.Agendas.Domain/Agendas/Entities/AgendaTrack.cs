using System;
using System.Collections.Generic;
using Confab.Shared.Abstractions.Kernel.Types;
using Confab.Shared.Abstractions.Kernel.Types.Base;

namespace Confab.Modules.Agendas.Domain.Agendas.Entities
{
    public class AgendaTrack : AggregateRoot
    {
        public ConferenceId ConferenceId { get; private set; }
        public string Name { get; private set; }
        public ICollection<AgendaSlot> Slots { get; }

        public static AgendaTrack Create(ConferenceId conferenceId, string name)
        {
            var entity = new AgendaTrack
            {
                Id = Guid.NewGuid(),
                ConferenceId = conferenceId
            };
            entity.ChangeName(name);
            entity.ClearEvents();

            return entity;
        }

        private void ChangeName(string name)
        {
            throw new NotImplementedException();
        }
    }
}