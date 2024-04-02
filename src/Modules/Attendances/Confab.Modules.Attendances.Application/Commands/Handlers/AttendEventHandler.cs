using System.Threading.Tasks;
using Confab.Modules.Attendances.Domain.Exceptions;
using Confab.Modules.Attendances.Domain.Repositories;
using Confab.Shared.Abstractions.Commands;

namespace Confab.Modules.Attendances.Application.Commands.Handlers;

internal sealed class AttendEventHandler : ICommandHandler<AttendEvent>
{
    private readonly IAttendableEventsRepository _attendableEventsRepository;
    private readonly IAttendanceRepository _attendanceRepository;
    private readonly IParticipantsRepository _participantsRepository;

    public AttendEventHandler(IAttendableEventsRepository attendableEventsRepository,
        IParticipantsRepository participantsRepository, IAttendanceRepository attendanceRepository)
    {
        _attendableEventsRepository = attendableEventsRepository;
        _participantsRepository = participantsRepository;
        _attendanceRepository = attendanceRepository;
    }

    public async Task HandleAsync(AttendEvent command)
    {
        var attendableEvent = await _attendableEventsRepository.GetAsync(command.Id);
        if (attendableEvent is null)
            throw new AttendableEventNotFoundException(command.Id);

        var participant = await _participantsRepository
            .GetAsync(attendableEvent.ConferenceId, command.ParticipantId);
        if (participant is null)
            throw new ParticipantNotFoundException(attendableEvent.ConferenceId, command.ParticipantId);

        var attendance = attendableEvent.Attend(participant);
        await _attendanceRepository.AddAsync(attendance);
        await _participantsRepository.UpdateAsync(participant);
        await _attendableEventsRepository.UpdateAsync(attendableEvent);
    }
}