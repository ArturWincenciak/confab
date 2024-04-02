﻿using Confab.Modules.Agendas.Application.Agendas.Types;
using Confab.Modules.Agendas.Domain.Agendas.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Confab.Modules.Agendas.Infrastructure.EF.Configurations;

internal sealed class AgendaSlotConfiguration : IEntityTypeConfiguration<AgendaSlot>
{
    public void Configure(EntityTypeBuilder<AgendaSlot> builder)
    {
        builder.HasKey(x => x.Id);

        builder
            .Property(x => x.Id)
            .HasConversion(convertToProviderExpression: entityId => entityId.Value,
                convertFromProviderExpression: guid => new(guid));

        builder
            .HasDiscriminator<string>("Type")
            .HasValue<PlaceholderAgendaSlot>(AgendaSlotType.Placeholder)
            .HasValue<RegularAgendaSlot>(AgendaSlotType.Regular);
    }
}