using System;
using System.Threading.Tasks;
using Confab.Shared.Infrastructure.Modules;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace Confab.Bootstrapper
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = CreateHostBuilder(args);
            var host = builder.Build();
            await host.RunAsync();
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            var defaultBuilder = Host.CreateDefaultBuilder(args);
            var configureWebHostDefaults = defaultBuilder.ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
            });

            var configureModules = configureWebHostDefaults.ConfigureModules();
            return configureModules;
        }
    }
}