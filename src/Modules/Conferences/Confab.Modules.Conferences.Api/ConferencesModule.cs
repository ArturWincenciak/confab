﻿using System.Collections.Generic;
using Confab.Modules.Conferences.Api.Controllers;
using Confab.Modules.Conferences.Core;
using Confab.Shared.Abstractions.Modules;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Confab.Modules.Conferences.Api;

internal class ConferencesModule : IModule
{
    public const string BasePath = "conferences-module";
    public string Name { get; } = "Conferences";
    public string Path { get; } = BasePath;

    public IEnumerable<string> Policies { get; } = new[]
    {
        ConferencesController.Policy,
        HostsController.Policy
    };

    public void Register(IServiceCollection services)
    {
        services.AddCore();
    }

    public void Use(IApplicationBuilder app)
    {
    }
}