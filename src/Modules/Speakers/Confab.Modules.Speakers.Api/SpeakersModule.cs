using System.Collections.Generic;
using Confab.Modules.Speakers.Api.Controllers;
using Confab.Modules.Speakers.Core;
using Confab.Modules.Speakers.Core.DTO;
using Confab.Shared.Abstractions.Modules;
using Confab.Shared.Infrastructure.Modules;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Confab.Modules.Speakers.Api
{
    internal class SpeakersModule : IModule
    {
        public const string BasePath = "speakers-module";
        public string Name { get; } = "Speakers";
        public string Path { get; } = BasePath;

        public IEnumerable<string> Policies { get; } = new[]
        {
            SpeakersController.Policy
        };

        public void Register(IServiceCollection services)
        {
            services.AddCore();
        }

        public void Use(IApplicationBuilder app)
        {
            app
                .UseModuleRequests()
                .MapRequest<SpeakerDto, Null>("speakers/create");
        }
    }
}