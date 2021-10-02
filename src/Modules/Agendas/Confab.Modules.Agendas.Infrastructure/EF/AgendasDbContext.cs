using Confab.Modules.Agendas.Domain.Agendas.Entities;
using Confab.Modules.Agendas.Domain.CallForPaper.Entities;
using Confab.Modules.Agendas.Domain.Submissions.Entities;
using Microsoft.EntityFrameworkCore;

namespace Confab.Modules.Agendas.Infrastructure.EF
{
    internal sealed class AgendasDbContext : DbContext
    {
        public AgendasDbContext(DbContextOptions<AgendasDbContext> options)
            : base(options)
        {
        }

        public DbSet<Submission> Submissions { get; set; }
        public DbSet<Speaker> Speakers { get; set; }
        public DbSet<CallForPapers> CallForPapers { get; set; }
        public DbSet<AgendaItem> AgendaItems { get; set; }
        public DbSet<AgendaTrack> AgendaTracks { get; set; }
        public DbSet<AgendaSlot> AgendaSlots { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("agendas");
            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
        }
    }
}