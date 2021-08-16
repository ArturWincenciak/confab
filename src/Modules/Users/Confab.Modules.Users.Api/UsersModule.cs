using System;
using System.Collections.Generic;
using Confab.Shared.Abstractions.Modules;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Confab.Modules.Users.Api
{
    public class UsersModule : IModule
    {
        public const string BasePath = "users-module";
        public string Name => "Users";
        public string Path => BasePath;
        public IEnumerable<string> Policies { get; } = new[] {"users"};

        public void Register(IServiceCollection services)
        {
            throw new NotImplementedException();
        }

        public void Use(IApplicationBuilder app)
        {
            throw new NotImplementedException();
        }
    }
}
