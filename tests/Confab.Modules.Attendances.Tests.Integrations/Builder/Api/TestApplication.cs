using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Confab.Modules.Attendances.Tests.Integrations.Builder.Api
{
    public class TestApplication : IDisposable
    {
        private readonly HttpClient _api;
        private readonly Action _dispose;

        public TestApplication(HttpClient api, Action dispose)
        {
            _api = api;
            _dispose = dispose;
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

        public void Dispose()
        {
            _dispose?.Invoke();
            _api?.Dispose();
        }
    }
}