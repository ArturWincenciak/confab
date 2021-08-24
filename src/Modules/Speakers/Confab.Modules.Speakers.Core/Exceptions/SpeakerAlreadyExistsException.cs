using Confab.Shared.Abstractions.Exceptions;

namespace Confab.Modules.Speakers.Core.Exceptions
{
    internal class SpeakerAlreadyExistsException : ConfabException
    {
        public SpeakerAlreadyExistsException(string email)
            : base($"Speaker with email: '{email}' already exists.")
        {
            Email = email;
        }

        public string Email { get; }
    }
}