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

        [Fact]
        public async Task Given_Host_When_Get_The_Host_Then_Ok200()
        {
            // arrange
            var target = await new TestBuilder()
                .WithAuthentication()
                .WithCreatedHost()
                .Build();

            // act
            var actual = await target.GetHost();

            // arrange
            actual.ShouldBeOk200();
            await actual.HostShouldBeAsAlreadyCreated();
        }
    }
}