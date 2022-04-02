using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Confab.Shared.Tests;

namespace Confab.Modules.Attendances.Tests.Integrations.Builder
{
    public class TestBuilder
    {
        private readonly List<Action> _actions = new();
        private HttpClient _client;

        public TestApplication Build()
        {
            var applicationFactory = new TestApplicationFactory();
            _client = applicationFactory.CreateClient();

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