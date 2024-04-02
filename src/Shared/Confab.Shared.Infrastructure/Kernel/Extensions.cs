﻿using System.Collections.Generic;
using System.Reflection;
using Confab.Shared.Infrastructure.Postgres.Decorators;
using Confab.Shared.Kernel;
using Microsoft.Extensions.DependencyInjection;

namespace Confab.Shared.Infrastructure.Kernel;

internal static class Extensions
{
    public static IServiceCollection AddDomainEvents(this IServiceCollection services,
        IEnumerable<Assembly> assemblies)
    {
        services.AddSingleton<IDomainEventDispatcher, DomainEventDispatcher>();
        services.Scan(selector => selector.FromAssemblies(assemblies)
            .AddClasses(filter => filter.AssignableTo(typeof(IDomainEventHandler<>))
                .WithoutAttribute<DecoratorAttribute>())
            .AsImplementedInterfaces()
            .WithScopedLifetime());
        return services;
    }
}