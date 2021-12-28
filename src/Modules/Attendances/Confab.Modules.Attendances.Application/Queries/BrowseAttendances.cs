using System;
using System.Collections;
using System.Collections.Generic;
using Confab.Modules.Attendances.Application.DTO;
using Confab.Shared.Abstractions.Queries;

namespace Confab.Modules.Attendances.Application.Queries
{
    public class BrowseAttendances : IQuery<BrowseAttendances.Result>
    {
        public Guid UserId { get; set; }
        public Guid ConferenceId { get; set; }

        public class Result : IReadOnlyList<AttendanceDto>, IQueryResult
        {
            private readonly IReadOnlyList<AttendanceDto> _attendances;

            public Result(IReadOnlyList<AttendanceDto> attendances)
            {
                _attendances = attendances;
            }

            public IEnumerator<AttendanceDto> GetEnumerator()
            {
                return _attendances.GetEnumerator();
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }

            public int Count => _attendances.Count;

            public AttendanceDto this[int index] => _attendances[index];
        }
    }
}