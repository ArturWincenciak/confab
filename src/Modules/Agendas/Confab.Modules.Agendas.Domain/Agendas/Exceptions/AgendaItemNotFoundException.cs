using Confab.Shared.Kernel.Exceptions;
using Confab.Shared.Kernel.Types.Base;

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