using System.Threading.Tasks;
using Xunit;

namespace Confab.Tests.Integrations.ControllerTests
{
    public class SpeakerModuleTests : ModuleIntegrationTests
    {
        [Fact]
        internal async Task Given_Authentication_User_When_Create_Speaker_Then_Created201()
        {
            // arrange
            var target = await TestBuilder
                .WithAuthentication()
                .Build();

            // act
            var actual = await target.CreateSpeaker();

            // assert
            actual.ShouldBeCreated201();
        }
    }
}