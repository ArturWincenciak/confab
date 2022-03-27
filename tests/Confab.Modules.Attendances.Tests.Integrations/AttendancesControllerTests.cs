using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Confab.Modules.Attendances.Infrastructure.EF;
using Confab.Shared.Tests;
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

        public AttendancesControllerTests(TestApplicationFactory appFactory, TestAttendancesDbContext dbContext)
        {
            _client = appFactory.CreateClient();
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
            WithAuthentication();
            var notExistsConferenceId = Guid.Parse("5E3D8A6B-14BA-49A1-8736-BAAB10329F0D");

            // act
            var response = await _client.GetAsync($"{Path}/{notExistsConferenceId}");

            // assert
            response.StatusCode.ShouldBe(HttpStatusCode.NotFound);
        }

        private void WithAuthentication()
        {
            var userId = "B4B599B4-AE95-4AAD-8F22-82BB999C9302";
            var jwt = AuthHelper.GenerateJwt(userId);
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwt);
        }
    }
}