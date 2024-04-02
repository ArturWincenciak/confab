using System.Threading.Tasks;
using Xunit;

namespace Confab.Tests.Integrations.ControllerTests;

public class UsersModuleTest : ModuleIntegrationTests
{
    [Fact]
    internal async Task Given_Just_App_When_Create_User_Then_Created()
    {
        // arrange
        var target = await TestBuilder
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
        var target = await TestBuilder
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