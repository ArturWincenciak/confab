using System.Collections.Generic;
using System.Reflection;
using Confab.Shared.Abstractions.Queries;
using Confab.Shared.Infrastructure.Postgres.Decorators;
using Microsoft.Extensions.DependencyInjection;

namespace Confab.Shared.Infrastructure.Queries
{
    internal static class Extensions
    {
        public static IServiceCollection AddQueries(this IServiceCollection services, IEnumerable<Assembly> assemblies)
        {
            services.AddSingleton<IQueryDispatcher, QueryDispatcher>();
            services.Scan(typeSourceSelector => typeSourceSelector.FromAssemblies(assemblies)
                .AddClasses(filter => filter.AssignableTo(typeof(IQueryHandler<,>))
                    .WithoutAttribute<DecoratorAttribute>())
                .AsImplementedInterfaces()
                .WithScopedLifetime());
            return services;
        }
    }
}