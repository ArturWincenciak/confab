using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using Confab.Shared.Abstractions;
using Confab.Shared.Abstractions.Modules;
using Confab.Shared.Abstractions.Storage;
using Confab.Shared.Infrastructure.Api;
using Confab.Shared.Infrastructure.Auth;
using Confab.Shared.Infrastructure.Commands;
using Confab.Shared.Infrastructure.Contexts;
using Confab.Shared.Infrastructure.Events;
using Confab.Shared.Infrastructure.Exceptions;
using Confab.Shared.Infrastructure.Kernel;
using Confab.Shared.Infrastructure.Messaging;
using Confab.Shared.Infrastructure.Modules;
using Confab.Shared.Infrastructure.Postgres;
using Confab.Shared.Infrastructure.Queries;
using Confab.Shared.Infrastructure.Services;
using Confab.Shared.Infrastructure.Storage;
using Confab.Shared.Infrastructure.Time;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: InternalsVisibleTo("Confab.Bootstrapper")]
[assembly: InternalsVisibleTo("Confab.Shared.Tests")]

namespace Confab.Shared.Infrastructure;

internal static class Extensions
{
    private const string CorsPolicy = "cors";

    public static IServiceCollection AddInfrastructure(this IServiceCollection services,
        IList<IModule> modules,
        IEnumerable<Assembly> assemblies)
    {
        services.AddCors(corsOption =>
        {
            corsOption.AddPolicy(CorsPolicy, configurePolicy: builder =>
            {
                builder
                    .WithOrigins("*")
                    .WithMethods("POST", "PUT", "DELETE")
                    .WithHeaders("Content-Type", "Authorization");
            });
        });

        services.AddSwaggerGen(options =>
        {
            options.CustomSchemaIds(type => type.FullName);
            options.SwaggerDoc(name: "v1", info: new()
            {
                Title = "Confab API",
                Version = "v1"
            });
        });

        services.AddMemoryCache();
        services.AddSingleton<IRequestStorage, RequestStorage>();
        services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        services.AddSingleton<IContextFactory, ContextFactory>();
        services.AddTransient(sp => sp.GetRequiredService<IContextFactory>().Create());
        services.AddModuleInfo(modules);
        services.AddModuleRequests(assemblies);
        services.AddAuth(modules);
        services.AddErrorHandling();
        services.AddCommands(assemblies);
        services.AddMessaging();
        services.AddEvents(assemblies);
        services.AddDomainEvents(assemblies);
        services.AddQueries(assemblies);
        services.AddPostgresOptions();
        services.AddSingleton<IClock, UtcClock>();
        services.AddHostedService<AppInitializer>();

        services.AddControllers()
            .ConfigureApplicationPartManager(manager =>
            {
                var disabledModules = DetectDisabledModules(services);
                manager.AddOnlyNotDisabledModuleParts(disabledModules);
            });

        return services;
    }

    public static IApplicationBuilder UseInfrastructure(this IApplicationBuilder app)
    {
        app.UseCors(CorsPolicy);
        app.UseErrorHandling();

        app.UseSwagger();
        app.UseSwaggerUI(c =>
            c.SwaggerEndpoint(url: "/swagger/v1/swagger.json", name: "Confab Api"));
        app.UseReDoc(options =>
        {
            options.RoutePrefix = "docs";
            options.SpecUrl("/swagger/v1/swagger.json");
            options.DocumentTitle = "Confab API";
        });

        app.UseAuthentication();
        app.UseRouting();
        app.UseAuthorization();

        return app;
    }

    public static string GetModuleName(this Type type)
    {
        if (string.IsNullOrEmpty(type.Namespace))
            return string.Empty;

        return type.Namespace.StartsWith("Confab.Modules.")
            ? type.Namespace.Split(".")[2].ToLowerInvariant()
            : string.Empty;
    }

    public static T GetOptions<T>(this IServiceCollection services, string sectionName) where T : new()
    {
        using var serviceProvider = services.BuildServiceProvider();
        var configuration = serviceProvider.GetService<IConfiguration>();
        return configuration.GetOptions<T>(sectionName);
    }

    private static T GetOptions<T>(this IConfiguration configuration, string sectionName) where T : new()
    {
        var options = new T();
        configuration.GetSection(sectionName).Bind(options);
        return options;
    }

    private static IEnumerable<string> DetectDisabledModules(IServiceCollection services)
    {
        using var serviceProvider = services.BuildServiceProvider();
        var configuration = serviceProvider.GetRequiredService<IConfiguration>();
        var disabledModules = new List<string>();
        foreach (var (key, value) in configuration.AsEnumerable())
        {
            if (!key.Contains(":module:enabled"))
                continue;

            if (!bool.Parse(value))
            {
                var splitKey = key.Split(":");
                var moduleName = splitKey[0];
                disabledModules.Add(moduleName);
            }
        }

        return disabledModules;
    }

    private static ApplicationPartManager AddOnlyNotDisabledModuleParts(this ApplicationPartManager manager,
        IEnumerable<string> disabledModules)
    {
        var removedParts = new List<ApplicationPart>();
        foreach (var disabledModule in disabledModules)
        {
            var parts = manager.ApplicationParts
                .Where(applicationPart => applicationPart.Name.Contains(disabledModule,
                    StringComparison.InvariantCultureIgnoreCase));

            removedParts.AddRange(parts);
        }

        foreach (var part in removedParts)
            manager.ApplicationParts.Remove(part);

        manager.FeatureProviders.Add(new InternalControllerFeatureProvider());
        return manager;
    }
}