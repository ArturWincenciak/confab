using System.Collections.Generic;
using System.Reflection;
using Confab.Shared.Abstractions.Commands;
using Confab.Shared.Infrastructure.Postgres.Decorators;
using Microsoft.Extensions.DependencyInjection;

namespace Confab.Shared.Infrastructure.Commands
{
    internal static class Extensions
    {
        public static IServiceCollection AddCommands(this IServiceCollection services, IEnumerable<Assembly> assemblies)
        {
            services.AddSingleton<ICommandDispatcher, CommandDispatcher>();

            services.Scan(typeSourceSelector => typeSourceSelector.FromAssemblies(assemblies)
                .AddClasses(filter => filter.AssignableTo(typeof(ICommandHandler<,>))
                    /*.WithAttribute<DecoratorAttribute>()*/)
                .AsImplementedInterfaces()
                .WithScopedLifetime());

            services.Scan(typeSourceSelector => typeSourceSelector.FromAssemblies(assemblies)
                .AddClasses(filter => filter.AssignableTo(typeof(ICommandHandler<>))
                    /*.WithAttribute<DecoratorAttribute>()*/)
                .AsImplementedInterfaces()
                .WithScopedLifetime());

            return services;
        }
    }
}