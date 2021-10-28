using Confab.Modules.Agendas.Application.CallForProps.Repositories;
using Confab.Modules.Agendas.Application.Submissions.Repositories;
using Confab.Modules.Agendas.Domain.Agendas.Repositories;
using Confab.Modules.Agendas.Infrastructure.EF;
using Confab.Modules.Agendas.Infrastructure.EF.Repositories;
using Confab.Shared.Infrastructure.Postgres;
using Microsoft.Extensions.DependencyInjection;

namespace Confab.Modules.Agendas.Infrastructure
{
    public static class Extensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            return services
                .AddPostgres<AgendasDbContext>()
                .AddScoped<ISubmissionRepository, SubmissionRepository>()
                .AddScoped<ISpeakerRepository, SpeakerRepository>()
                .AddScoped<IAgendaItemRepository, AgendaItemRepository>()
                .AddScoped<ICallForPapersRepository, CallForPapersRepository>();
        }
    }
}