﻿using System;
using Confab.Shared.Kernel.Exceptions;

namespace Confab.Modules.Conferences.Core.Exceptions;

public class HostNotFoundException : ConfabException
{
    public Guid Id { get; }

    public HostNotFoundException(Guid id)
        : base($"Host with ID: '{id}' was not found.") =>
        Id = id;
}