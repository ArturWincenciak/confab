﻿using System;
using Confab.Shared.Kernel.Exceptions;

namespace Confab.Modules.Tickets.Core.Exceptions;

internal class TooManyTicketsException : ConfabException
{
    public Guid ConferenceId { get; }

    public TooManyTicketsException(Guid conferenceId)
        : base("Too many tickets would be generated for the conference.") =>
        ConferenceId = conferenceId;
}