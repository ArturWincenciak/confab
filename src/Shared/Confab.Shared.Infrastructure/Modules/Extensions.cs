using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Confab.Shared.Abstractions.Events;
using Confab.Shared.Abstractions.Modules;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Confab.Shared.Infrastructure.Modules
{
    public static class Extensions
    {
        internal static IServiceCollection AddModuleInfo(this IServiceCollection services, IList<IModule> modules)
        {
            var moduleInfo =
                modules?.Select(x => new ModuleInfo(x.Name, x.Path, x.Policies ?? Enumerable.Empty<string>())) ??
                Enumerable.Empty<ModuleInfo>();
            var moduleInfoProvider = new ModuleInfoProvider(moduleInfo);
            services.AddSingleton(moduleInfoProvider);

            return services;
        }

        internal static void MapModuleInfo(this IEndpointRouteBuilder endpointRouteBuilder)
        {
            endpointRouteBuilder.MapGet("modules", context =>
            {
                var moduleInfoProvider = context.RequestServices.GetRequiredService<ModuleInfoProvider>();
                return context.Response.WriteAsJsonAsync(moduleInfoProvider.Modules);
            });
        }

        internal static IHostBuilder ConfigureModules(this IHostBuilder builder)
        {
            Console.WriteLine("Configuring modules setting by host builder configuration...");

            return builder.ConfigureAppConfiguration((ctx, cfg) =>
            {
                var environment = ctx.HostingEnvironment.EnvironmentName;

                var moduleMainAppSettingsJsonFiles = GetModulesMainAppSettingsJson();
                foreach (var appSettingsJson in moduleMainAppSettingsJsonFiles)
                {
                    cfg.AddJsonFile(appSettingsJson);
                    Console.WriteLine($"Added module main app setting Json: {appSettingsJson}.");
                }

                var fallbackEnvironmentSettings = GetModulesFallbackEnvAppSettingsJson(environment);
                foreach (var appSettingsJson in fallbackEnvironmentSettings)
                {
                    cfg.AddJsonFile(appSettingsJson);
                    Console.WriteLine($"Added module fallback env app setting Json: {appSettingsJson}.");
                }

                string[] GetModulesMainAppSettingsJson()
                {
                    return Directory.EnumerateFiles(ctx.HostingEnvironment.ContentRootPath, "module.*.json",
                            SearchOption.AllDirectories)
                        .Where(file =>
                        {
                            var fileName = Path.GetFileName(file);
                            var fileNameParts = fileName.Split(".");
                            var fileContainsOnlyTwoMainPart = fileNameParts.Length == 3;
                            return fileContainsOnlyTwoMainPart;
                        })
                        .ToArray();
                }

                string[] GetModulesFallbackEnvAppSettingsJson(string env)
                {
                    return Directory.EnumerateFiles(ctx.HostingEnvironment.ContentRootPath, "module.*.json",
                            SearchOption.AllDirectories)
                        .Where(file =>
                        {
                            var fileName = Path.GetFileName(file);
                            var fileNameParts = fileName.Split(".");
                            var containsEnvironment = fileNameParts.Contains(env.ToLower());
                            var fileContainsTreeMainParts = fileNameParts.Length == 4;
                            return containsEnvironment && fileContainsTreeMainParts;
                        })
                        .ToArray();
                }
            });
        }

        internal static IServiceCollection AddModuleRequests(this IServiceCollection services,
            IEnumerable<Assembly> assemblies)
        {
            services.AddModuleRegistry(assemblies);
            services.AddSingleton<IModuleClient, ModuleClient>();
            services.AddSingleton<IModuleSerializer, JsonModuleSerializer>();
            return services;
        }

        private static void AddModuleRegistry(this IServiceCollection services, IEnumerable<Assembly> assemblies)
        {
            Console.WriteLine("\n\nAdding module registry...\n\n");

            var registry = new ModuleRegistry();

            var types = assemblies.SelectMany(x => x.GetTypes()).ToArray();
            var eventTypes = types
                .Where(x => x.IsClass && typeof(IEvent).IsAssignableFrom(x))
                .ToArray();

            services.AddSingleton<IModuleRegistry>(sp =>
            {
                Console.WriteLine("\n\nResolving module registry...");
                var eventDispatcher = sp.GetRequiredService<IEventDispatcher>();
                var eventDispatcherType = eventDispatcher.GetType();

                foreach (var eventType in eventTypes)
                {
                    registry.AddBroadcastAction(eventType,
                    @event =>
                    {
                        try
                        {
                            Console.WriteLine($"[Trace] Invoking weaved action for event: '{@event}'.");
                            var methodInfo = eventDispatcherType.GetMethod(nameof(eventDispatcher.PublishAsync));
                            var genericMethodInfo = methodInfo.MakeGenericMethod(eventType);
                            var methodResult = genericMethodInfo.Invoke(eventDispatcher, new[] {@event});
                            return (Task) methodResult;
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Exception: '{ex.Message}'; Event: '{@event}'.");
                            throw;
                        }
                    });

                    Console.WriteLine(
                        $"[Trace] Module registry item added for event type: '{eventType.FullName}'.");
                }

                return registry;
            });
        }
    }
}