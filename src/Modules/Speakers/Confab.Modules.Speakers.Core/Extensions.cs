using System.Runtime.CompilerServices;
using Confab.Modules.Speakers.Core.DAL;
using Confab.Modules.Speakers.Core.DAL.Repositories;
using Confab.Modules.Speakers.Core.Repositories;
using Confab.Modules.Speakers.Core.Services;
using Confab.Shared.Infrastructure.Postgres;
using Microsoft.Extensions.DependencyInjection;

[assembly: InternalsVisibleTo("Confab.Modules.Speakers.Api")]
[assembly: InternalsVisibleTo("Confab.Tests.Integrations")]

namespace Confab.Modules.Speakers.Core
{
    internal static class Extensions
    {
        public static IServiceCollection AddCore(this IServiceCollection services)
        {
            services.AddPostgresDbContext<SpeakersDbContext>();
            services.AddScoped<ISpeakerService, SpeakerService>();
            services.AddScoped<ISpeakerRepository, SpeakerRepository>();
            return services;
        }
    }
}