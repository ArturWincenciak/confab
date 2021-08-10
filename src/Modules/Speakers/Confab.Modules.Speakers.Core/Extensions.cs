using System.Runtime.CompilerServices;
using Microsoft.Extensions.DependencyInjection;

[assembly: InternalsVisibleTo(assemblyName: "Confab.Modules.Speakers.Api")]
namespace Confab.Modules.Speakers.Core
{
    internal static class Extensions
    {
        public static IServiceCollection AddCore(this IServiceCollection services)
        {
            //services.AddPostgres<ConferencesDbContext>();

            //services.AddScoped<IHostService, HostService>();
            ////services.AddSingleton<IHostRepository, InMemoryHostRepository>();
            //services.AddScoped<IHostRepository, HostRepository>();
            //services.AddSingleton<IHostDeletionPolice, HostDeletionPolice>();

            //services.AddScoped<IConferenceService, ConferenceService>();
            ////services.AddSingleton<IConferenceRepository, InMemoryConferenceRepository>();
            //services.AddScoped<IConferenceRepository, ConferenceRepository>();
            //services.AddSingleton<IConferenceDeletionPolice, ConferenceDeletionPolice>();

            return services;
        }
    }
}
