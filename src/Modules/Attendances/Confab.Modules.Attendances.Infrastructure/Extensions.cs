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

                //services.AddScoped<PostgresUnitOfWork<AttendancesDbContext>>();

                services.Decorate<ICommandHandler<AttendEvent>, TransactionalCommandHandlerDecorator<AttendEvent, IAttendancesUnitOfWork>>();

            return services;
        }

        //public static IServiceCollection AddPostgresUnitOfWork<TUnitOfWork, TImplementation>(
        //    this IServiceCollection services)
        //    where TUnitOfWork : class, IUnitOfWork
        //    where TImplementation: class, TUnitOfWork
        //{
        //    services.AddScoped<TUnitOfWork, TImplementation>();
        //    services.AddScoped<IUnitOfWork, TImplementation>();

        //    using var serviceProvider = services.BuildServiceProvider();
        //    var registry = serviceProvider.GetRequiredService<PostgresUnitOfWorkTypeRegistry>();
        //    registry.Register<TUnitOfWork>();

        //    return services;
        //}

        //private static IServiceCollection AddPostgresTransactionalDecorators(this IServiceCollection services)
        //{
        //    services.TryDecorate(typeof(ICommandHandler<>), typeof(TransactionalCommandHandlerDecorator<>));
        //    return services;
        //}
    }
}