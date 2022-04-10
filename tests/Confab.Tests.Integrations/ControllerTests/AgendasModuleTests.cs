using System.Threading.Tasks;
using Xunit;

namespace Confab.Tests.Integrations.ControllerTests
{
    public class AgendasModuleTests : ModuleIntegrationTests
    {
        [Fact]
        internal async Task Given_Conference_When_Create_In_The_Conference_The_NoContent204()
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
    }
}