using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Confab.Modules.Conferences.Core.DTO;
using Confab.Modules.Users.Core.DTO;
using Shouldly;
using Xunit;

namespace Confab.Modules.Attendances.Tests.Integrations.Builder
{
    internal class TestResult
    {
        private HttpResponseMessage _httpResponse;

        private readonly SignUpDto _signUpUser;
        private readonly HostDto _host;

        internal TestResult(SignUpDto signUpUser, HostDto host)
        {
            _signUpUser = signUpUser;
            _host = host;
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
            var createdHost = await Response<HostDetailsDto>();
            Assert.NotEqual(Guid.Empty, createdHost.Id);
            Assert.Equal(0, createdHost.Conferences.Count);
            Assert.Equal(_host.Name, createdHost.Name);
            Assert.Equal(_host.Description, createdHost.Description);
        }
    }
}