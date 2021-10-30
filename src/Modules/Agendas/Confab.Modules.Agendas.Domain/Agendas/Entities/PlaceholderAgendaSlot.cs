using System;
using Confab.Modules.Agendas.Domain.Agendas.Exceptions;

namespace Confab.Modules.Agendas.Domain.Agendas.Entities
{
    public sealed class PlaceholderAgendaSlot : AgendaSlot
    {
        public string Placeholder { get; private set; }

        internal static PlaceholderAgendaSlot Create(DateTime from, DateTime to)
        {
            var id = Guid.NewGuid();
            var entity = new PlaceholderAgendaSlot {Id = id};
            entity.ChangeDateRange(from, to);
            return entity;
        }

        internal void ChangePlaceholder(string placeholder)
        {
            if (string.IsNullOrWhiteSpace(placeholder))
                throw new EmptyAgendaSlotPlaceholderException();

            Placeholder = placeholder;
        }
    }
}