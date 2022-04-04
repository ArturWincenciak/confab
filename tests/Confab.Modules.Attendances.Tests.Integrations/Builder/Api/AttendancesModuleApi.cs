using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Confab.Modules.Attendances.Tests.Integrations.Builder.Api
{
    internal static class AttendancesModuleApi
    {
        private static readonly string AttendancesModule = "attendances-module";
        private static readonly string Attendances = $"{AttendancesModule}/attendances";

        public static Task<HttpResponseMessage> GetConference(this HttpClient client, Guid conferenceId)
        {
            return client.GetAsync($"{Attendances}/{conferenceId}");
        }
    }
}