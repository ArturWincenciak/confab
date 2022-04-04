using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Confab.Modules.Users.Core.DTO;
using Shouldly;
using Xunit;

namespace Confab.Modules.Attendances.Tests.Integrations.Builder
{
    public class TestResult
    {
        private HttpResponseMessage _httpResponse;

        private readonly SignUpDto _signUpUser;

        public TestResult(SignUpDto signUpUser)
        {
            _signUpUser = signUpUser;
        }

        public TestResult WithHttpResponse(HttpResponseMessage httpResponse)
        {
            _httpResponse = httpResponse;
            return this;
        }

        public void ShouldBeNotFound404()
        {
            _httpResponse.StatusCode.ShouldBe(HttpStatusCode.NotFound);
        }

        public void ShouldBeUnauthorized401()
        {
            _httpResponse.StatusCode.ShouldBe(HttpStatusCode.Unauthorized);
        }

        public void ShouldBeNoContent204()
        {
            _httpResponse.StatusCode.ShouldBe(HttpStatusCode.NoContent);
        }

        public void ShouldBeOk200()
        {
            _httpResponse.StatusCode.ShouldBe(HttpStatusCode.OK);
        }

        public async Task SignedInUserShouldBeSignedUpUser()
        {
            var signedUpUser = await _httpResponse.Content.ReadFromJsonAsync<AccountDto>();
            Assert.Equal(_signUpUser.Email, signedUpUser.Email);
            Assert.Equal(_signUpUser.Role, signedUpUser.Role);
            Assert.Equal(_signUpUser.Claims, signedUpUser.Claims);
        }
    }
}