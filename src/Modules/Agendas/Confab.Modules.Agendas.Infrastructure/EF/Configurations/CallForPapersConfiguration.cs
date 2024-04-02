using Confab.Modules.Agendas.Domain.CallForPaper.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Confab.Modules.Agendas.Infrastructure.EF.Configurations;

internal sealed class CallForPapersConfiguration : IEntityTypeConfiguration<CallForPapers>
{
    public void Configure(EntityTypeBuilder<CallForPapers> builder)
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
            .Property(x => x.Version)
            .IsConcurrencyToken();
    }
}