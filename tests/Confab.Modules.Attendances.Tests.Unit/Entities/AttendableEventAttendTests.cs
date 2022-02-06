using System;
using Confab.Modules.Attendances.Domain.Entities;
using Confab.Modules.Attendances.Domain.Exceptions;
using Shouldly;
using Xunit;

namespace Confab.Modules.Attendances.Tests.Unit.Entities
{
    public class AttendableEventAttendTests
    {
        private readonly AttendableEvent _attendableEvent;
        private readonly Participant _participant;

        public AttendableEventAttendTests()
        {
            var conferenceId = Guid.Parse("44C6F685-1E79-490A-B0B1-F3BE3545C990");
            var attendableEventId = Guid.Parse("962C87AD-9FC0-45C4-B38D-98F215271746");
            var from = new DateTime(2022, 2, 6, 11, 0, 0);
            var to = new DateTime(2022, 2, 6, 12, 0, 0);
            _attendableEvent = AttendableEvent.Create(attendableEventId, conferenceId, from, to);
            var userId = Guid.Parse("BDF65461-9944-4B31-9EC3-C43EA63CFB7F");
            _participant = Participant.Create(conferenceId, userId, attendances: null);
        }

        private Attendance Act()
        {
            return _attendableEvent.Attend(_participant);
        }

        [Fact]
        public void Given_No_Slots_When_Attend_Then_Should_Fail()
        {
            var actual = Record.Exception(Act);

            actual.ShouldNotBeNull();
            actual.ShouldBeOfType<NoFreeSlotsException>();
            _attendableEvent.Slots.ShouldBeEmpty();
        }
    }
}
