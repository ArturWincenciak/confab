using Confab.Modules.Agendas.Application.Agendas.Exceptions;
using Confab.Modules.Agendas.Domain.Agendas.Entities;

namespace Confab.Modules.Agendas.Application.Agendas.Types
{
    internal static class AgendaSlotType
    {
        public const string Regular = "regular";
        public const string Placeholder = "placeholder";

        public static string GetSlotType(object slot)
        {
            return slot switch
            {
                RegularAgendaSlot => Regular,
                PlaceholderAgendaSlot => Placeholder,
                _ => throw new AgendaSlotTypeNotFoundException(slot.GetType().Name)
            };
        }
    }
}