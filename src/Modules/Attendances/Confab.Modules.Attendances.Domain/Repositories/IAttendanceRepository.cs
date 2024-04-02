using System.Threading.Tasks;
using Confab.Modules.Attendances.Domain.Entities;

namespace Confab.Modules.Attendances.Domain.Repositories;

public interface IAttendanceRepository
{
    Task AddAsync(Attendance attendance);
}