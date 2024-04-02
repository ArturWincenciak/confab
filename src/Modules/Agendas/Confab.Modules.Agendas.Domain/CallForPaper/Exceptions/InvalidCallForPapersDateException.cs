using System;
using Confab.Shared.Kernel.Exceptions;

namespace Confab.Modules.Agendas.Domain.CallForPaper.Exceptions;

internal class InvalidCallForPapersDateException : ConfabException
{
    public DateTime From { get; }
    public DateTime To { get; }

    public InvalidCallForPapersDateException(DateTime from, DateTime to)
        : base($"CFP has invalid dates, from: '{from}', to: '{to}'.")
    {
        From = from;
        To = to;
    }
}