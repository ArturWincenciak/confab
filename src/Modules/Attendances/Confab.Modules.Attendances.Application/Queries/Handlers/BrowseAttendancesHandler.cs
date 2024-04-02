using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Confab.Modules.Attendances.Application.Clients.Agendas;
using Confab.Modules.Attendances.Application.Clients.Agendas.DTO;
using Confab.Modules.Attendances.Application.DTO;
using Confab.Modules.Attendances.Domain.Repositories;
using Confab.Shared.Abstractions.Queries;

namespace Confab.Modules.Attendances.Application.Queries.Handlers;

internal sealed class BrowseAttendancesHandler : IQueryHandler<BrowseAttendances, BrowseAttendances.Result>
{
    private readonly IAgendasApiClient _agendasApiClient;
    private readonly IParticipantsRepository _participantsRepository;

    public BrowseAttendancesHandler(IParticipantsRepository participantsRepository,
        IAgendasApiClient agendasApiClient)
    {
        _participantsRepository = participantsRepository;
        _agendasApiClient = agendasApiClient;
    }

    public async Task<BrowseAttendances.Result> HandleAsync(BrowseAttendances query)
    {
        var participant = await _participantsRepository.GetAsync(query.ConferenceId, query.UserId);
        if (participant is null)
            return null;

        var attendances = new List<AttendanceDto>();
        var tracks = await _agendasApiClient.GetAgendaAsync(query.ConferenceId);
        var slots = tracks.SelectMany(x => x.Slots.OfType<RegularAgendaSlotDto>()).ToArray();
        foreach (var attendance in participant.Attendances)
        {
            var slot = slots.Single(x => x.AgendaItem.Id == attendance.AttendableEventId);
            var attendanceDto = new AttendanceDto
            (
                attendance.Id,
                ConferenceId: query.ConferenceId,
                EventId: slot.Id,
                From: attendance.From,
                To: attendance.To,
                Title: slot.AgendaItem.Title,
                Description: slot.AgendaItem.Description,
                Level: slot.AgendaItem.Level
            );
            attendances.Add(attendanceDto);
        }

        return new(attendances);
    }
}