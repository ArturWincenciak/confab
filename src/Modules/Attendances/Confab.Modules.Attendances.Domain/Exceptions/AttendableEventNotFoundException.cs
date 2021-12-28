using System;
using Confab.Shared.Kernel.Exceptions;

namespace Confab.Modules.Attendances.Domain.Exceptions
{
    public class AttendableEventNotFoundException : ConfabException
    {
        public AttendableEventNotFoundException(Guid id)
            : base($"Attendable event with ID: '{id}' was not found.")
        {
            Id = id;
        }

        public Guid Id { get; }
    }
}