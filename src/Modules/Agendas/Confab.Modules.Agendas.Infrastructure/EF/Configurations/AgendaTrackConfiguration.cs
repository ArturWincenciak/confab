using Confab.Modules.Agendas.Domain.Agendas.Entities;
using Confab.Shared.Abstractions.Kernel.Types;
using Confab.Shared.Abstractions.Kernel.Types.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Confab.Modules.Agendas.Infrastructure.EF.Configurations
{
    internal sealed class AgendaTrackConfiguration : IEntityTypeConfiguration<AgendaTrack>
    {
        public void Configure(EntityTypeBuilder<AgendaTrack> builder)
        {
            builder.HasKey(x => x.Id);

            builder
                .Property(x => x.Id)
                .HasConversion(aggregateId => aggregateId.Value, guid => new AggregateId(guid));

            builder
                .Property(x => x.ConferenceId)
                .HasConversion(conferenceId => conferenceId.Value, guid => new ConferenceId(guid));

            builder
                .HasMany(x => x.Slots)
                .WithOne(x => x.Track);

            builder
                .Property(x => x.Version)
                .IsConcurrencyToken();
        }
    }
}