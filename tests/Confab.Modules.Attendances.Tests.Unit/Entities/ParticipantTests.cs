using System;
using System.Collections.Generic;
using Confab.Modules.Attendances.Domain.Entities;
using Confab.Modules.Attendances.Domain.Exceptions;
using Confab.Modules.Attendances.Domain.Types;
using Confab.Shared.Kernel.Types;
using Shouldly;
using Xunit;

namespace Confab.Modules.Attendances.Tests.Unit.Entities
{
    public class ParticipantTests
    {
        private List<Attendance> _attendances;
        private Guid _userId;
        private Participant _participant;
        private Attendance _firstNewAttendance;

        private void Arrange()
        {
            var conferenceId = new ConferenceId(Guid.Parse("B8036804-9E9E-4A17-B1B8-93F5686390C4"));
            _userId = Guid.Parse("90D9EC92-417A-4811-BCE7-B9E56BAFA595");
            _attendances = new List<Attendance>();
            _participant = Participant.Create(conferenceId, new UserId(_userId), _attendances);
        }

        private void WithExistingAttendance()
        {
            _attendances.Add(new Attendance(
                Guid.Parse("0C4FA969-6EF7-4096-8A89-0A5400477484"),
                new AttendableEventId(Guid.Parse("082453B4-266E-436C-BD7B-560C91C5F73A")),
                new SlotId(Guid.Parse("BA085127-361B-4710-9B1D-F58F5E4CDE7F")),
                _participant.Id,
                new DateTime(2022, 2, 14, 5, 25, 0),
                new DateTime(2202, 2, 14, 18, 0, 0))
            );
        }

        private void WithNewAttendanceInTheSameTimeAsAlreadyExisting()
        {
            _firstNewAttendance = new Attendance(Guid.Parse("78CD686D-4FF3-48CE-882B-1C125DB57B81"),
                new AttendableEventId(Guid.Parse("650EEDE5-9E03-46F1-B89A-16DE55B4B001")),
                new SlotId(Guid.Parse("BA085127-361B-4710-9B1D-F58F5E4CDE7F")),
                _participant.Id,
                _attendances[0].From,
                _attendances[0].To);
        }

        private void Act(Attendance attendance)
        {
            _participant.Attend(attendance);
        }

        [Fact]
        public void Given_Already_Participating_In_Event_When_Than_Attend_Then_Fail()
        {
            Arrange();
            WithExistingAttendance();
            var alreadyAddedAttendance = _attendances[0];

            var actual = Record.Exception(() => Act(alreadyAddedAttendance));

            actual.ShouldNotBeNull();
            actual.ShouldBeOfType<AlreadyParticipatingInEventException>();
        }

        [Fact]
        public void Given_Already_Participating_In_The_Same_Time_When_Attend_Then_Fail()
        {
            Arrange();
            WithExistingAttendance();
            WithNewAttendanceInTheSameTimeAsAlreadyExisting();

            var actual = Record.Exception(() => Act(_firstNewAttendance));

            actual.ShouldNotBeNull();
            actual.ShouldBeOfType<AlreadyParticipatingSameTimeException>();
        }
    }
}