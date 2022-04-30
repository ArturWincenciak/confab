using System.Linq;
using System.Threading.Tasks;
using NSubstitute.Routing.Handlers;
using Shouldly;
using Xunit;

namespace Confab.Tests.Integrations.ControllerTests
{
    public class AgendasModuleTests : ModuleIntegrationTests
    {
        [Fact]
        internal async Task Given_Conference_When_Create_In_The_Conference_Then_NoContent204()
        {
            // arrange
            var target = await TestBuilder
                .WithAuthentication()
                .WithHost()
                .WithConference()
                .Build();

            // act
            var actual = await target.CreateTrack();

            // arrange
            actual.ShouldBeNoContent204();
        }

        [Fact]
        internal async Task Given_Host_With_Conference_With_Track_When_Get_The_Already_Defined_Track_Then_Ok200()
        {
            // arrange
            var target = await TestBuilder
                .WithAuthentication()
                .WithHost()
                .WithConference()
                .WithTrack()
                .Build();

            // act
            var actual = await target.GetTrack();

            // arrange
            actual.ShouldBeOk200();
            await actual.TrackShouldContainExpectedProperties(assert =>
            {
                assert.ActaulResult.Id.ShouldBe(assert.Expected.AlreadyCreatedTrackId);
                assert.ActaulResult.ConferenceId.ShouldBe(assert.Expected.AlreadyCreatedConferenceId);
                assert.ActaulResult.Name.ShouldBe(assert.Expected.CreateAgendaTrackCommand.Name);
                assert.ActaulResult.Slots.Count().ShouldBe(0);
            });
        }

        [Fact]
        internal async Task Given_Track_When_Create_Slot_Then_NotContent204()
        {
            // arrange
            var target = await TestBuilder
                .WithAuthentication()
                .WithHost()
                .WithConference()
                .WithTrack()
                .Build();

            // act
            var actual = await target.CreateRegularSlot();

            // assert
            actual.ShouldBeNoContent204();
        }
    }
}