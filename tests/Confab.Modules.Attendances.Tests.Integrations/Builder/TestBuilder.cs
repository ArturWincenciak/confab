using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Confab.Modules.Attendances.Tests.Integrations.Builder.Api;
using Confab.Modules.Attendances.Tests.Integrations.Infrastructure;
using Confab.Modules.Users.Core.DTO;
using Confab.Shared.Abstractions.Auth;
using Confab.Shared.Tests;

namespace Confab.Modules.Attendances.Tests.Integrations.Builder
{
    public class TestBuilder
    {
        private readonly List<Func<Task>> _actions = new();
        private HttpClient _client;
        private bool _ensureDatabaseDeleted = true;

        private static readonly SignUpDto SignUpUser = new()
        {
            Email = "email@email.com",
            Password = "strict",
            Role = "user",
            Claims = new Dictionary<string, IEnumerable<string>>
            {
                {
                    "permissions",
                    new[]
                    {
                        "conferences", "hosts", "speakers", "users", "agendas", "cfps", "submissions", "ticket-sales"
                    }
                }
            }
        };

        private static readonly SignInDto SignInUser = new()
        {
            Email = SignUpUser.Email,
            Password = SignUpUser.Password
        };

        public async Task<TestApplication> Build()
        {
            _client = new TestApplicationFactory()
                .WithWebHostBuilder(builder =>
                {
                    builder.ConfigureServices(services =>
                    {
                        if (_ensureDatabaseDeleted)
                            Db.EnsureDatabaseDeleted(services);
                    });
                })
                .CreateClient();

            foreach (var action in _actions)
                await action();

            return new TestApplication(_client, SignUpUser);
        }

        public TestBuilder WithAuthentication()
        {
            WithSignUp();
            WithSignIn();
            return this;
        }

        public TestBuilder WithoutEnsureDatabaseDeleted()
        {
            _ensureDatabaseDeleted = false;
            return this;
        }

        public TestBuilder WithSignUp()
        {
            _actions.Add(SignUp);
            return this;

            async Task SignUp()
            {
                var response = await _client.SignUp(SignUpUser);
                response.EnsureSuccessStatusCode();
            }
        }

        public TestBuilder WithSignIn()
        {
            _actions.Add(SignIn);
            return this;

            async Task SignIn()
            {
                var response = await _client.SignIn(SignInUser);
                response.EnsureSuccessStatusCode();
                var jwt = await response.Content.ReadFromJsonAsync<JsonWebToken>();
                _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwt.AccessToken);
            }
        }
    }
}