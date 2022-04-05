using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Confab.Modules.Attendances.Tests.Integrations.Builder.Api;
using Confab.Modules.Attendances.Tests.Integrations.Infrastructure;
using Confab.Modules.Conferences.Core.DTO;
using Confab.Modules.Users.Core.DTO;
using Confab.Shared.Abstractions.Auth;
using Confab.Shared.Tests;

namespace Confab.Modules.Attendances.Tests.Integrations.Builder
{
    internal class TestBuilder
    {
        private readonly List<Func<Task>> _actions = new();
        private HttpClient _client;
        private bool _ensureDatabaseDeleted = true;

        internal async Task<TestingApplication> Build()
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

            return new TestingApplication(_client, SignUpUser, Host, _createdHostLocation, ArrangeConference());
        }

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

        private static readonly HostDto Host = new()
        {
            Name = "Fowler Host",
            Description = "Description of Fowler Host"
        };

        private static Uri _createdHostLocation;

        private Guid ResolveHostId(Uri hostLocation)
        {
            var id = hostLocation.Segments.Last();
            return Guid.Parse(id);
        }

        private ConferenceDetailsDto ArrangeConference()
        {
            return new()
            {
                HostId = ResolveHostId(_createdHostLocation),
                Name = "Kent Beck Conference",
                Localization = "Melbourne",
                LogoUrl = "http://logo.com/conf1.jpg",
                ParticipantsLimit = 100,
                From = new DateTime(2022, 4, 5, 9, 0, 0),
                To = new DateTime(2022, 4, 5, 17, 0, 0),
                Description = "Description of Kent Back Conference"
            };
        }

        internal TestBuilder WithAuthentication()
        {
            WithSignUp();
            WithSignIn();
            return this;
        }

        internal TestBuilder WithoutEnsureDatabaseDeleted()
        {
            _ensureDatabaseDeleted = false;
            return this;
        }

        internal TestBuilder WithSignUp()
        {
            _actions.Add(SignUp);
            return this;

            async Task SignUp()
            {
                var response = await _client.SignUp(SignUpUser);
                response.EnsureSuccessStatusCode();
            }
        }

        internal TestBuilder WithSignIn()
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

        public TestBuilder WithHost()
        {
            _actions.Add(CreateHost);
            return this;

            async Task CreateHost()
            {
                var response = await _client.CreateHost(Host);
                response.EnsureSuccessStatusCode();
                _createdHostLocation = response.Headers.Location;
            }
        }
    }
}