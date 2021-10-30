using System;
using Confab.Shared.Abstractions.Commands;

namespace Confab.Modules.Agendas.Application.Agendas.Commands
{
    public sealed record CreateAgendaSlot(Guid AgendaTrackId, DateTime From, DateTime To, int? ParticipantsLimit,
        string Type) : ICommand<CreateAgendaSlot.AgendaSlotId>
    {
        public sealed record AgendaSlotId(Guid Id) : ICommandResult;
    }
}