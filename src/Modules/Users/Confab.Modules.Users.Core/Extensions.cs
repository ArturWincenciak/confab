using System.Runtime.CompilerServices;
using Confab.Modules.Users.Core.DAL;
using Confab.Shared.Infrastructure.Postgres;
using Microsoft.Extensions.DependencyInjection;

[assembly: InternalsVisibleTo(assemblyName: "Confab.Modules.Users.Api")]
namespace Confab.Modules.Users.Core
{
    internal static class Extensions
    {
        public static IServiceCollection AddCore(this IServiceCollection services)
        {
            services.AddPostgres<UsersDbContext>();

            return services;
        }
    }
}
