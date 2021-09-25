using System;
using Confab.Modules.Agendas.Domain.Agendas.Exceptions;
using Confab.Shared.Abstractions.Kernel.Types.Base;

namespace Confab.Modules.Agendas.Domain.Agendas.Entities
{
    public sealed class PlaceholderAgendaSlot : AgendaSlot
    {
        public string Placeholder { get; private set; }

        internal static PlaceholderAgendaSlot Create(EntityId id, DateTime from, DateTime to)
        {
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