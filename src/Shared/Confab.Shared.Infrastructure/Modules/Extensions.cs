using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
            endpointRouteBuilder.MapGet(pattern: "modules", requestDelegate: context =>
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
                {
                    cfg.AddJsonFile(settings);
                }

                Console.WriteLine($"\n\nAll configurations settings added:\n* {string.Join("\n* ", allSettings)}");

                var environmentName = ctx.HostingEnvironment.EnvironmentName;
                var fallbackEnvironmentSettings = GetSettings($"*.{environmentName}");
                foreach (var settings in fallbackEnvironmentSettings)
                {
                    cfg.AddJsonFile(settings);
                }

                Console.WriteLine(
                    $"\n\nConfigurations settings for environment '{environmentName}' added:\n* " +
                    $"{string.Join("\n* ", fallbackEnvironmentSettings)}");

                IEnumerable<string> GetSettings(string pattern) =>
                    Directory.EnumerateFiles(path: ctx.HostingEnvironment.ContentRootPath,
                        searchPattern: $"module.{pattern}.json", searchOption: SearchOption.AllDirectories);
            });
        }
    }
}
