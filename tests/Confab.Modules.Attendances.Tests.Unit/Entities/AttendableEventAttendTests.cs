using System;
using Confab.Modules.Attendances.Domain.Entities;
using Confab.Modules.Attendances.Domain.Exceptions;
using Shouldly;
using Xunit;

namespace Confab.Modules.Attendances.Tests.Unit.Entities
{
    public class AttendableEventAttendTests
    {
        private Guid _conferenceId;
        private Participant _participant_1;
        private Participant _participant_2;
        private Slot _slot_1;
        private AttendableEvent _target;

        private void Arrange()
        {
            _conferenceId = Guid.Parse("44C6F685-1E79-490A-B0B1-F3BE3545C990");
            var attendableEventId = Guid.Parse("962C87AD-9FC0-45C4-B38D-98F215271746");
            var from = new DateTime(2022, 2, 6, 11, 0, 0);
            var to = new DateTime(2022, 2, 6, 12, 0, 0);
            _target = AttendableEvent.Create(attendableEventId, _conferenceId, from, to);
            var userId_1 = Guid.Parse("BDF65461-9944-4B31-9EC3-C43EA63CFB7F");
            _participant_1 = Participant.Create(_conferenceId, userId_1, null);
            _slot_1 = new Slot(Guid.Parse("39720949-CD62-4EFA-B1C8-ABBDF8F7334B"));
        }

        private void WithSlot()
        {
            _target.AddSlots(new[] {_slot_1});
        }

        private void WithTakeSlot()
        {
            _slot_1.Take(_participant_1.Id);
        }

        private void WithSecondParticipant()
        {
            var userId_2 = Guid.Parse("BD578305-F3C5-46F2-88CB-956E2F44278E");
            _participant_2 = Participant.Create(_conferenceId, userId_2, null);
        }

        private Attendance Act()
        {
            return _target.Attend(_participant_1);
        }

        private Attendance Act(Participant participant)
        {
            return _target.Attend(participant);
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

        [Fact]
        public void Given_No_Free_Slots_When_Attend_Then_Should_Fail()
        {
            Arrange();
            WithSlot();
            WithTakeSlot();
            WithSecondParticipant();

            var actual = Record.Exception(() => Act(_participant_2));

            actual.ShouldNotBeNull();
            actual.ShouldBeOfType<NoFreeSlotsException>();
            _target.Slots.ShouldNotBeEmpty();
        }

        [Fact]
        public void Given_Free_Slot_When_Attend_Then_Success()
        {
            Arrange();
            WithSlot();

            var actual = Act();

            actual.ShouldNotBeNull();
            actual.ParticipantId.ShouldBe(_participant_1.Id);
        }
    }
}