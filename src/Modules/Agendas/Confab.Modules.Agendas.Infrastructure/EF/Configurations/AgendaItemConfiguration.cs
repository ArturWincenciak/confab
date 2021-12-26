using System;
using System.Collections.Generic;
using System.Linq;
using Confab.Modules.Agendas.Domain.Agendas.Entities;
using Confab.Shared.Kernel.Types;
using Confab.Shared.Kernel.Types.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Confab.Modules.Agendas.Infrastructure.EF.Configurations
{
    internal sealed class AgendaItemConfiguration : IEntityTypeConfiguration<AgendaItem>
    {
        public void Configure(EntityTypeBuilder<AgendaItem> builder)
        {
            builder.HasKey(x => x.Id);

            builder
                .Property(x => x.Id)
                .HasConversion(aggregateId => aggregateId.Value, guid => new AggregateId(guid));

            builder
                .Property(x => x.ConferenceId)
                .HasConversion(conferenceId => conferenceId.Value, guid => new ConferenceId(guid));

            builder
                .Property(x => x.Tags)
                .HasConversion(tags => string.Join(',', tags), tags => tags.Split(',', StringSplitOptions.None));

            builder
                .Property(x => x.Version)
                .IsConcurrencyToken();

            builder
                .Property(x => x.Tags).Metadata.SetValueComparer(
                    new ValueComparer<IEnumerable<string>>((c1, c2) => c1.SequenceEqual(c2),
                        c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode()))));
        }
    }
}