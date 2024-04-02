using Confab.Modules.Attendances.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Confab.Modules.Attendances.Infrastructure.EF.Configurations;

internal class ParticipantConfiguration : IEntityTypeConfiguration<Participant>
{
    public void Configure(EntityTypeBuilder<Participant> builder)
    {
        builder.Property(x => x.Id)
            .HasConversion(convertToProviderExpression: x => x.Value, convertFromProviderExpression: x => new(x));

        builder.Property(x => x.ConferenceId)
            .HasConversion(convertToProviderExpression: x => x.Value, convertFromProviderExpression: x => new(x));

        builder.Property(x => x.UserId)
            .HasConversion(convertToProviderExpression: x => x.Value, convertFromProviderExpression: x => new(x));

        builder.HasIndex(x => new {x.UserId, x.ConferenceId})
            .IsUnique();

        builder.Property(x => x.Version)
            .IsConcurrencyToken();
    }
}