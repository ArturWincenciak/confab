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
        public ICollection<AgendaSlot> Slots { get; private set; }

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

        public EntityId AddRegularSlot(DateTime from, DateTime to, int? participantsLimit)
        {
            ValidateTimeConflict(from, to);

            EntityId createdSlotId = Guid.Empty;

            Apply(() =>
            {
                var slot = RegularAgendaSlot.Create(from, to, participantsLimit);
                Slots.Add(slot);
                createdSlotId = slot.Id;
            });

            return createdSlotId;
        }

        public EntityId AddPlaceholderSlot(DateTime from, DateTime to)
        {
            ValidateTimeConflict(from, to);

            EntityId createdSlotId = Guid.Empty;

            Apply(() =>
            {
                var slot = PlaceholderAgendaSlot.Create(from, to);
                Slots.Add(slot);
                createdSlotId = slot.Id;
            });

            return createdSlotId;
        }

        public void ChangeSlotPlaceholder(EntityId id, string placeholder)
        {
            var slot = Slots.FirstOrDefault(x => x.Id == id);
            if (slot is null)
                throw new AgendaSlotNotFoundException(id);

            if (slot is not PlaceholderAgendaSlot placeholderAgendaSlot)
                throw new InvalidAgendaSlotTypeException(id);

            Apply(() => { placeholderAgendaSlot.ChangePlaceholder(placeholder); });
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
            ValidateTimeTimePeriod(from, to);

            var hasConflictingSlots = Slots
                .Any(x =>
                    @from <= x.From && to > x.From ||
                    to >= x.To && @from < x.To);

            if (hasConflictingSlots)
                throw new ConflictingAgendaSlotException(from, to);
        }

        private void ValidateTimeTimePeriod(DateTime from, DateTime to)
        {
            if (from == to || from > to)
                throw new TimePeriodException(from, to);
        }
    }
}