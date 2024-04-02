using System;

namespace Confab.Modules.Attendances.Application.DTO;

public record AttendanceDto(
    Guid Id,
    Guid EventId,
    Guid ConferenceId,
    string Title,
    string Description,
    int Level,
    DateTime From,
    DateTime To
);