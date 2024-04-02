using System.Runtime.CompilerServices;
using Confab.Modules.Conferences.Core.DAL;
using Confab.Modules.Conferences.Core.DAL.Repositories;
using Confab.Modules.Conferences.Core.Policies;
using Confab.Modules.Conferences.Core.Repositories;
using Confab.Modules.Conferences.Core.Services;
using Confab.Shared.Infrastructure.Postgres;
using Microsoft.Extensions.DependencyInjection;

[assembly: InternalsVisibleTo("Confab.Modules.Conferences.Api")]
[assembly: InternalsVisibleTo("Confab.Tests.Integrations")]

namespace Confab.Modules.Conferences.Core;

internal static class Extensions
{
    public static IServiceCollection AddCore(this IServiceCollection services)
    {
        services.AddPostgresDbContext<ConferencesDbContext>();
        services.AddScoped<IHostService, HostService>();
        services.AddScoped<IHostRepository, HostRepository>();
        services.AddSingleton<IHostDeletionPolice, HostDeletionPolice>();
        services.AddScoped<IConferenceService, ConferenceService>();
        services.AddScoped<IConferenceRepository, ConferenceRepository>();
        services.AddSingleton<IConferenceDeletionPolice, ConferenceDeletionPolice>();

        return services;
    }
}