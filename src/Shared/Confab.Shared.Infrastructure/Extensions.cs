using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Confab.Shared.Abstractions;
using Confab.Shared.Infrastructure.Api;
using Confab.Shared.Infrastructure.Exceptions;
using Confab.Shared.Infrastructure.Postgres;
using Confab.Shared.Infrastructure.Services;
using Confab.Shared.Infrastructure.Time;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: InternalsVisibleTo("Confab.Bootstrapper")]

namespace Confab.Shared.Infrastructure
{
    internal static class Extensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            var disabledModules = new List<string>();
            using (var serviceProvider = services.BuildServiceProvider())
            {
                var configuration = serviceProvider.GetRequiredService<IConfiguration>();

                Console.WriteLine("\n\nConfiguration:");
                foreach (var (key, value) in configuration.AsEnumerable())
                {
                    Console.WriteLine($"Key: '{key}', Value: '{value}'");
                    if (!key.Contains(":module:enabled"))
                    {
                        continue;
                    }

                    if (!bool.Parse(value))
                    {
                        var splitKey = key.Split(":");
                        var moduleName = splitKey[0];
                        disabledModules.Add(moduleName);
                        Console.WriteLine(
                            $"---\nDisabled module '{moduleName}' by key '{key}' with value '{value}'\n---");
                    }
                }
            }

            services.AddPostgres();
            services.AddErrorHandling();
            services.AddSingleton<IClock, UtcClock>();
            services.AddHostedService<AppInitializer>();

            services.AddControllers()
                .ConfigureApplicationPartManager(manager =>
                {
                    Console.WriteLine("\nConfigure application part manager starting with parts: " +
                                      $"[{string.Join(" | ", manager.ApplicationParts.Select(x => x.Name))}].");

                    var removedParts = new List<ApplicationPart>();
                    foreach (var disabledModule in disabledModules)
                    {
                        var parts = manager.ApplicationParts
                            .Where(applicationPart => applicationPart.Name.Contains(disabledModule,
                                StringComparison.InvariantCultureIgnoreCase));

                        removedParts.AddRange(parts);
                    }

                    foreach (var part in removedParts)
                    {
                        manager.ApplicationParts.Remove(part);
                    }

                    Console.WriteLine($"Removed application parts: " +
                                      $"[{string.Join(" | ", removedParts.Select(x => x.Name))}].");

                    manager.FeatureProviders.Add(new InternalControllerFeatureProvider());

                    Console.WriteLine("Configure application part manager done.");
                });

            return services;
        }

        public static IApplicationBuilder UseInfrastructure(this IApplicationBuilder app)
        {
            app.UseErrorHandling();
            app.UseRouting();

            return app;
        }

        public static T GetOptions<T>(this IServiceCollection services, string sectionName) where T : new()
        {
            Console.WriteLine($"Build Service Provider by call GetOption of '{typeof(T)}' type to get option '{sectionName}' name.");

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
    }
}
