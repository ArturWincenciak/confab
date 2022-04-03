using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Confab.Modules.Attendances.Tests.Integrations.Builder.Api
{
    public static class TestAttendancesApi
    {
        private static readonly string AttendancesModule = "attendances-module";
        private static readonly string Attendances = $"{AttendancesModule}/attendances";

        public static Task<HttpResponseMessage> GetConferenceAsync(this HttpClient client, Guid conferenceId)
        {
            return client.GetAsync($"{Attendances}/{conferenceId}");
        }
    }
}