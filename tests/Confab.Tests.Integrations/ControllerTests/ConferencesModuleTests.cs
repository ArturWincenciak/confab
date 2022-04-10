using System;
using System.Threading.Tasks;
using Shouldly;
using Xunit;

namespace Confab.Tests.Integrations.ControllerTests
{
    public class ConferencesModuleTests : ModuleIntegrationTests
    {
        [Fact]
        internal async Task Given_App_When_Create_Host_Then_Created200()
        {
            // arrange
            var target = await TestBuilder
                .WithAuthentication()
                .Build();

            // act
            var actual = await target.CreateHost();

            // assert
            actual.ShouldBeCreated201();
        }

        [Fact]
        internal async Task Given_Host_When_Get_The_Already_Defined_Host_Then_Ok200()
        {
            // arrange
            var target = await TestBuilder
                .WithAuthentication()
                .WithHost()
                .Build();

            // act
            var actual = await target.GetHost();

            // assert
            actual.ShouldBeOk200();
            await actual.HostShouldContainExpectedProperties(assert =>
            {
                assert.ActualResult.Id.ShouldBe(assert.Expected.AlreadyCreatedHostId);
                assert.ActualResult.Conferences.Count.ShouldBe(0);
                assert.ActualResult.Name.ShouldBe(assert.Expected.InputHostDto.Name);
                assert.ActualResult.Description.ShouldBe(assert.Expected.InputHostDto.Description);
            });
        }

        [Fact]
        internal async Task Given_Host_When_Create_Conference_In_The_Host_Then_Created201()
        {
            // arrange
            var target = await TestBuilder
                .WithAuthentication()
                .WithHost()
                .Build();

            // act
            var actual = await target.CreateConference();

            // assert
            actual.ShouldBeCreated201();
        }

        [Fact]
        internal async Task Given_Host_With_Conference_When_Get_The_Already_Defined_Conference_Then_Ok200()
        {
            // arrange
            var target = await TestBuilder
                .WithAuthentication()
                .WithHost()
                .WithConference()
                .Build();

            // act
            var actual = await target.GetConference();

            // assert
            actual.ShouldBeOk200();
            await actual.ConferenceShouldContainExpectedProperties((assert) =>
            {
                assert.ActualResult.Id.ShouldBe(assert.Expected.AlreadyCreatedConferenceId);
                assert.ActualResult.HostId.ShouldBe(assert.Expected.AlreadyCreatedHostId);
                assert.ActualResult.HostName.ShouldBe(assert.Expected.InputHostDto.Name);
                assert.ActualResult.Name.ShouldBe(assert.Expected.InputConferenceDto.Name);
                assert.ActualResult.Description.ShouldBe(assert.Expected.InputConferenceDto.Description);
                assert.ActualResult.From.ShouldBe(assert.Expected.InputConferenceDto.From);
                assert.ActualResult.To.ShouldBe(assert.Expected.InputConferenceDto.To);
                assert.ActualResult.Localization.ShouldBe(assert.Expected.InputConferenceDto.Localization);
                assert.ActualResult.LogoUrl.ShouldBe(assert.Expected.InputConferenceDto.LogoUrl);
                assert.ActualResult.ParticipantsLimit.ShouldBe(assert.Expected.InputConferenceDto.ParticipantsLimit);
            });
        }
    }
}