﻿using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Confab.Modules.Agendas.Api.Controllers;
using Confab.Modules.Agendas.Application;
using Confab.Modules.Agendas.Application.Agendas.Queries;
using Confab.Modules.Agendas.Domain;
using Confab.Modules.Agendas.Infrastructure;
using Confab.Shared.Abstractions.Modules;
using Confab.Shared.Infrastructure.Modules;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

[assembly: InternalsVisibleTo("Confab.Tests.Integrations")]

namespace Confab.Modules.Agendas.Api;

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
        app.UseModuleRequests()
            .MapRequest<GetRegularAgendaSlot, GetRegularAgendaSlot.Result>($"{Name}/{nameof(GetRegularAgendaSlot)}")
            .MapRequest<GetAgenda, GetAgenda.Result>($"{Name}/{nameof(GetAgenda)}");
    }
}