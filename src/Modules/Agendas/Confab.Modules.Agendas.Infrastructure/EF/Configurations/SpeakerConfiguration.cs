using Confab.Modules.Agendas.Domain.Submissions.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Confab.Modules.Agendas.Infrastructure.EF.Configurations;

internal sealed class SpeakerConfiguration : IEntityTypeConfiguration<Speaker>
{
    public void Configure(EntityTypeBuilder<Speaker> builder)
    {
        builder.HasKey(x => x.Id);

        builder
            .Property(x => x.Id)
            .HasConversion(convertToProviderExpression: aggregateId => aggregateId.Value,
                convertFromProviderExpression: guid => new(guid));

        builder
            .Property(x => x.Version)
            .IsConcurrencyToken();
    }
}