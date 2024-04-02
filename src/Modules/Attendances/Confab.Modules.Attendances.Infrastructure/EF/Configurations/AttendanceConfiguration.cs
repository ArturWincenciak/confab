using Confab.Modules.Attendances.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Confab.Modules.Attendances.Infrastructure.EF.Configurations;

internal class AttendanceConfiguration : IEntityTypeConfiguration<Attendance>
{
    public void Configure(EntityTypeBuilder<Attendance> builder)
    {
        builder.Property(x => x.AttendableEventId)
            .HasConversion(convertToProviderExpression: x => x.Value, convertFromProviderExpression: x => new(x));

        builder.Property(x => x.SlotId)
            .HasConversion(convertToProviderExpression: x => x.Value, convertFromProviderExpression: x => new(x));

        builder.Property(x => x.ParticipantId)
            .HasConversion(convertToProviderExpression: x => x.Value, convertFromProviderExpression: x => new(x));
    }
}