using System;
using System.Net.Http;
using System.Threading.Tasks;
using Confab.Modules.Conferences.Core.DTO;
using Confab.Modules.Users.Core.DTO;

namespace Confab.Modules.Attendances.Tests.Integrations.Builder.Api
{
    internal class TestingApplication
    {
        private readonly TestResult _testResult;
        private readonly HttpClient _api;
        private readonly SignUpDto _signUpUser;
        private readonly HostDto _host;

        internal TestingApplication(HttpClient api, SignUpDto signUpUser, HostDto host)
        {
            _api = api;
            _signUpUser = signUpUser;
            _host = host;
            _testResult = new TestResult(signUpUser);
        }

        internal async Task<TestResult> GetNotExistingConference()
        {
            var notExistingConferenceId = Guid.Parse("1E795B8E-A3F1-4E1A-BB94-435BC707F03C");
            var response = await _api.GetConferenceAsync(notExistingConferenceId);
            return _testResult.WithHttpResponse(response);
        }

        internal async Task<TestResult> SignUp()
        {
            var response = await _api.SignUp(_signUpUser);
            return _testResult.WithHttpResponse(response);
        }

        internal async Task<TestResult> GetSignedInUser()
        {
            var response = await _api.GetUser();
            return _testResult.WithHttpResponse(response);
        }

        internal async Task<TestResult> CreateHost()
        {
            var response = await _api.CreateHost(_host);
            return _testResult.WithHttpResponse(response);
        }
    }
}