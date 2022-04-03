using System.Threading.Tasks;
using Confab.Modules.Attendances.Tests.Integrations.Builder;
using Xunit;

namespace Confab.Modules.Attendances.Tests.Integrations.ControllerTests
{
    public class AccountsControllerTests
    {
        [Fact]
        public async Task Given_Just_App_When_Create_User_Then_Created()
        {
            // arrange
            using var target = new TestBuilder()
                .WithEnsureDatabaseDeleted()
                .Build();

            // act
            var actual = await target.CreateUser();

            // assert
            actual.ShouldBeNoContent204();
        }
    }
}