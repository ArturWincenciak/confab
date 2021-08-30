using Confab.Modules.Tickets.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Confab.Modules.Tickets.Core.DAL
{
    internal class TicketsDbContext : DbContext
    {
        public TicketsDbContext(DbContextOptions<TicketsDbContext> options)
            : base(options)
        {
        }

        public DbSet<Conference> Conferences { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<TicketSale> TicketSales { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("tickets");
            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
        }
    }
}