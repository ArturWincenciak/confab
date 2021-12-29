using System;
using Confab.Shared.Abstractions.Queries;

namespace Confab.Modules.Attendances.Infrastructure.Clients.Requests
{
    internal sealed record GetRegularAgendaSlot(Guid AgendaItemId) : IModuleRequest;
}