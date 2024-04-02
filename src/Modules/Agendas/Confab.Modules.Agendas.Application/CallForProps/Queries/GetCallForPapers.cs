﻿using System;
using Confab.Shared.Abstractions.Queries;

namespace Confab.Modules.Agendas.Application.CallForProps.Queries;

public record GetCallForPapers(Guid ConferenceId) : IRequestMessage<GetCallForPapers.Result>
{
    public record Result(Guid Id, Guid ConferenceId, DateTime From, DateTime To, bool IsOpened) : IResponseMessage;
}