using System;
using Confab.Shared.Abstractions.Commands;

namespace Confab.Modules.Agendas.Application.CallForProps.Commands
{
    public record CreateCallForPapers(Guid ConferenceId, DateTime From, DateTime To) : ICommand;
}