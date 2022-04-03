using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using Confab.Modules.Attendances.Tests.Integrations.Builder.Api;
using Confab.Shared.Tests;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Confab.Modules.Attendances.Tests.Integrations.Builder
{
    public class TestBuilder
    {
        private readonly List<Action> _actions = new();
        private HttpClient _client;

        public TestApplication Build()
        {
            _client = new TestApplicationFactory()
                .WithWebHostBuilder(builder =>
                {
                    builder.ConfigureServices(services =>
                    {
                        EnsureDatabaseDeleted(services);
                    });
                })
                .CreateClient();


            foreach (var action in _actions)
                action();

            return new TestApplication(_client);
        }

        private static void EnsureDatabaseDeleted(IServiceCollection services)
        {
            var serviceProvider = services.BuildServiceProvider();
            using var scope = serviceProvider.CreateScope();
            var scopedServices = scope.ServiceProvider;
            var dbContextType = GetSomeFirstDbContextType();
            var db = scopedServices.GetRequiredService(dbContextType) as DbContext;
            db.Database.EnsureDeleted();
        }

        private static Type GetSomeFirstDbContextType()
        {
            var dbContextType = AppDomain.CurrentDomain
                .GetAssemblies()
                .SelectMany(x => x.GetTypes())
                .First(x =>
                    typeof(DbContext).IsAssignableFrom(x) &&
                    !x.IsInterface &&
                    x != typeof(DbContext));
            return dbContextType;
        }

        public TestBuilder WithAuthentication()
        {
            _actions.Add(Authenticate);
            return this;

            void Authenticate()
            {
                var userId = "B4B599B4-AE95-4AAD-8F22-82BB999C9302";
                var jwt = AuthHelper.GenerateJwt(userId);
                _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwt);
            }
        }
    }
}