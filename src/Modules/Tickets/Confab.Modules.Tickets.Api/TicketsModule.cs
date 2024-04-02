﻿using System.Collections.Generic;
using Confab.Modules.Tickets.Api.Controllers;
using Confab.Modules.Tickets.Core;
using Confab.Shared.Abstractions.Modules;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Confab.Modules.Tickets.Api;

internal class TicketsModule : IModule
{
    public const string BasePath = "tickets-module";

    public string Name => "Tickets";

    public string Path => BasePath;

    public IEnumerable<string> Policies { get; } = new[]
    {
        SalesController.Policy
    };

    public void Register(IServiceCollection services)
    {
        services.AddCore();
    }

    public void Use(IApplicationBuilder app)
    {
    }
}