using System;
using Confab.Shared.Abstractions.Exceptions;
using Confab.Shared.Kernel.Exceptions;

namespace Confab.Modules.Users.Core.Exceptions
{
    internal class UserNotActiveException : ConfabException
    {
        public UserNotActiveException(Guid userId)
            : base($"User with ID: '{userId}' is not active.")
        {
            UserId = userId;
        }

        public Guid UserId { get; }
    }
}