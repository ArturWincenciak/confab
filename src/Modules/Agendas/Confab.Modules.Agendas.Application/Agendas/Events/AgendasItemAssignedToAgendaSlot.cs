using System;
using Confab.Shared.Abstractions.Events;

namespace Confab.Modules.Agendas.Application.Agendas.Events
{
    public sealed record AgendasItemAssignedToAgendaSlot(Guid AgendaSlotId, Guid AgendaItemId) : IEvent;
}