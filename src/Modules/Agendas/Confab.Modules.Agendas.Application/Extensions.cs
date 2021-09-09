using Microsoft.Extensions.DependencyInjection;

namespace Confab.Modules.Agendas.Application
{
    public static class Extensions
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            return services;
        }
    }
}
