using System;
using System.Net.Http;
using System.Threading.Tasks;
using Confab.Modules.Agendas.Api.Controllers;
using Confab.Modules.Conferences.Core.DTO;
using Confab.Modules.Users.Core.DTO;

namespace Confab.Tests.Integrations.Builder.Api
{
    internal class TestingApplication
    {
        private readonly TestResult _testResult;
        private readonly HttpClient _api;
        private readonly SignUpDto _signUpUser;
        private readonly HostDto _host;
        private readonly Uri _hostLocation;
        private readonly ConferenceDetailsDto _conference;
        private readonly Uri _conferenceLocation;
        private readonly AgendasController.CreateAgendaTrackCommand _track;

        internal TestingApplication(HttpClient api, SignUpDto signUpUser, HostDto host, Uri hostLocation,
            ConferenceDetailsDto conference, Uri conferenceLocation, AgendasController.CreateAgendaTrackCommand track)
        {
            _api = api;
            _signUpUser = signUpUser;
            _host = host;
            _hostLocation = hostLocation;
            _conference = conference;
            _conferenceLocation = conferenceLocation;
            _track = track;
            _testResult = new TestResult(signUpUser, host, conference);
        }

        internal async Task<TestResult> GetNotExistingConference()
        {
            var notExistingConferenceId = Guid.Parse("1E795B8E-A3F1-4E1A-BB94-435BC707F03C");
            var response = await _api.GetAttendancesConference(notExistingConferenceId);
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

        public async Task<TestResult> GetHost()
        {
            var response = await _api.Get(_hostLocation);
            return _testResult.WithHttpResponse(response);
        }

        public async Task<TestResult> CreateConference()
        {
            var response = await _api.CreateConference(_conference);
            return _testResult.WithHttpResponse(response);
        }

        public async Task<TestResult> GetConference()
        {
            var response = await _api.Get(_conferenceLocation);
            return _testResult.WithHttpResponse(response);
        }

        public async Task<TestResult> CreateTrack()
        {
            var response = await _api.CreateTrack(_conference.Id, _track);
            return _testResult.WithHttpResponse(response);
        }
    }
}