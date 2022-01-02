using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Confab.Shared.Infrastructure.Postgres
{
    public static class Extensions
    {
        public static IServiceCollection AddPostgres(this IServiceCollection services)
        {
            var options = services.GetOptions<PostgresOptions>("postgres");
            services.AddSingleton(options);

            return services;
        }

        public static IServiceCollection AddPostgres<T>(this IServiceCollection services) where T : DbContext
        {
            var options = services.GetOptions<PostgresOptions>("postgres");
            services.AddDbContext<T>(dbContextOptionsBuilder =>
                dbContextOptionsBuilder.UseNpgsql(options.ConnectionString));

            return services;
        }

        public static IServiceCollection AddPostgresUnitOfWork<TUnitOfWork, TImplementation>(
            this IServiceCollection services)
            where TUnitOfWork : class, IUnitOfWork
            where TImplementation: class, TUnitOfWork
        {
            services.AddScoped<TUnitOfWork, TImplementation>();
            services.AddScoped<IUnitOfWork, TImplementation>();
            return services;
        }
    }
}