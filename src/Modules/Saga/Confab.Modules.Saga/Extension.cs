using Chronicle;
using Microsoft.Extensions.DependencyInjection;

namespace Confab.Modules.Saga;

public static class Extension
{
    public static IServiceCollection AddSaga(this IServiceCollection services)
    {
        services.AddChronicle();
        return services;
    }
}