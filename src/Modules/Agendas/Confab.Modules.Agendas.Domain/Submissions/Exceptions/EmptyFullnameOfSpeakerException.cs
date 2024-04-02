﻿using System;
using Confab.Shared.Kernel.Exceptions;

namespace Confab.Modules.Agendas.Domain.Submissions.Exceptions;

public class EmptyFullnameOfSpeakerException : ConfabException
{
    public EmptyFullnameOfSpeakerException(Guid id)
        : base($"Full name of speaker ID: '{id}' is empty.")
    {
    }
}