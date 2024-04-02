using System;
using Confab.Shared.Abstractions.Commands;

namespace Confab.Modules.Agendas.Application.Agendas.Commands;

public sealed record AssignPlaceholderAgendaSlot(Guid AgendaSlotId,
    Guid AgendaTrackId,
    string Placeholder) : ICommand;