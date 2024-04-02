using Confab.Shared.Kernel.Exceptions;

namespace Confab.Modules.Speakers.Core.Exceptions;

internal class SpeakerAlreadyExistsException : ConfabException
{
    public string Email { get; }

    public SpeakerAlreadyExistsException(string email)
        : base($"Speaker with email: '{email}' already exists.") =>
        Email = email;
}