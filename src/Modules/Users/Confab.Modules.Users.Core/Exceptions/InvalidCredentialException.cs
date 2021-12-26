using Confab.Shared.Abstractions.Exceptions;
using Confab.Shared.Kernel.Exceptions;

namespace Confab.Modules.Users.Core.Exceptions
{
    internal class InvalidCredentialException : ConfabException
    {
        public InvalidCredentialException()
            : base("Invalid credentials.")
        {
        }
    }
}