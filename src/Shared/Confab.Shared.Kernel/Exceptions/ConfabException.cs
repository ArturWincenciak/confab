using System;

namespace Confab.Shared.Kernel.Exceptions;

public abstract class ConfabException : Exception
{
    protected ConfabException(string message)
        : base(message)
    {
    }
}