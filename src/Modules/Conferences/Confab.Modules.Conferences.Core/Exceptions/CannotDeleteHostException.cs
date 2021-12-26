using System;
using Confab.Shared.Abstractions.Exceptions;
using Confab.Shared.Kernel.Exceptions;

namespace Confab.Modules.Conferences.Core.Exceptions
{
    internal class CannotDeleteHostException : ConfabException
    {
        public CannotDeleteHostException(Guid id)
            : base($"Host with ID: '{id}' cannot be deleted.")
        {
            Id = id;
        }

        public Guid Id { get; }
    }
}