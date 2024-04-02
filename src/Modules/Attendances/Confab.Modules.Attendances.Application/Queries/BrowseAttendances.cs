using System;
using System.Collections;
using System.Collections.Generic;
using Confab.Modules.Attendances.Application.DTO;
using Confab.Shared.Abstractions.Queries;

namespace Confab.Modules.Attendances.Application.Queries;

public class BrowseAttendances : IRequestMessage<BrowseAttendances.Result>
{
    public Guid UserId { get; set; }
    public Guid ConferenceId { get; set; }

    public class Result : IReadOnlyList<AttendanceDto>,
        IResponseMessage
    {
        private readonly IReadOnlyList<AttendanceDto> _attendances;

        public int Count => _attendances.Count;

        public AttendanceDto this[int index] => _attendances[index];

        public Result(IReadOnlyList<AttendanceDto> attendances) =>
            _attendances = attendances;

        public IEnumerator<AttendanceDto> GetEnumerator() =>
            _attendances.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() =>
            GetEnumerator();
    }
}