using System;
using System.Collections.Generic;
using System.Linq;
using Confab.Modules.Attendances.Domain.Events;
using Confab.Modules.Attendances.Domain.Exceptions;
using Confab.Modules.Attendances.Domain.Types;
using Confab.Shared.Kernel.Types;
using Confab.Shared.Kernel.Types.Base;

namespace Confab.Modules.Attendances.Domain.Entities
{
    public class Participant : AggregateRoot<ParticipantId>
    {
        public ConferenceId ConferenceId { get; private set; }
        public UserId UserId { get; private set; }
        public ICollection<Attendance> Attendances { get; private set; }

        public static Participant Create(ConferenceId conferenceId, UserId userId,
            ICollection<Attendance> attendances = null)
        {
            return Build(() =>
            {
                var entity = new Participant
                {
                    Id = Guid.NewGuid(),
                    ConferenceId = conferenceId,
                    UserId = userId,
                    Attendances = attendances ?? Array.Empty<Attendance>()
                };
                return entity;
            });
        }

        public void Attend(Attendance attendance)
        {
            if (Attendances.Any(x => x.AttendableEventId == attendance.AttendableEventId))
                throw new AlreadyParticipatingInEventException();

            if (HasCollision(attendance))
                throw new AlreadyParticipatingSameTimeException();

            Apply(() =>
            {
                Attendances.Add(attendance);
                AddEvent(new ParticipantAttendedToEvent(this, attendance));
            });
        }

        private bool HasCollision(Attendance attendance)
        {
            return Attendances.Any(x => attendance.From >= x.From && attendance.From < x.To) ||
                   Attendances.Any(x => attendance.From <= x.From && attendance.To > x.From);
        }
    }
}