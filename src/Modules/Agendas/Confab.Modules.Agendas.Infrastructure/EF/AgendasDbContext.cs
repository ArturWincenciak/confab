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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("agendas");
            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
        }
    }
}