using System;
using System.Collections.Generic;
using System.Linq;
using Confab.Modules.Agendas.Domain.Agendas.Exceptions;
using Confab.Shared.Abstractions.Kernel.Types;
using Confab.Shared.Abstractions.Kernel.Types.Base;

namespace Confab.Modules.Agendas.Domain.Agendas.Entities
{
    public class AgendaTrack : AggregateRoot
    {
        public ConferenceId ConferenceId { get; private set; }
        public string Name { get; private set; }
        public ICollection<AgendaSlot> Slots { get; private set; } //TU

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

        public void ChangeName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new EmptyAgendaTrackNameException(Id);

            Apply(() => Name = name);
        }

        public void AddRegularSlot(DateTime from, DateTime to, int? participantsLimit)
        {
            ValidateTimeConflict(from, to);

            Apply(() =>
            {
                var slot = RegularAgendaSlot.Create(from, to, participantsLimit);
                Slots.Add(slot);
            });
        }

        public void AddPlaceholderSlot(DateTime from, DateTime to)
        {
            ValidateTimeConflict(from, to);

            Apply(() =>
            {
                var slot = PlaceholderAgendaSlot.Create(from, to);
                Slots.Add(slot);
            });
        }

        public void ChangeSlotPlaceholder(EntityId id, string placeholder)
        {
            var slot = Slots.FirstOrDefault(x => x.Id == id);
            if (slot is null)
                throw new AgendaSlotNotFoundException(id);

            if (slot is not PlaceholderAgendaSlot placeholderAgendaSlot)
                throw new InvalidAgendaSlotTypeException(id);

            Apply(() =>
            {
                placeholderAgendaSlot.ChangePlaceholder(placeholder);
            });
        }

        internal void ChangeSlotAgendaItem(EntityId agendaSlotId, AgendaItem agendaItem)
        {
            var slot = Slots.FirstOrDefault(x => x.Id == agendaSlotId);
            if (slot is null)
                throw new AgendaSlotNotFoundException(agendaSlotId);

            if (slot is not RegularAgendaSlot regularAgendaSlot)
                throw new InvalidAgendaSlotTypeException(agendaSlotId);

            regularAgendaSlot.ChangeAgendaItem(agendaItem);
        }

        private void ValidateTimeConflict(DateTime from, DateTime to)
        {
            var hasConflictingSlots = Slots
                .Any(x => (x.From <= from && x.To >= from) || (x.From <= to && x.To >= to));

            if (hasConflictingSlots)
                throw new ConflictingAgendaSlotException(from, to);
        }
    }
}