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
        public ConferenceId ConferenceId { private set; get; }
        public DateTime From { private set; get; }
        public DateTime To { private set; get; }
        public ICollection<Slot> Slots { private set; get; }

        public static AttendableEvent Create(AttendableEventId id, ConferenceId conferenceId, DateTime from,
            DateTime to, ICollection<Slot> slots = null)
        {
            return Build(() =>
            {
                var entity = new AttendableEvent
                {
                    Id = id,
                    ConferenceId = conferenceId,
                    From = from,
                    To = to,
                    Slots = slots ?? Array.Empty<Slot>()
                };
                return entity;
            });
        }

        public Attendance Attend(Participant participant)
        {
            if (!Slots.Any())
                throw new NoFreeSlotsException();

            if (Slots.Any(x => x.ParticipantId == participant.Id))
                throw new AlreadyParticipatingInEventException();

            // here should be randomize to avoid to reduce the likelihood of taking the same resource in parallel
            var slot = Slots.FirstOrDefault(x => x.IsFree);
            if (slot is null)
                throw new NoFreeSlotsException();

            var attendance = new Attendance(Guid.NewGuid(), Id, slot.Id, participant.Id, From, To);

            //Apply(() => { // as in course mentioned here we do not call Apply and then IncrementVersion
                slot.Take(participant.Id);
                participant.Attend(attendance);
            //});

            return attendance;
        }

        public void AddSlots(IEnumerable<Slot> slots)
        {
            Apply(() =>
            {
                foreach (var slot in slots)
                    Slots.Add(slot);
            });
        }
    }
}