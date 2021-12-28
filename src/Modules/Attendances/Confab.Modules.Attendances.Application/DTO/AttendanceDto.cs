using System;
using System.Collections;
using System.Collections.Generic;
using Confab.Shared.Abstractions.Queries;

namespace Confab.Modules.Attendances.Application.DTO
{
    public class AttendanceDto
    {
        public Guid Id { get; set; }
        public Guid EventId { get; set; }
        public Guid ConferenceId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Level { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
    }

    public class AttendancesDto : IReadOnlyList<AttendanceDto>, IQueryResult
    {
        private readonly IReadOnlyList<AttendanceDto> _attendances;

        public AttendancesDto(IReadOnlyList<AttendanceDto> attendances)
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