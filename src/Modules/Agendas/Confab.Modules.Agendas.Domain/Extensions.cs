using Microsoft.Extensions.DependencyInjection;

namespace Confab.Modules.Agendas.Domain
{
    public static class Extensions
    {
        public static IServiceCollection AddDomain(this IServiceCollection services)
        {
            return services;
        }
    }
}
