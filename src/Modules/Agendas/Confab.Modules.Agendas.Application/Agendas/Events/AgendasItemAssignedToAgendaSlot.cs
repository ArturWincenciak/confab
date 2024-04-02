using System;
using Confab.Shared.Abstractions.Events;

namespace Confab.Modules.Agendas.Application.Agendas.Events;

public sealed record AgendaItemAssignedToAgendaSlot(Guid AgendaSlotId, Guid AgendaItemId) : IEvent;