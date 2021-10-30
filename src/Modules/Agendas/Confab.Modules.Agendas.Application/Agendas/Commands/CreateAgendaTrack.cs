using System;
using Confab.Shared.Abstractions.Commands;

namespace Confab.Modules.Agendas.Application.Agendas.Commands
{
    public sealed record CreateAgendaTrack(Guid ConferenceId, string Name) : ICommand<CreateAgendaTrack.AgendaTrackId>
    {
        public sealed record AgendaTrackId(Guid Id) : ICommandResult;
    }
}