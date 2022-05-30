using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Confab.Tests.Integrations.Infrastructure
{
    internal static class Db
    {
        internal static void EnsureDatabaseDeleted(IServiceCollection services)
        {
            Retry(() =>
            {
                var serviceProvider = services.BuildServiceProvider();
                using var scope = serviceProvider.CreateScope();
                var scopedServices = scope.ServiceProvider;
                var dbContextType = GetSomeFirstDbContextType();
                var db = scopedServices.GetService(dbContextType) as DbContext;
                db?.Database?.EnsureDeleted();
            }, maxAttempt: 10, delay: TimeSpan.FromSeconds(1));
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

        private static void Retry(Action action, int maxAttempt, TimeSpan delay)
        {
            var currentAttempt = 1;
            while (currentAttempt <= maxAttempt)
            {
                try
                {
                    action();
                    break;
                }
                catch
                {
                    if (currentAttempt > maxAttempt)
                    {
                        throw;
                    }

                    currentAttempt++;
                    Task.Delay(delay).Wait();
                }
            }
        }
    }
}