using System;
using Confab.Shared.Abstractions.Exceptions;
using Confab.Shared.Kernel.Exceptions;

namespace Confab.Modules.Speakers.Core.Exceptions
{
    internal class SpeakerNotFoundException : ConfabException
    {
        public SpeakerNotFoundException(Guid id)
            : base($"Speaker with ID: '{id}' was not found.")
        {
            Id = id;
        }

        public Guid Id { get; }
    }
}