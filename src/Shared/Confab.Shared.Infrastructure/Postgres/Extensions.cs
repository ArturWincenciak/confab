using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Confab.Shared.Infrastructure.Postgres;

public static class Extensions
{
    public static IServiceCollection AddPostgresOptions(this IServiceCollection services)
    {
        var options = services.GetOptions<PostgresOptions>("postgres");
        services.AddSingleton(options);
        return services;
    }

    public static IServiceCollection AddPostgresDbContext<T>(this IServiceCollection services) where T : DbContext
    {
        var options = services.GetOptions<PostgresOptions>("postgres");
        services.AddDbContext<T>(dbContextOptionsBuilder =>
            dbContextOptionsBuilder.UseNpgsql(options.ConnectionString));

        return services;
    }
}