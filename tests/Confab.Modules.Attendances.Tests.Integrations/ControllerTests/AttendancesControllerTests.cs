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
            var target = new TestBuilder()
                .Build();

            // act
            var actual = await target.GetNotExistingConference();

            // assert
            actual.ShouldBeUnauthorized();
        }

        [Fact]
        public async Task Given_Not_Existing_Conference_Id_When_Get_Conference_Than_Not_Found_Http_Status()
        {
            // arrange
            var target = new TestBuilder()
                .WithAuthentication()
                .Build();

            // act
            var actual = await target.GetNotExistingConference();

            // assert
            actual.ShouldBeNotFound();
        }

        //TODO
        //[Fact]
        //public async Task get_browse_attendances_given_valid_conference_and_participant_should_return_all_attendances()
        //{
        //    var from = DateTime.UtcNow;
        //    var to = from.AddDays(1);
        //    var conferenceId = Guid.NewGuid();
        //    var userId = Guid.NewGuid();
        //    var participant = new Participant(Guid.NewGuid(), conferenceId, userId);
        //    var slot = new Slot(Guid.NewGuid(), participant.Id);
        //    var attendableEvent = new AttendableEvent(Guid.NewGuid(), conferenceId, from, to, new[] {slot});
        //    var attendance = new Attendance(Guid.NewGuid(), attendableEvent.Id, slot.Id, participant.Id, from, to);

        //    await _dbContext.AttendableEvents.AddAsync(attendableEvent);
        //    await _dbContext.Attendances.AddAsync(attendance);
        //    await _dbContext.Participants.AddAsync(participant);
        //    await _dbContext.Slots.AddAsync(slot);
        //    await _dbContext.SaveChangesAsync();

        //    _agendasApiClient.GetAgendaAsync(conferenceId).Returns(new List<AgendaTrackDto>
        //    {
        //        new()
        //        {
        //            Id = Guid.NewGuid(),
        //            Name = "Track 1",
        //            ConferenceId = conferenceId,
        //            Slots = new[]
        //            {
        //                new RegularAgendaSlotDto
        //                {
        //                    Id = Guid.NewGuid(),
        //                    From = from,
        //                    To = to,
        //                    AgendaItem = new AgendaItemDto
        //                    {
        //                        Id = attendableEvent.Id,
        //                        Title = "test",
        //                        Description = "test",
        //                        Level = 1
        //                    }
        //                }
        //            }
        //        }
        //    });

        //    Authenticate(userId);
        //    var response = await _client.GetAsync($"{Path}/{conferenceId}");
        //    response.IsSuccessStatusCode.ShouldBeTrue();

        //    var attendances = await response.Content.ReadFromJsonAsync<AttendanceDto[]>();
        //    attendances.ShouldNotBeEmpty();
        //    attendances.Length.ShouldBe(1);
        //}
    }
}