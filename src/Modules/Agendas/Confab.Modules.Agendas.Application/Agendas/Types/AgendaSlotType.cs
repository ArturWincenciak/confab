using Confab.Modules.Agendas.Application.Agendas.Exceptions;
using Confab.Modules.Agendas.Domain.Agendas.Entities;

namespace Confab.Modules.Agendas.Application.Agendas.Types
{
    public static class AgendaSlotType
    {
        public const string Regular = "regular";
        public const string Placeholder = "laceholder";

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