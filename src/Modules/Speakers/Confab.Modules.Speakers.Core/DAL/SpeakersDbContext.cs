using Confab.Modules.Speakers.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Confab.Modules.Speakers.Core.DAL
{
    internal class SpeakersDbContext : DbContext
    {
        public SpeakersDbContext(DbContextOptions<SpeakersDbContext> options)
            : base(options)
        {
        }

        public DbSet<Speaker> Speakers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("speakers");
            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
        }
    }
}