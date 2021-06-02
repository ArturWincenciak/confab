using System;

namespace Confab.Shared.Abstractions.Exceptions
{
    public class ConfabException : Exception
    {
        public ConfabException(string message)
            : base(message)
        {
            
        } 
    }
}