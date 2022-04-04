using System.Threading.Tasks;
using Confab.Modules.Attendances.Tests.Integrations.Builder;
using Xunit;

namespace Confab.Modules.Attendances.Tests.Integrations.ControllerTests
{
    public class AttendancesControllerTests
    {
        [Fact]
        public async Task Given_Not_Authorized_Http_Client_When_Call_Api_Then_Response_Is_Unauthorized_Http_Status()
        {
            // arrange
            var target = await new TestBuilder()
                .Build();

            // act
            var actual = await target.GetNotExistingConference();

            // assert
            actual.ShouldBeUnauthorized401();
        }

        [Fact]
        public async Task Given_Not_Existing_Conference_Id_When_Get_Conference_Than_Not_Found_Http_Status()
        {
            // arrange
            var target = await new TestBuilder()
                .WithAuthentication()
                .Build();

            // act
            var actual = await target.GetNotExistingConference();

            // assert
            actual.ShouldBeNotFound404();
        }
    }
}