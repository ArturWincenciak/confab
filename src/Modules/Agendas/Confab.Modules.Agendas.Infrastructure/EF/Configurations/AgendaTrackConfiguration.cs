using Confab.Modules.Agendas.Domain.Agendas.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Confab.Modules.Agendas.Infrastructure.EF.Configurations;

internal sealed class AgendaTrackConfiguration : IEntityTypeConfiguration<AgendaTrack>
{
    public void Configure(EntityTypeBuilder<AgendaTrack> builder)
    {
        builder.HasKey(x => x.Id);

        builder
            .Property(x => x.Id)
            .HasConversion(convertToProviderExpression: aggregateId => aggregateId.Value,
                convertFromProviderExpression: guid => new(guid));

        builder
            .Property(x => x.ConferenceId)
            .HasConversion(convertToProviderExpression: conferenceId => conferenceId.Value,
                convertFromProviderExpression: guid => new(guid));

        builder
            .HasMany(x => x.Slots)
            .WithOne(x => x.Track);

        builder
            .Property(x => x.Version)
            .IsConcurrencyToken();
    }
}