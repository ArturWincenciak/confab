using System;
using System.Threading.Tasks;
using Confab.Modules.Users.Core.Entities;
using Confab.Modules.Users.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Confab.Modules.Users.Core.DAL.Repositories
{
    internal class UserRepository : IUserRepository
    {
        private readonly UsersDbContext _dbContext;
        private readonly DbSet<User> _users;

        public UserRepository(UsersDbContext dbContext)
        {
            _dbContext = dbContext;
            _users = _dbContext.Users;
        }

        public Task<User> GetAsync(Guid id)
        {
            return _users.SingleOrDefaultAsync(x => x.Id == id);
        }

        public Task<User> GetAsync(string email)
        {
            return _users.SingleOrDefaultAsync(x => x.Email == email);
        }

        public async Task AddAsync(User user)
        {
            _users.Add(user);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(User user)
        {
            _users.Update(user);
            await _dbContext.SaveChangesAsync();
        }
    }
}