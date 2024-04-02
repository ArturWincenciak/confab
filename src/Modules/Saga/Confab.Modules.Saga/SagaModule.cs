using System.Collections.Generic;
using Confab.Modules.Saga.Services;
using Confab.Shared.Abstractions.Modules;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Confab.Modules.Saga;

public class SagaModule : IModule
{
    public const string BasePath = "saga-module";

    public string Name { get; } = "Saga";

    public string Path => BasePath;

    public IEnumerable<string> Policies { get; }

    public void Register(IServiceCollection services)
    {
        services.AddSaga();
        services.AddScoped<InvitationSpeakersStub>();
    }

    public void Use(IApplicationBuilder app)
    {
    }
}