using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Confab.Modules.Conferences.Core.DTO;
using Confab.Modules.Users.Core.DTO;
using Shouldly;
using Xunit;

namespace Confab.Tests.Integrations.Builder
{
    internal class TestResult
    {
        private HttpResponseMessage _httpResponse;

        private readonly SignUpDto _signUpUser;
        private readonly HostDto _host;
        private readonly ConferenceDetailsDto _conference;

        internal TestResult(SignUpDto signUpUser, HostDto host, ConferenceDetailsDto conference)
        {
            _signUpUser = signUpUser;
            _host = host;
            _conference = conference;
        }

        internal TestResult WithHttpResponse(HttpResponseMessage httpResponse)
        {
            _httpResponse = httpResponse;
            return this;
        }

        private async Task<T> Response<T>()
        {
            return await _httpResponse.Content.ReadFromJsonAsync<T>();
        }

        internal void ShouldBeNotFound404()
        {
            _httpResponse.StatusCode.ShouldBe(HttpStatusCode.NotFound);
        }

        internal void ShouldBeUnauthorized401()
        {
            _httpResponse.StatusCode.ShouldBe(HttpStatusCode.Unauthorized);
        }

        internal void ShouldBeNoContent204()
        {
            _httpResponse.StatusCode.ShouldBe(HttpStatusCode.NoContent);
        }

        internal void ShouldBeOk200()
        {
            _httpResponse.StatusCode.ShouldBe(HttpStatusCode.OK);
        }

        internal void ShouldBeCreated201()
        {
            _httpResponse.StatusCode.ShouldBe(HttpStatusCode.Created);
        }

        internal async Task SignedInUserShouldBeSignedUpUser()
        {
            var signedUpUser = await Response<AccountDto>();
            Assert.Equal(_signUpUser.Email, signedUpUser.Email);
            Assert.Equal(_signUpUser.Role, signedUpUser.Role);
            Assert.Equal(_signUpUser.Claims, signedUpUser.Claims);
        }

        internal async Task HostShouldBeCreatedProperly()
        {
            var host = await Response<HostDetailsDto>();
            Assert.NotEqual(Guid.Empty, host.Id);
            Assert.Equal(0, host.Conferences.Count);
            Assert.Equal(_host.Name, host.Name);
            Assert.Equal(_host.Description, host.Description);
        }

        internal async Task ConferenceShouldBeCreatedProperly()
        {
            var conference = await Response<ConferenceDetailsDto>();
            Assert.NotEqual(Guid.Empty, conference.Id);
            Assert.Equal(_conference.HostId, conference.HostId);
            Assert.Equal(_conference.Name, _conference.Name);
            Assert.Equal(_conference.Description, conference.Description);
            Assert.Equal(_conference.From, conference.From);
            Assert.Equal(_conference.To, conference.To);
            Assert.Equal(_conference.Localization, conference.Localization);
            Assert.Equal(_conference.LogoUrl, conference.LogoUrl);
            Assert.Equal(_conference.ParticipantsLimit, conference.ParticipantsLimit);
            Assert.Equal(_host.Name, conference.HostName);
        }
    }
}