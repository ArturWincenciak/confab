using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using System.Threading.Tasks;
using Confab.Shared.Infrastructure.Modules;

namespace Confab.Bootstrapper
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            Console.WriteLine("\n---\nStarting... creating host builder...\n---\n");

            var builder = CreateHostBuilder(args);

            Console.WriteLine("\n---\nHost builder has been created..., building the host...\n---\n");

            var host = builder.Build();

            Console.WriteLine("\n---\nHost has been built..., running...\n---\n");

            await host.RunAsync();

            Console.WriteLine("\n---\nHost has been run.");
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            Console.WriteLine("\n---\nStarting create host builder method..., creating default builder...");

            var defaultBuilder = Host.CreateDefaultBuilder(args);

            Console.WriteLine("\n---\nDefault host builder has been created..., configuring web host defaults...");

            var configureWebHostDefaults = defaultBuilder.ConfigureWebHostDefaults(webBuilder =>
            {
                Console.WriteLine("\n---\nRunning lambda with setting Startup class " +
                                  "as entry point on host builder...\n---\n");
                webBuilder.UseStartup<Startup>();
            });

            Console.WriteLine("\n---\nWeb host defaults has been configured..., configuring modules...\n---\n");

            var configureModules = configureWebHostDefaults.ConfigureModules();

            Console.WriteLine("\n---\nModules has been configured..., returning the host builder...\n---\n");

            return configureModules;
        }
    }
}
