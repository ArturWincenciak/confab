using System;
using Confab.Shared.Kernel.Exceptions;

namespace Confab.Modules.Agendas.Domain.Agendas.Exceptions;

internal class TimePeriodException : ConfabException
{
    public TimePeriodException(DateTime from, DateTime to)
        : base($"Invalid time period: '{from}' | '{to}'.")
    {
    }
}