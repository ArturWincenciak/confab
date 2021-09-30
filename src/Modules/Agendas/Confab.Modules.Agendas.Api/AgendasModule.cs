using System.Collections.Generic;
using Confab.Modules.Agendas.Api.Controllers;
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
        public const string BasePath = "agendas-module";

        public string Name => "Agendas";
        public string Path => BasePath;

        public IEnumerable<string> Policies => new[]
        {
            AgendasController.Policy, CallForPapersController.Policy, "submissions"
        };

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