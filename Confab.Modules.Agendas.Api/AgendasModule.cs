using System.Collections.Generic;
using Confab.Modules.Agendas.Application;
using Confab.Modules.Agendas.Domain;
using Confab.Modules.Agendas.Infrastructure;
using Confab.Shared.Abstractions.Modules;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Confab.Modules.Agendas.Api
{
    internal class AgendasModule : IModule
    {
        public string BasePath = "agendas-module";

        public string Name { get; } = "agendas";
        public string Path => BasePath;
        public IEnumerable<string> Policies { get; }

        public void Register(IServiceCollection services)
        {
            services
                .AddDomain()
                .AddApplication()
                .AddInfrastructure();
        }

        public void Use(IApplicationBuilder app)
        {
        }
    }
}