using Confab.Modules.Agendas.Domain.Submissions.Entities;
using Microsoft.EntityFrameworkCore;

namespace Confab.Modules.Agendas.Infrastructure.EF
{
    internal sealed class AgendasDbContext : DbContext
    {
        public DbSet<Submission> Submissions { get; set; }
        public DbSet<Speaker> Speakers { get; set; }

        public AgendasDbContext(DbContextOptions<AgendasDbContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("agendas");
            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
        }
    }
}