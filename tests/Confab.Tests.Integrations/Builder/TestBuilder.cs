using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Confab.Modules.Agendas.Api.Controllers;
using Confab.Modules.Agendas.Application.Agendas.Commands;
using Confab.Modules.Conferences.Core.DTO;
using Confab.Modules.Speakers.Core.DTO;
using Confab.Modules.Users.Core.DTO;
using Confab.Shared.Abstractions.Auth;
using Confab.Shared.Tests;
using Confab.Tests.Integrations.Builder.Api;
using Confab.Tests.Integrations.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace Confab.Tests.Integrations.Builder
{
    public class TestBuilder : IDisposable
    {
        private Uri _createdConferenceLocation;
        private readonly List<Func<Task>> _actions = new();
        private HttpClient _client;

        private IServiceCollection _servicesCollection;

        private static Configuration DbConnectionString(string dbName) =>
            new("postgres:connectionString", $"Host=localhost;Database={dbName};Username=postgres;Password=");

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

        private static readonly AgendasController.CreateAgendaTrackCommand CreateTrackCommand =
            new("Robert C. Martin Track");

        private CreateAgendaSlot CreateRegularSlotCommand => new(AgendaTrackId: ResolveTrackId(), From: StartFirstSlot,
            To: EndFirstSlot, 50, "regular");

        private string _trackResourceId;

        private static readonly DateTime StartConferenceTime = new(2022, 4, 5, 9, 0, 0);
        private static readonly DateTime EndConferenceTime = new(2022, 4, 5, 17, 0, 0);
        private static readonly DateTime StartFirstSlot = StartConferenceTime;
        private static readonly DateTime EndFirstSlot = StartConferenceTime.AddHours(1);

        private string _slotResourceId;

        private readonly SpeakerDto _inputSpeakerDto = new()
        {
            FullName = "Konrad Kokosa",
            Email = "konrad@kokosa.email",
            Bio = "Garbage Collector Master of the Masters",
            AvatarUrl = "https://konrad.kokosa.site.com"
        };

        internal async Task<TestingApplication> Build([CallerMemberName] string callerName = "Unknown")
        {
            _client = new TestApplicationFactory()
                .WithSetting(DbConnectionString($"Test_{callerName}"))
                .WithWebHostBuilder(builder =>
                {
                    builder.ConfigureServices(services =>
                    {
                        _servicesCollection = services;
                        Db.EnsureDatabaseDeleted(_servicesCollection);
                    });
                })
                .CreateClient();

            foreach (var action in _actions)
                await action();

            return new TestingApplication(
                _client,
                SignUpUser,
                Host,
                _createdHostLocation,
                ArrangeConference(),
                _createdConferenceLocation,
                CreateTrackCommand,
                _trackResourceId,
                CreateRegularSlotCommand,
                _slotResourceId,
                _inputSpeakerDto);
        }

        private Guid ResolveHostId(Uri hostLocation)
        {
            var id = hostLocation?.Segments.Last();
            if (id is null)
                return Guid.Empty;

            return Guid.Parse(id);
        }

        private Guid ResolveTrackId()
        {
            if (string.IsNullOrWhiteSpace(_trackResourceId))
                return Guid.Empty;

            return Guid.Parse(_trackResourceId);
        }

        private ConferenceDetailsDto ArrangeConference()
        {

            return new ConferenceDetailsDto
            {
                HostId = ResolveHostId(_createdHostLocation),
                Name = "Kent Beck Conference",
                Localization = "Melbourne",
                LogoUrl = "http://logo.com/conf1.jpg",
                ParticipantsLimit = 100,
                From = StartConferenceTime,
                To = EndConferenceTime,
                Description = "Description of Kent Back Conference"
            };
        }

        internal TestBuilder WithAuthentication()
        {
            WithSignUp();
            WithSignIn();
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

        public TestBuilder WithConference()
        {
            _actions.Add(CreateConference);
            return this;

            async Task CreateConference()
            {
                var response = await _client.CreateConference(ArrangeConference());
                response.EnsureSuccessStatusCode();
                _createdConferenceLocation = response.Headers.Location;
            }
        }

        public TestBuilder WithTrack()
        {
            _actions.Add(CreateTrack);
            return this;

            async Task CreateTrack()
            {
                var conferenceId = Guid.Parse(_createdConferenceLocation.Segments.Last());
                var response = await _client.CreateTrack(conferenceId, CreateTrackCommand);
                response.EnsureSuccessStatusCode();
                _trackResourceId = response.Headers.GetValues("Resource-ID").Single();
            }
        }

        public TestBuilder WithRegularSlot()
        {
            _actions.Add(CreateRegularSlot);
            return this;

            async Task CreateRegularSlot()
            {
                var conferenceId = Guid.Parse(_createdConferenceLocation.Segments.Last());
                var response = await _client.CreateSlot(conferenceId, CreateRegularSlotCommand);
                response.EnsureSuccessStatusCode();
                _slotResourceId = response.Headers.GetValues("Resource-ID").Single();
            }
        }

        public void Dispose()
        {
            Db.EnsureDatabaseDeleted(_servicesCollection);
            _client.Dispose();
        }
    }
}