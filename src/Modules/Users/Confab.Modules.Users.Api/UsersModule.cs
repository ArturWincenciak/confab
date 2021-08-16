using System;
using System.Collections.Generic;
using Confab.Shared.Abstractions.Modules;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Confab.Modules.Users.Api
{
    public class UsersModule : IModule
    {
        public string Name { get; }
        public string Path { get; }
        public IEnumerable<string> Policies { get; }
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
