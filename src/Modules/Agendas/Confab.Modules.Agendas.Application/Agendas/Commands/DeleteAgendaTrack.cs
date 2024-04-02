using System;
using Confab.Shared.Abstractions.Commands;

namespace Confab.Modules.Agendas.Application.Agendas.Commands;

public sealed record DeleteAgendaTrack(Guid Id) : ICommand;