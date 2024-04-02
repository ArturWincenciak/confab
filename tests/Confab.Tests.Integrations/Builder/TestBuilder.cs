using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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

namespace Confab.Tests.Integrations.Builder;

public class TestBuilder : IDisposable
{
    private readonly static SignUpDto SignUpUser = new()
    {
        Email = "email@email.com",
        Password = "strict",
        Role = "user",
        Claims = new()
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

    private readonly static SignInDto SignInUser = new()
    {
        Email = SignUpUser.Email,
        Password = SignUpUser.Password
    };

    private readonly static HostDto Host = new()
    {
        Name = "Fowler Host",
        Description = "Description of Fowler Host"
    };

    private static Uri _createdHostLocation;

    private readonly static AgendasController.CreateAgendaTrackCommand CreateTrackCommand =
        new("Robert C. Martin Track");

    private readonly static DateTime StartConferenceTime =
        new(year: 2022, month: 4, day: 5, hour: 9, minute: 0, second: 0);

    private readonly static DateTime EndConferenceTime =
        new(year: 2022, month: 4, day: 5, hour: 17, minute: 0, second: 0);

    private readonly static DateTime StartFirstSlot = StartConferenceTime;
    private readonly static DateTime EndFirstSlot = StartConferenceTime.AddHours(1);
    private readonly List<Func<Task>> _actions = new();

    private readonly SpeakerDto _inputSpeakerDto = new()
    {
        FullName = "Konrad Kokosa",
        Email = "konrad@kokosa.email",
        Bio = "Garbage Collector Master of the Masters",
        AvatarUrl = "https://konrad.kokosa.site.com"
    };

    private HttpClient _client;
    private Uri _createdConferenceLocation;

    private IServiceCollection _servicesCollection;

    private string _slotResourceId;

    private string _trackResourceId;

    private CreateAgendaSlot CreateRegularSlotCommand => new(AgendaTrackId: ResolveTrackId(), StartFirstSlot,
        EndFirstSlot, ParticipantsLimit: 50, Type: "regular");

    public void Dispose()
    {
        Db.EnsureDatabaseDeleted(_servicesCollection);
        _client.Dispose();
    }

    private static Configuration DbConnectionString(string dbName) =>
        new(Key: "postgres:connectionString", Value: $"Host=localhost;Database={dbName};Username=postgres;Password=");

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

        return new(
            _client,
            SignUpUser,
            Host,
            _createdHostLocation,
            inputConferenceDto: ArrangeConference(),
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

    private ConferenceDetailsDto ArrangeConference() =>
        new()
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
            _client.DefaultRequestHeaders.Authorization = new(scheme: "Bearer", jwt.AccessToken);
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
}