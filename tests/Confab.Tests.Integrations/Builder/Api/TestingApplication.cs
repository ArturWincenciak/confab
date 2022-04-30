using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Confab.Modules.Agendas.Api.Controllers;
using Confab.Modules.Agendas.Application.Agendas.Commands;
using Confab.Modules.Conferences.Core.DTO;
using Confab.Modules.Users.Core.DTO;

namespace Confab.Tests.Integrations.Builder.Api
{
    internal class TestingApplication
    {
        private readonly HttpClient _api;
        private readonly Uri _createdConferenceLocation;
        private readonly Uri _createdHostLocation;
        private readonly ConferenceDetailsDto _inputConferenceDto;
        private readonly HostDto _inputHostDto;
        private readonly SignUpDto _signUpUser;
        private readonly TestResult _testResult;
        private readonly AgendasController.CreateAgendaTrackCommand _createTrackCommand;
        private readonly CreateAgendaSlot _createAgendaSlotCommand;
        private readonly string _createdTrackResourceId;

        internal TestingApplication(HttpClient api, SignUpDto signUpUser, HostDto inputHostDto,
            Uri createdHostLocation, ConferenceDetailsDto inputConferenceDto, Uri createdConferenceLocation,
            AgendasController.CreateAgendaTrackCommand createTrackCommand, string createdTrackResourceId,
            CreateAgendaSlot createAgendaSlotCommand)
        {
            _api = api;
            _signUpUser = signUpUser;
            _inputHostDto = inputHostDto;
            _createdHostLocation = createdHostLocation;
            _inputConferenceDto = inputConferenceDto;
            _createdConferenceLocation = createdConferenceLocation;
            _createTrackCommand = createTrackCommand;
            _createdTrackResourceId = createdTrackResourceId;
            _createAgendaSlotCommand = createAgendaSlotCommand;
            _testResult = new TestResult(
                signUpUser,
                inputHostDto,
                ResolveHostId(),
                inputConferenceDto,
                createTrackCommand,
                ResolveConferenceId(),
                ResolveTrackId(createdTrackResourceId));
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
            var response = await _api.CreateHost(_inputHostDto);
            return _testResult.WithHttpResponse(response);
        }

        public async Task<TestResult> GetHost()
        {
            var response = await _api.Get(_createdHostLocation);
            return _testResult.WithHttpResponse(response);
        }

        public async Task<TestResult> CreateConference()
        {
            var response = await _api.CreateConference(_inputConferenceDto);
            return _testResult.WithHttpResponse(response);
        }

        public async Task<TestResult> GetConference()
        {
            var response = await _api.Get(_createdConferenceLocation);
            return _testResult.WithHttpResponse(response);
        }

        public async Task<TestResult> CreateTrack()
        {
            var conferenceId = ResolveConferenceId();
            var response = await _api.CreateTrack(_inputConferenceDto.Id, _createTrackCommand);
            return _testResult.WithHttpResponse(response);
        }

        public async Task<TestResult> GetTrack()
        {
            var conferenceId = Guid.Parse(_createdConferenceLocation.Segments.Last());
            var trackId = Guid.Parse(_createdTrackResourceId);
            var response = await _api.GetTrack(conferenceId, trackId);
            return _testResult.WithHttpResponse(response);
        }

        public async Task<TestResult> CreateRegularSlot()
        {
            var conferenceId = Guid.Parse(_createdConferenceLocation.Segments.Last());
            var response = await _api.CreateSlot(conferenceId, _createAgendaSlotCommand);
            return _testResult.WithHttpResponse(response);
        }

        private Guid ResolveConferenceId()
        {
            return ResolveIdByLocationUrl(_createdConferenceLocation);
        }

        private Guid ResolveHostId()
        {
            return ResolveIdByLocationUrl(_createdHostLocation);
        }

        private Guid ResolveIdByLocationUrl(Uri location)
        {
            if(location is null)
                return Guid.Empty;

            return Guid.Parse(location.Segments.Last());
        }

        private static Guid ResolveTrackId(string createdTrackResourceId)
        {
            if(createdTrackResourceId is null)
                return Guid.Empty;

            return Guid.Parse(createdTrackResourceId);
        }
    }
}