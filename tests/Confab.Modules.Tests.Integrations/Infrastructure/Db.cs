using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Confab.Modules.Tests.Integrations.Infrastructure
{
    internal static class Db
    {
        internal static void EnsureDatabaseDeleted(IServiceCollection services)
        {
            var serviceProvider = services.BuildServiceProvider();
            using var scope = serviceProvider.CreateScope();
            var scopedServices = scope.ServiceProvider;
            var dbContextType = GetSomeFirstDbContextType();
            var db = scopedServices.GetService(dbContextType) as DbContext;
            db?.Database?.EnsureDeleted();
        }

        private static Type GetSomeFirstDbContextType()
        {
            var dbContextType = AppDomain.CurrentDomain
                .GetAssemblies()
                .SelectMany(x => x.GetTypes())
                .FirstOrDefault(x =>
                    typeof(DbContext).IsAssignableFrom(x) &&
                    !x.IsInterface &&
                    x != typeof(DbContext));
            return dbContextType;
        }
    }
}