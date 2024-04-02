using System;
using System.Collections.Generic;
using Confab.Modules.Attendances.Domain.Entities;
using Confab.Modules.Attendances.Domain.Exceptions;
using Confab.Shared.Kernel.Types;
using Shouldly;
using Xunit;

namespace Confab.Modules.Attendances.Tests.Unit.Entities;

public class ParticipantTests
{
    private List<Attendance> _attendances;
    private Attendance _newAttendance;
    private Participant _participant;
    private Guid _userId;

    private void Arrange()
    {
        var conferenceId = new ConferenceId(Guid.Parse("B8036804-9E9E-4A17-B1B8-93F5686390C4"));
        _userId = Guid.Parse("90D9EC92-417A-4811-BCE7-B9E56BAFA595");
        _attendances = new();
        _participant = Participant.Create(conferenceId, userId: new(_userId), _attendances);
    }

    private void WithExistingAttendance()
    {
        _attendances.Add(new(
            Id: Guid.Parse("0C4FA969-6EF7-4096-8A89-0A5400477484"),
            AttendableEventId: new(Guid.Parse("082453B4-266E-436C-BD7B-560C91C5F73A")),
            SlotId: new(Guid.Parse("BA085127-361B-4710-9B1D-F58F5E4CDE7F")),
            _participant.Id,
            From: GetDate(hour: 9),
            To: GetDate(hour: 10, minute: 30))
        );
    }

    private void WithNewAttendance(DateTime from, DateTime to)
    {
        _newAttendance = new(Id: Guid.Parse("78CD686D-4FF3-48CE-882B-1C125DB57B81"),
            AttendableEventId: new(Guid.Parse("650EEDE5-9E03-46F1-B89A-16DE55B4B001")),
            SlotId: new(Guid.Parse("BA085127-361B-4710-9B1D-F58F5E4CDE7F")),
            _participant.Id,
            from,
            to);
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

    [Theory]
    [MemberData(nameof(WithCollidingDates))]
    public void Given_Colliding_Attendance_When_Attend_Then_Fail(DateTime from, DateTime to)
    {
        Arrange();
        WithExistingAttendance();
        WithNewAttendance(from, to);

        var actual = Record.Exception(() => Act(_newAttendance));

        actual.ShouldNotBeNull();
        actual.ShouldBeOfType<AlreadyParticipatingSameTimeException>();
    }

    [Theory]
    [MemberData(nameof(WithAvailableDates))]
    public void Given_Available_Attendance_When_Attend_Then_Success(DateTime from, DateTime to)
    {
        Arrange();
        WithExistingAttendance();
        WithNewAttendance(from, to);

        Act(_newAttendance);

        _participant.Attendances.ShouldContain(x => x.Id == _newAttendance.Id);
    }

    public static IEnumerable<object[]> WithCollidingDates()
    {
        yield return new object[] {GetDate(8), GetDate(hour: 10, minute: 30)};
        yield return new object[] {GetDate(8), GetDate(hour: 11, minute: 30)};
        yield return new object[] {GetDate(9), GetDate(hour: 10, minute: 30)};
        yield return new object[] {GetDate(9), GetDate(hour: 11, minute: 30)};
        yield return new object[] {GetDate(10), GetDate(hour: 10, minute: 30)};
        yield return new object[] {GetDate(10), GetDate(hour: 11, minute: 30)};
    }

    public static IEnumerable<object[]> WithAvailableDates()
    {
        yield return new object[] {GetDate(8), GetDate(9)};
        yield return new object[] {GetDate(7), GetDate(hour: 8, minute: 30)};
        yield return new object[] {GetDate(hour: 10, minute: 30), GetDate(12)};
        yield return new object[] {GetDate(hour: 11, minute: 30), GetDate(12)};
    }

    private static DateTime GetDate(int hour, int minute = 0, int second = 0) =>
        new(year: 2022, month: 2, day: 14, hour, minute, second);
}