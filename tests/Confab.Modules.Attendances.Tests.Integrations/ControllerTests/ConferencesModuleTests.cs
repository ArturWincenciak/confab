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
            var target = await new TestBuilder()
                .WithAuthentication()
                .Build();

            // act
            var actual = await target.CreateHost();

            // arrange
            actual.ShouldBeCreated201();
        }
    }
}