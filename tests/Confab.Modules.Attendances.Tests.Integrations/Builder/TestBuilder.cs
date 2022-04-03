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
    public class TestBuilder : IDisposable
    {
        private readonly List<Action> _actions = new();
        private HttpClient _client;
        private bool _ensureDatabaseDeleted = false;
        private static IServiceCollection _serviceCollection;

        public TestApplication Build()
        {
            _client = new TestApplicationFactory()
                .WithWebHostBuilder(builder =>
                {
                    builder.ConfigureServices(services =>
                    {
                        _serviceCollection = services;

                        if(_ensureDatabaseDeleted)
                            EnsureDatabaseDeleted(services);
                    });
                })
                .CreateClient();

            foreach (var action in _actions)
                action();

            return new TestApplication(_client, Dispose);
        }

        private static void EnsureDatabaseDeleted(IServiceCollection services)
        {
            var serviceProvider = _serviceCollection.BuildServiceProvider();
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

        public TestBuilder WithEnsureDatabaseDeleted()
        {
            _ensureDatabaseDeleted = true;
            return this;
        }

        public void Dispose()
        {
            if(_ensureDatabaseDeleted)
                EnsureDatabaseDeleted(_serviceCollection);

            _client?.Dispose();
        }
    }
}