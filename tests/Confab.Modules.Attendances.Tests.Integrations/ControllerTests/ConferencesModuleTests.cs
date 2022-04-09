using System.Threading.Tasks;
using Confab.Modules.Attendances.Tests.Integrations.Builder;
using Xunit;

namespace Confab.Modules.Attendances.Tests.Integrations.ControllerTests
{
    public class ConferencesModuleTests
    {
        [Fact]
        public async Task Given_App_When_Create_Host_Then_Created200()
        {
            // arrange
            using var target = await new TestBuilder()
                .WithAuthentication()
                .Build();

            // act
            var actual = await target.CreateHost();

            // assert
            actual.ShouldBeCreated201();
        }

        [Fact]
        public async Task Given_Host_When_Get_The_Host_Then_Ok200()
        {
            // arrange
            using var target = await new TestBuilder()
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
        public async Task Given_Host_When_Create_Conference_Then_Created201()
        {
            // arrange
            using var target = await new TestBuilder()
                .WithAuthentication()
                .WithHost()
                .Build();

            // act
            var actual = await target.CreateConference();

            // assert
            actual.ShouldBeCreated201();
        }

        [Fact]
        public async Task Given_Conference_When_Get_The_Conference_Then_Ok200()
        {
            // arrange
            using var target = await new TestBuilder()
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