using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using Confab.Modules.Attendances.Tests.Integrations.Builder.Api;
using Confab.Shared.Tests;
using Microsoft.Extensions.DependencyInjection;

namespace Confab.Modules.Attendances.Tests.Integrations.Builder
{
    public class TestBuilder
    {
        private readonly List<Action> _actions = new();
        private HttpClient _client;
        private TestApplicationFactory _applicationFactory;

        public TestApplication Build()
        {
            _applicationFactory = new TestApplicationFactory();
            _applicationFactory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(services =>
                {
                    // do something here if needed ...
                    //var serviceProvider = services.BuildServiceProvider();
                    //using var scope = serviceProvider.CreateScope();
                    //var scopedServices = scope.ServiceProvider;
                    //var db = scopedServices.GetRequiredService<UsersDbContext>();

                    //try
                    //{
                    //    Utilities.ReinitializeDbForTests(db);
                    //}
                    //catch (Exception ex)
                    //{

                    //}
                });
            });

            _client = _applicationFactory.CreateClient();

            foreach (var action in _actions)
                action();

            return new TestApplication(_client);
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