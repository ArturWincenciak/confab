using Microsoft.Extensions.DependencyInjection;

namespace Confab.Shared.Infrastructure.Postgres
{
    internal static class Extensions
    {
        public static IServiceCollection AddPostgres(this IServiceCollection services)
        {
            var options = services.GetOptions<PostgresOptions>("postgres");
            services.AddSingleton(options);

            return services;
        }
    }
}
