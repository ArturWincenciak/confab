using System.Net;
using System.Net.Http;
using Shouldly;

namespace Confab.Modules.Attendances.Tests.Integrations.Builder
{
    public class TestResult
    {
        private readonly HttpResponseMessage _httpResponse;

        public TestResult(HttpResponseMessage httpResponse)
        {
            _httpResponse = httpResponse;
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
    }
}