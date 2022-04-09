using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Confab.Modules.Tests.Integrations.Builder.Api
{
    internal static class AttendancesModuleApi
    {
        private static readonly string AttendancesModule = "attendances-module";
        private static readonly string Attendances = $"{AttendancesModule}/attendances";

        public static Task<HttpResponseMessage> GetAttendancesConference(this HttpClient client, Guid conferenceId)
        {
            return client.GetAsync($"{Attendances}/{conferenceId}");
        }
    }
}