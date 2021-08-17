using System.Linq;
using Confab.Modules.Users.Core.DTO;
using Confab.Modules.Users.Core.Entities;

namespace Confab.Modules.Users.Core.Mappings
{
    internal static class Extensions
    {
        public static AccountDto AsDto(this User user)
        {
            return new AccountDto
            {
                Id = user.Id,
                Email = user.Email,
                Role = user.Role,
                Claims = user.Claims,
                CreateAt = user.CreatedAt
                // TODO: CreateAt = ???
            };
        }
    }
}