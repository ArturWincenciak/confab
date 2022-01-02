using Confab.Modules.Attendances.Application.Clients.Agendas;
using Confab.Modules.Attendances.Application.Commands;
using Confab.Modules.Attendances.Domain.Repositories;
using Confab.Modules.Attendances.Infrastructure.Clients;
using Confab.Modules.Attendances.Infrastructure.EF;
using Confab.Modules.Attendances.Infrastructure.EF.Repositories;
using Confab.Shared.Abstractions.Commands;
using Confab.Shared.Infrastructure.Postgres;
using Confab.Shared.Infrastructure.Postgres.Decorators;
using Microsoft.Extensions.DependencyInjection;

namespace Confab.Modules.Attendances.Infrastructure
{
    public static class Extensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services
                .AddSingleton<IAgendasApiClient, AgendasApiClient>()
                .AddScoped<IAttendableEventsRepository, AttendableEventsRepository>()
                .AddScoped<IParticipantsRepository, ParticipantsRepository>()
                .AddPostgres<AttendancesDbContext>();

                services.AddScoped<IAttendancesUnitOfWork, AttendancesUnitOfWork>();
                services.Decorate<ICommandHandler<AttendEvent>, TransactionalCommandHandlerDecorator<AttendEvent, IAttendancesUnitOfWork>>();

            return services;
        }
    }
}