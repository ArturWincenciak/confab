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
            return builder.ConfigureAppConfiguration((ctx, cfg) =>
            {
                var allSettings = GetSettings("*");
                foreach (var settings in allSettings)
                {
                    cfg.AddJsonFile(settings);
                }

                Console.WriteLine(
                    $"\n\nAll configurations settings:\n* {string.Join("\n* ", allSettings)}");

                var fallbackEnvironmentSettings = GetSettings($"*.{ctx.HostingEnvironment.EnvironmentName}");
                foreach (var settings in fallbackEnvironmentSettings)
                {
                    cfg.AddJsonFile(settings);
                }

                Console.WriteLine(
                    $"\n\nEnvironment configurations settings:\n* {string.Join("\n* ", fallbackEnvironmentSettings)}");

                IEnumerable<string> GetSettings(string pattern) =>
                    Directory.EnumerateFiles(ctx.HostingEnvironment.ContentRootPath, $"module.{pattern}.json",
                        SearchOption.AllDirectories);
            });
        }
    }
}
