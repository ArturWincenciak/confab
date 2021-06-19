using System;
using System.Collections.Generic;
using System.Linq;
using Confab.Shared.Abstractions.Modules;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Confab.Shared.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Confab.Bootstrapper
{
    public class Startup
    {
        private readonly IList<IModule> _modules;

        public Startup()
        {
            var assemblies = ModuleLoader.LoadAssemblies();
            _modules = ModuleLoader.LoadModules(assemblies);
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddInfrastructure();

            foreach (var module in _modules)
            {
                module.Register(services);
            }

            Console.WriteLine(
                $"\n\nConfigured services of modules: " +
                $"{string.Join(" | ", _modules.Select(module => module.Name))}\n\n");
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILogger<Startup> logger)
        {
            app.UseInfrastructure();

            foreach (var module in _modules)
            {
                module.Use(app);
            }

            logger.LogInformation(
                $"Configured app with modules: {string.Join(" | ", _modules.Select(module => module.Name))}");

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapGet("/", async context => await context.Response.WriteAsync("Confab API!"));
            });
        }
    }
}
