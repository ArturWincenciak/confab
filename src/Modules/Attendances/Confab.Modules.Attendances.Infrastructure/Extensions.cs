using System.Runtime.CompilerServices;
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

[assembly: InternalsVisibleTo("Confab.Modules.Tests.Integrations")]

namespace Confab.Modules.Attendances.Infrastructure
{
    public static class Extensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            return services
                .AddSingleton<IAgendasApiClient, AgendasApiClient>()
                .AddScoped<IAttendableEventsRepository, AttendableEventsRepository>()
                .AddScoped<IParticipantsRepository, ParticipantsRepository>()
                .AddScoped<IAttendanceRepository, AttendanceRepository>()
                .AddPostgresDbContext<AttendancesDbContext>()
                .WithTransactionalCommandHandles();
        }

        private static IServiceCollection WithTransactionalCommandHandles(this IServiceCollection services)
        {
            return services
                .AddUnityOfWork()
                .WithTransactionalCommandHandlerOf<AttendEvent>();
        }

        private static IServiceCollection WithTransactionalCommandHandlerOf<T>(this IServiceCollection services)
            where T : class, ICommand
        {
            return services
                .Decorate<ICommandHandler<T>, TransactionalCommandHandlerDecorator<T, IAttendancesUnitOfWork>>();
        }

        private static IServiceCollection AddUnityOfWork(this IServiceCollection services)
        {
            return services
                .AddScoped<IAttendancesUnitOfWork, AttendancesUnitOfWork>();
        }
    }
}