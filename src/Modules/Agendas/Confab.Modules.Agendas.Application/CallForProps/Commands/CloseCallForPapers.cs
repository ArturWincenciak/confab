using System;
using Confab.Shared.Abstractions.Commands;

namespace Confab.Modules.Agendas.Application.CallForProps.Commands
{
    public record CloseCallForPapers(Guid ConferenceId) : ICommand, ICommandResult;
}