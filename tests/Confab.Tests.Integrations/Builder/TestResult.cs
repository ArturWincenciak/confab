using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Confab.Modules.Agendas.Api.Controllers;
using Confab.Modules.Agendas.Application.Agendas.Queries;
using Confab.Modules.Conferences.Core.DTO;
using Confab.Modules.Users.Core.DTO;
using Shouldly;
using Xunit;

namespace Confab.Tests.Integrations.Builder;

internal class TestResult
{
    private readonly Guid _createdConferenceId;
    private readonly Guid _createdHostId;
    private readonly Guid _createdTrackId;

    private readonly ConferenceDetailsDto _inputConferenceDto;
    private readonly HostDto _inputHostDto;
    private readonly AgendasController.CreateAgendaTrackCommand _inputTrackDto;
    private readonly SignUpDto _signUpUser;
    private HttpResponseMessage _httpResponse;

    internal TestResult(SignUpDto signUpUser, HostDto inputHostDto, Guid createdHostId,
        ConferenceDetailsDto inputConferenceDto, AgendasController.CreateAgendaTrackCommand inputTrackDto,
        Guid createdConferenceId, Guid createdTrackId)
    {
        _signUpUser = signUpUser;
        _inputHostDto = inputHostDto;
        _createdHostId = createdHostId;
        _inputConferenceDto = inputConferenceDto;
        _inputTrackDto = inputTrackDto;
        _createdConferenceId = createdConferenceId;
        _createdTrackId = createdTrackId;
    }

    internal TestResult WithHttpResponse(HttpResponseMessage httpResponse)
    {
        _httpResponse = httpResponse;
        return this;
    }

    private async Task<T> Response<T>() =>
        await _httpResponse.Content.ReadFromJsonAsync<T>();

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
        var signedUpUser = await Response<AccountDto>();
        Assert.Equal(_signUpUser.Email, signedUpUser.Email);
        Assert.Equal(_signUpUser.Role, signedUpUser.Role);
        Assert.Equal(_signUpUser.Claims, signedUpUser.Claims);
    }

    internal async Task HostShouldContainExpectedProperties(
        Action<(HostDetailsDto ActualResult, (
            HostDto InputHostDto, Guid AlreadyCreatedHostId) Expected)> assert)
    {
        var actualResult = await Response<HostDetailsDto>();
        assert((actualResult, (_inputHostDto, _createdHostId)));
    }

    internal async Task ConferenceShouldContainExpectedProperties(
        Action<(ConferenceDetailsDto ActualResult, (
            ConferenceDetailsDto InputConferenceDto,
            HostDto InputHostDto,
            Guid AlreadyCreatedConferenceId,
            Guid AlreadyCreatedHostId) Expected)> assert)
    {
        var actualResult = await Response<ConferenceDetailsDto>();
        assert((actualResult, (_inputConferenceDto, _inputHostDto, _createdConferenceId, _createdHostId)));
    }

    internal async Task TrackShouldContainExpectedProperties(
        Action<(GetAgendaTrack.Result ActaulResult, (
            AgendasController.CreateAgendaTrackCommand CreateAgendaTrackCommand,
            Guid AlreadyCreatedConferenceId, Guid AlreadyCreatedTrackId) Expected)> assert)
    {
        var actualResult = await Response<GetAgendaTrack.Result>();
        assert((actualResult, (_inputTrackDto, _createdConferenceId, _createdTrackId)));
    }
}