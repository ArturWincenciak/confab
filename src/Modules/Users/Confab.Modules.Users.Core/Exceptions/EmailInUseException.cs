﻿using Confab.Shared.Kernel.Exceptions;

namespace Confab.Modules.Users.Core.Exceptions;

internal class EmailInUseException : ConfabException
{
    public EmailInUseException(string email)
        : base($"Email '{email}' is already in use.")
    {
    }
}