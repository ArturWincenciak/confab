using System.Threading.Tasks;
using Xunit;

namespace Confab.Modules.Tests.Integrations.ControllerTests
{
    public class AttendancesModuleTest : ModuleTests
    {
        [Fact]
        internal async Task Given_Not_Authorized_Http_Client_When_Call_Api_Then_Response_Is_Unauthorized_Http_Status()
        {
            // arrange
            var target = await TestBuilder
                .Build();

            // act
            var actual = await target.GetNotExistingConference();

            // assert
            actual.ShouldBeUnauthorized401();
        }

        [Fact]
        internal async Task Given_Not_Existing_Conference_Id_When_Get_Conference_Than_Not_Found_Http_Status()
        {
            // arrange
            var target = await TestBuilder
                .WithAuthentication()
                .Build();

            // act
            var actual = await target.GetNotExistingConference();

            // assert
            actual.ShouldBeNotFound404();
        }
    }
}