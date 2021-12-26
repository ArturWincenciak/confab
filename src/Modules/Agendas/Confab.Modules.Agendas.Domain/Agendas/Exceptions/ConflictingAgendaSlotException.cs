using System;
using Confab.Shared.Kernel.Exceptions;

namespace Confab.Modules.Agendas.Domain.Agendas.Exceptions
{
    internal class ConflictingAgendaSlotException : ConfabException
    {
        public ConflictingAgendaSlotException(DateTime from, DateTime to)
            : base($"There is slot conflicting with date range: '{from}' | '{to}'.")
        {
        }
    }
}