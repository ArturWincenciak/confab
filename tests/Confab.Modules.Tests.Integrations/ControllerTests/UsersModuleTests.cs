using System.Threading.Tasks;
using Confab.Modules.Tests.Integrations.Builder;
using Xunit;

namespace Confab.Modules.Tests.Integrations.ControllerTests
{
    public class UsersModuleTests
    {
        [Fact]
        internal async Task Given_Just_App_When_Create_User_Then_Created()
        {
            // arrange
            using var target = await new TestBuilder()
                .Build();

            // act
            var actual = await target.SignUp();

            // assert
            actual.ShouldBeNoContent204();
        }

        [Fact]
        internal async Task Given_Signed_In_User_When_Get_His_Details_Then_Ok200()
        {
            // arrange
            using var target = await new TestBuilder()
                .WithSignUp()
                .WithSignIn()
                .Build();

            // act
            var actual = await target.GetSignedInUser();

            // assert
            actual.ShouldBeOk200();
            await actual.SignedInUserShouldBeSignedUpUser();
        }
    }
}