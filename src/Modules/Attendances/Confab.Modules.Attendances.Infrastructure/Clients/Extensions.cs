using Confab.Modules.Attendances.Application.Clients.Agendas;
using Microsoft.Extensions.DependencyInjection;

namespace Confab.Modules.Attendances.Infrastructure.Clients
{
    public static class Extensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddSingleton<IAgendasApiClient, AgendasApiClient>();
            return services;
        }
    }
}