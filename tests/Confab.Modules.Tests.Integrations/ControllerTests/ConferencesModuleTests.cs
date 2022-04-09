using System.Threading.Tasks;
using Xunit;

namespace Confab.Modules.Tests.Integrations.ControllerTests
{
    public class ConferencesModuleTests : ModuleTests
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
        internal async Task Given_Host_When_Get_The_Host_Then_Ok200()
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
            await actual.HostShouldBeCreatedProperly();
        }

        [Fact]
        internal async Task Given_Host_When_Create_Conference_Then_Created201()
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
        internal async Task Given_Conference_When_Get_The_Conference_Then_Ok200()
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
            await actual.ConferenceShouldBeCreatedProperly();
        }
    }
}