using System.Runtime.CompilerServices;
using Confab.Modules.Conferences.Core.DAL;
using Confab.Modules.Conferences.Core.DAL.Repositories;
using Confab.Modules.Conferences.Core.Policies;
using Confab.Modules.Conferences.Core.Repositories;
using Confab.Modules.Conferences.Core.Services;
using Microsoft.Extensions.DependencyInjection;
using Confab.Shared.Infrastructure.Postgres;

[assembly: InternalsVisibleTo(assemblyName: "Confab.Modules.Conferences.Api")]
namespace Confab.Modules.Conferences.Core
{
    internal static class Extensions
    {
        public static IServiceCollection AddCore(this IServiceCollection services)
        {
            services.AddPostgres<ConferencesDbContext>();

            services.AddScoped<IHostService, HostService>();
            //services.AddSingleton<IHostRepository, InMemoryHostRepository>();
            services.AddScoped<IHostRepository, HostRepository>();
            services.AddSingleton<IHostDeletionPolice, HostDeletionPolice>();

            services.AddScoped<IConferenceService, ConferenceService>();
            //services.AddSingleton<IConferenceRepository, InMemoryConferenceRepository>();
            services.AddScoped<IConferenceRepository, ConferenceRepository>();
            services.AddSingleton<IConferenceDeletionPolice, ConferenceDeletionPolice>();

            return services;
        }
    }
}
