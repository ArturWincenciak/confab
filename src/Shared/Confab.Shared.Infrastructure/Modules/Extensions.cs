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
                var allSettings = GetSettings("*");
                foreach (var settings in allSettings)
                    cfg.AddJsonFile(settings);

                Console.WriteLine($"\n\nAll configurations settings added:\n* {string.Join("\n* ", allSettings)}");

                var environmentName = ctx.HostingEnvironment.EnvironmentName;
                var fallbackEnvironmentSettings = GetSettings($"*.{environmentName}");
                foreach (var settings in fallbackEnvironmentSettings)
                    cfg.AddJsonFile(settings);

                Console.WriteLine(
                    $"\n\nConfigurations settings for environment '{environmentName}' added:\n* " +
                    $"{string.Join("\n* ", fallbackEnvironmentSettings)}");

                IEnumerable<string> GetSettings(string pattern)
                {
                    return Directory.EnumerateFiles(ctx.HostingEnvironment.ContentRootPath,
                        $"module.{pattern}.json", SearchOption.AllDirectories);
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
            var registry = new ModuleRegistry();

            var types = assemblies.SelectMany(x => x.GetTypes()).ToArray();
            var eventTypes = types
                .Where(x => x.IsClass && typeof(IEvent).IsAssignableFrom(x))
                .ToArray();

            services.AddSingleton<IModuleRegistry>(sp =>
            {
                var eventDispatcher = sp.GetRequiredService<IEventDispatcher>();
                var eventDispatcherType = eventDispatcher.GetType();

                foreach (var eventType in eventTypes)
                    registry.AddBroadcastAction(
                        eventType,
                        @event =>
                        {
                            var methodInfo = eventDispatcherType.GetMethod(nameof(eventDispatcher.PublishAsync));
                            var genericMethodInfo = methodInfo.MakeGenericMethod(eventType);
                            var methodResult = genericMethodInfo.Invoke(eventDispatcher, new[] {@event});
                            return (Task) methodResult;
                        });

                return registry;
            });
        }
    }
}