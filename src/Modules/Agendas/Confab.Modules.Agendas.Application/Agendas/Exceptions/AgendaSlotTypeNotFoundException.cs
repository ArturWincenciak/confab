using Confab.Shared.Abstractions.Exceptions;

namespace Confab.Modules.Agendas.Application.Agendas.Exceptions
{
    internal sealed class AgendaSlotTypeNotFoundException : ConfabException
    {
        public AgendaSlotTypeNotFoundException(string type)
            : base($"Agenda slot type: '{type}' was not found.")
        {
            Type = type;
        }

        public string Type { get; }
    }
}