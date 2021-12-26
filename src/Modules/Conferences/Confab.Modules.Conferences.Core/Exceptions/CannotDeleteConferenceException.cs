using System;
using Confab.Shared.Kernel.Exceptions;

namespace Confab.Modules.Conferences.Core.Exceptions
{
    internal class CannotDeleteConferenceException : ConfabException
    {
        public CannotDeleteConferenceException(Guid id)
            : base($"Conference with ID: '{id}' cannot be deleted.")
        {
            Id = id;
        }

        public Guid Id { get; }
    }
}