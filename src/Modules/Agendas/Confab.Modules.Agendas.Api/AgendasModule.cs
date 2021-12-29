using System.Collections.Generic;
using Confab.Modules.Agendas.Api.Controllers;
using Confab.Modules.Agendas.Application;
using Confab.Modules.Agendas.Application.Agendas.Queries;
using Confab.Modules.Agendas.Domain;
using Confab.Modules.Agendas.Infrastructure;
using Confab.Shared.Abstractions.Modules;
using Confab.Shared.Infrastructure.Modules;
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
            AgendasController.Policy,
            CallForPapersController.Policy,
            SubmissionsController.Policy
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
            //todo: consider rename 'Subscribe' to 'MapRequest'
            app.UseModuleRequests() //todo: path can be generate by nameof(T)
                .Subscribe<GetRegularAgendaSlot, GetRegularAgendaSlot.Result>($"{Name}/{nameof(GetRegularAgendaSlot)}")
                .Subscribe<GetAgenda, GetAgenda.Result>($"{Name}/{nameof(GetAgenda)}");

            //app.UseModuleRequests()
            //    .Subscribe<GetRegularAgendaSlot, GetRegularAgendaSlot.Result>("GET:agendas/slots?type=regular",
            //        (request, sp) => sp.GetRequiredService<IQueryDispatcher>().QueryAsync(request));
        }
    }
}