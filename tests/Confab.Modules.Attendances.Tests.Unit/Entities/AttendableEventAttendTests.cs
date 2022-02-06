using System;
using Confab.Modules.Attendances.Domain.Entities;
using Confab.Modules.Attendances.Domain.Exceptions;
using Shouldly;
using Xunit;

namespace Confab.Modules.Attendances.Tests.Unit.Entities
{
    public class AttendableEventAttendTests
    {
        private AttendableEvent _target;
        private Participant _participant_1;
        private Slot _slot_1;

        private void Arrange()
        {
            var conferenceId = Guid.Parse("44C6F685-1E79-490A-B0B1-F3BE3545C990");
            var attendableEventId = Guid.Parse("962C87AD-9FC0-45C4-B38D-98F215271746");
            var from = new DateTime(2022, 2, 6, 11, 0, 0);
            var to = new DateTime(2022, 2, 6, 12, 0, 0);
            _target = AttendableEvent.Create(attendableEventId, conferenceId, from, to);
            var userId = Guid.Parse("BDF65461-9944-4B31-9EC3-C43EA63CFB7F");
            _participant_1 = Participant.Create(conferenceId, userId, attendances: null);
            _slot_1 = new Slot(Guid.Parse("39720949-CD62-4EFA-B1C8-ABBDF8F7334B"));
        }

        private void WithSlot()
        {
            _target.AddSlots(new []{_slot_1});
        }

        private void WithTakeSlot()
        {
            _slot_1.Take(_participant_1.Id);
        }

        private Attendance Act()
        {
            return _target.Attend(_participant_1);
        }

        [Fact]
        public void Given_No_Slots_When_Attend_Then_Should_Fail()
        {
            Arrange();

            var actual = Record.Exception(Act);

            actual.ShouldNotBeNull();
            actual.ShouldBeOfType<NoFreeSlotsException>();
            _target.Slots.ShouldBeEmpty();
        }

        [Fact]
        public void Given_Slots_With_Participant_When_Attend_Then_Should_Fail()
        {
            Arrange();
            WithSlot();
            WithTakeSlot();

            var actual = Record.Exception(Act);

            actual.ShouldNotBeNull();
            actual.ShouldBeOfType<AlreadyParticipatingInEventException>();
            _target.Slots.ShouldNotBeEmpty();
        }


    }
}
