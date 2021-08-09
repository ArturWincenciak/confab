using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace Confab.Shared.Infrastructure.Modules
{
    public static class Extensions
    {
        internal static IHostBuilder ConfigureModules(this IHostBuilder builder)
        {
            Console.WriteLine("Configuring modules setting by host builder configuration...");

            return builder.ConfigureAppConfiguration((ctx, cfg) =>
            {
                var allSettings = GetSettings("*");
                foreach (var settings in allSettings)
                {   //todo: here also will be set config with env name and can override mean config, consider this
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
