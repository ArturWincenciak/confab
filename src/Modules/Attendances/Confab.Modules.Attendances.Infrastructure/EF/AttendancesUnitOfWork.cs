using Confab.Shared.Infrastructure.Postgres;
using Microsoft.Extensions.Logging;

namespace Confab.Modules.Attendances.Infrastructure.EF
{
    internal class AttendancesUnitOfWork : PostgresUnitOfWork<AttendancesDbContext>, IAttendancesUnitOfWork
    {
        public AttendancesUnitOfWork(AttendancesDbContext dbContext, ILogger<AttendancesUnitOfWork> logger)
            : base(dbContext, logger)
        {
        }
    }
}