using System;
using Confab.Shared.Kernel.Exceptions;

namespace Confab.Modules.Conferences.Core.Exceptions;

internal class CannotDeleteConferenceException : ConfabException
{
    public Guid Id { get; }

    public CannotDeleteConferenceException(Guid id)
        : base($"Conference with ID: '{id}' cannot be deleted.") =>
        Id = id;
}