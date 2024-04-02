using System.Threading.Tasks;
using Confab.Modules.Attendances.Domain.Entities;
using Confab.Modules.Attendances.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Confab.Modules.Attendances.Infrastructure.EF.Repositories;

internal class AttendanceRepository : IAttendanceRepository
{
    private readonly DbSet<Attendance> _attendances;
    private readonly AttendancesDbContext _dbContext;

    public AttendanceRepository(AttendancesDbContext dbContext)
    {
        _dbContext = dbContext;
        _attendances = dbContext.Attendances;
    }

    public Task AddAsync(Attendance attendance)
    {
        _attendances.Add(attendance);
        return _dbContext.SaveChangesAsync();
    }
}