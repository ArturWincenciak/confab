using System;
using System.Net.Http;
using System.Threading.Tasks;
using Confab.Modules.Users.Core.DTO;

namespace Confab.Modules.Attendances.Tests.Integrations.Builder.Api
{
    public class TestApplication
    {
        private readonly TestResult _testResult;
        private readonly HttpClient _api;
        private readonly SignUpDto _signUpUser;

        public TestApplication(HttpClient api, SignUpDto signUpUser)
        {
            _api = api;
            _signUpUser = signUpUser;
            _testResult = new TestResult(signUpUser);
        }

        public async Task<TestResult> GetNotExistingConference()
        {
            var notExistingConferenceId = Guid.Parse("1E795B8E-A3F1-4E1A-BB94-435BC707F03C");
            var response = await _api.GetConferenceAsync(notExistingConferenceId);
            return _testResult.WithHttpResponse(response);
        }

        public async Task<TestResult> SignUp()
        {
            var response = await _api.SignUp(_signUpUser);
            return _testResult.WithHttpResponse(response);
        }

        public async Task<TestResult> GetSignedInUser()
        {
            var response = await _api.GetUser();
            return _testResult.WithHttpResponse(response);
        }
    }
}