using System.Collections.Generic;
using System.Reflection;
using Confab.Shared.Abstractions.Events;
using Confab.Shared.Infrastructure.Postgres.Decorators;
using Microsoft.Extensions.DependencyInjection;

namespace Confab.Shared.Infrastructure.Events
{
    internal static class Extensions
    {
        public static IServiceCollection AddEvents(this IServiceCollection services, IEnumerable<Assembly> assemblies)
        {
            services.AddSingleton<IEventDispatcher, EventDispatcher>();
            services.Scan(typeSourceSelector => typeSourceSelector.FromAssemblies(assemblies)
                .AddClasses(implementationTypeFilter => implementationTypeFilter.AssignableTo(typeof(IEventHandler<>))
                    .WithoutAttribute<DecoratorAttribute>())
                .AsImplementedInterfaces()
                .WithScopedLifetime());
            return services;
        }
    }
}