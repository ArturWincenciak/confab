using System;
using Confab.Shared.Abstractions.Exceptions;

namespace Confab.Modules.Speakers.Core.Exceptions
{
    internal class SpeakerNotFoundException : ConfabException
    {
        public Guid Id { get; }

        public SpeakerNotFoundException(Guid id)
            : base($"Speaker with ID: '{id}' was not found.")
        {
            Id = id;
        }
    }
}