using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Confab.Modules.Attendances.Tests.Integrations.Builder
{
    public class TestApplication
    {
        private readonly HttpClient _api;

        public TestApplication(HttpClient api)
        {
            _api = api;
        }

        public async Task<TestResult> GetNotExistingConference()
        {
            var notExistingConferenceId = Guid.Parse("1E795B8E-A3F1-4E1A-BB94-435BC707F03C");
            var response = await _api.GetConferenceAsync(notExistingConferenceId);

            return new TestResult(response);
        }

        public async Task<TestResult> CreateUser()
        {
            var response = await _api.CreateUserAsync();
            return new TestResult(response);
        }
    }
}