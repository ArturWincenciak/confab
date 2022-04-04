using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Confab.Modules.Users.Core.DTO;
using Shouldly;
using Xunit;

namespace Confab.Modules.Attendances.Tests.Integrations.Builder
{
    internal class TestResult
    {
        private HttpResponseMessage _httpResponse;

        private readonly SignUpDto _signUpUser;

        internal TestResult(SignUpDto signUpUser)
        {
            _signUpUser = signUpUser;
        }

        internal TestResult WithHttpResponse(HttpResponseMessage httpResponse)
        {
            _httpResponse = httpResponse;
            return this;
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
            var signedUpUser = await _httpResponse.Content.ReadFromJsonAsync<AccountDto>();
            Assert.Equal(_signUpUser.Email, signedUpUser.Email);
            Assert.Equal(_signUpUser.Role, signedUpUser.Role);
            Assert.Equal(_signUpUser.Claims, signedUpUser.Claims);
        }
    }
}