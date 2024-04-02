using Confab.Modules.Attendances.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Confab.Modules.Attendances.Infrastructure.EF.Configurations;

internal class SlotConfiguration : IEntityTypeConfiguration<Slot>
{
    public void Configure(EntityTypeBuilder<Slot> builder)
    {
        builder.Property(x => x.Id)
            .HasConversion(convertToProviderExpression: x => x.Value, convertFromProviderExpression: x => new(x));

        // defend against false update on concurrent write in 'Attend' method of 'AttendableEvent' aggregator
        builder.Property(x => x.ParticipantId)
            .HasConversion(convertToProviderExpression: x => x.Value, convertFromProviderExpression: x => new(x))
            .IsConcurrencyToken();
    }
}