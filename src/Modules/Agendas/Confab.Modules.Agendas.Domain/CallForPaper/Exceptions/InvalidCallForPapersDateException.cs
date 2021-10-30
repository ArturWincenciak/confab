using System;
using Confab.Shared.Abstractions.Exceptions;

namespace Confab.Modules.Agendas.Domain.CallForPaper.Exceptions
{
    internal class InvalidCallForPapersDateException : ConfabException
    {
        public InvalidCallForPapersDateException(DateTime from, DateTime to)
            : base($"CFP has invalid dates, from: '{from}', to: '{to}'.")
        {
            From = from;
            To = to;
        }

        public DateTime From { get; }
        public DateTime To { get; }
    }
}