using Confab.Modules.Attendances.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Confab.Modules.Attendances.Infrastructure.EF.Configurations;

internal class AttendableEventConfiguration : IEntityTypeConfiguration<AttendableEvent>
{
    public void Configure(EntityTypeBuilder<AttendableEvent> builder)
    {
        builder.Property(x => x.Id)
            .HasConversion(convertToProviderExpression: x => x.Value, convertFromProviderExpression: x => new(x));

        builder.Property(x => x.ConferenceId)
            .HasConversion(convertToProviderExpression: x => x.Value, convertFromProviderExpression: x => new(x));

        // intentionally there is no 'IsConcurrencyToken'
    }
}