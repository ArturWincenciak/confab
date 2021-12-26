using Confab.Modules.Agendas.Domain.CallForPaper.Entities;
using Confab.Shared.Kernel.Types;
using Confab.Shared.Kernel.Types.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Confab.Modules.Agendas.Infrastructure.EF.Configurations
{
    internal sealed class CallForPapersConfiguration : IEntityTypeConfiguration<CallForPapers>
    {
        public void Configure(EntityTypeBuilder<CallForPapers> builder)
        {
            builder.HasKey(x => x.Id);

            builder
                .Property(x => x.Id)
                .HasConversion(aggregateId => aggregateId.Value, guid => new AggregateId(guid));

            builder
                .Property(x => x.ConferenceId)
                .HasConversion(conferenceId => conferenceId.Value, guid => new ConferenceId(guid));

            builder
                .Property(x => x.Version)
                .IsConcurrencyToken();
        }
    }
}