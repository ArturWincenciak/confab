using System;
using Confab.Modules.Attendances.Infrastructure.EF;
using Confab.Shared.Tests;

namespace Confab.Modules.Attendances.Tests.Integrations
{
    public class TestAttendancesDbContext : IDisposable
    {
        public TestAttendancesDbContext()
        {
            var dbContextOptions = DbHelper.GetOptions<AttendancesDbContext>();
            DbContext = new AttendancesDbContext(dbContextOptions);
        }

        public AttendancesDbContext DbContext { get; }

        public void Dispose()
        {
            DbContext?.Database.EnsureDeleted();
            DbContext?.Dispose();
        }
    }
}