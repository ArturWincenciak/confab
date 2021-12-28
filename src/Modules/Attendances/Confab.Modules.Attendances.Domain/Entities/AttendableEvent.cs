using System;
using System.Collections.Generic;
using System.Linq;
using Confab.Modules.Attendances.Domain.Exceptions;
using Confab.Modules.Attendances.Domain.Types;
using Confab.Shared.Kernel.Types;
using Confab.Shared.Kernel.Types.Base;

namespace Confab.Modules.Attendances.Domain.Entities
{
    public class AttendableEvent : AggregateRoot<AttendableEventId>
    {
        private readonly HashSet<Slot> _slots = new();

        public AttendableEvent(AttendableEventId id, ConferenceId conferenceId, DateTime from, DateTime to,
            IEnumerable<Slot> slots = null)
        {
            Id = id;
            ConferenceId = conferenceId;
            From = from;
            To = to;
            _slots = new HashSet<Slot>(slots ?? Enumerable.Empty<Slot>());
        }

        public ConferenceId ConferenceId { get; }
        public DateTime From { get; }
        public DateTime To { get; }
        public IEnumerable<Slot> Slots => _slots;

        public Attendance Attend(Participant participant)
        {
            if (!_slots.Any())
                throw new NoFreeSlotsException();

            if (_slots.Any(x => x.ParticipantId == participant.Id))
                throw new AlreadyParticipatingInEventException();

            var slot = Slots.FirstOrDefault(x => x.IsFree);
            if (slot is null)
                throw new NoFreeSlotsException();

            slot.Take(participant.Id);
            var attendance = new Attendance(Guid.NewGuid(), Id, slot.Id, participant.Id, From, To);
            participant.Attend(attendance);
            IncrementVersion();

            return attendance;
        }

        public void AddSlots(IEnumerable<Slot> slots)
        {
            foreach (var slot in slots)
                _slots.Add(slot);
        }

        public void AddSlots(params Slot[] slots)
        {
            foreach (var slot in slots)
                _slots.Add(slot);
        }
    }
}