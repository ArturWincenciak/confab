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

        public void ShouldBeNotFound()
        {
            _httpResponse.StatusCode.ShouldBe(HttpStatusCode.NotFound);
        }

        public void ShouldBeUnauthorized()
        {
            _httpResponse.StatusCode.ShouldBe(HttpStatusCode.Unauthorized);
        }
    }
}