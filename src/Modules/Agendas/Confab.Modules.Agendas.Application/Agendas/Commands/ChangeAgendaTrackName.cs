using System;
using Confab.Shared.Abstractions.Commands;

namespace Confab.Modules.Agendas.Application.Agendas.Commands;

public sealed record ChangeAgendaTrackName(Guid Id, string Name) : ICommand;