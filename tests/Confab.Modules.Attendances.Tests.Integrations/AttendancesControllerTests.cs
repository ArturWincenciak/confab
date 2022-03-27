using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Confab.Modules.Attendances.Application.Clients.Agendas;
using Confab.Modules.Attendances.Infrastructure.EF;
using Confab.Shared.Tests;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using Shouldly;
using Xunit;

namespace Confab.Modules.Attendances.Tests.Integrations
{
    public class AttendancesControllerTests :
        IClassFixture<TestApplicationFactory>,
        IClassFixture<TestAttendancesDbContext>
    {
        private const string Path = "attendances-module/attendances";
        private readonly HttpClient _client;
        private readonly AttendancesDbContext _dbContext;
        private readonly IAgendasApiClient _agendasApiClient;

        public AttendancesControllerTests(TestApplicationFactory appFactory, TestAttendancesDbContext dbContext)
        {
            _agendasApiClient = Substitute.For<IAgendasApiClient>();

            _client = appFactory
                .WithWebHostBuilder(builder => builder.ConfigureServices(services =>
                {
                    services.AddSingleton(_agendasApiClient);
                }))
                .CreateClient();
            _dbContext = dbContext.DbContext;
        }

        [Fact]
        public async Task Given_Not_Authorized_Http_Client_When_Call_Api_Then_Response_Is_Unauthorized_Http_Status()
        {
            // arrange
            var notRelevantSomeId = "9D933F52-4D05-4B3C-9250-C0551FB9275E";
            var notRelevantSomeConferenceId = Guid.Parse(notRelevantSomeId);

            // act
            var response = await _client.GetAsync($"{Path}/{notRelevantSomeConferenceId}");

            // assert
            response.StatusCode.ShouldBe(HttpStatusCode.Unauthorized);
        }

        [Fact]
        public async Task Given_Not_Existing_Conference_Id_When_Get_Conference_Than_Not_Found_Http_Status()
        {
            // arrange
            var target = new TestBuilder()
                .WithAuthentication()
                .Build();

            // act
            var actual = await target.GetNotExistingConference();

            // assert
            actual.ShouldBeNotFound();
        }
    }
}