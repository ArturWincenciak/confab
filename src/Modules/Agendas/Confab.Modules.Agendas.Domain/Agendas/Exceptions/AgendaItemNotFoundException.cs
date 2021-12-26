using Confab.Shared.Abstractions.Kernel.Types.Base;
using Confab.Shared.Kernel.Exceptions;

namespace Confab.Modules.Agendas.Domain.Agendas.Exceptions
{
    internal class AgendaItemNotFoundException : ConfabException
    {
        public AgendaItemNotFoundException(AggregateId agendaItemId)
            : base($"Agenda item with ID: '{agendaItemId}' was not found.")
        {
        }
    }
}