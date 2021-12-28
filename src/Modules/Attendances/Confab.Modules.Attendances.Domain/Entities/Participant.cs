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
        private readonly HashSet<Attendance> _attendances = new();

        public Participant(ParticipantId id, ConferenceId conferenceId, UserId userId,
            IEnumerable<Attendance> attendances = null)
        {
            Id = id;
            ConferenceId = conferenceId;
            UserId = userId;
            _attendances = new HashSet<Attendance>(attendances ?? Enumerable.Empty<Attendance>());
        }

        public ConferenceId ConferenceId { get; }
        public UserId UserId { get; }
        public IEnumerable<Attendance> Attendances => _attendances;

        public void Attend(Attendance attendance)
        {
            if (_attendances.Any(x => x.AttendableEventId == attendance.AttendableEventId))
                throw new AlreadyParticipatingInEventException();

            if (HasCollision(attendance))
                throw new AlreadyParticipatingSameTimeException();

            _attendances.Add(attendance);
            AddEvent(new ParticipantAttendedToEvent(this, attendance));
        }

        private bool HasCollision(Attendance attendance)
        {
            return _attendances.Any(x => attendance.From >= x.From && attendance.From < x.To) ||
                   _attendances.Any(x => attendance.From <= x.From && attendance.To > x.From);
        }
    }
}