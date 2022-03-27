using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Confab.Modules.Attendances.Tests.Integrations
{
    public static class TestApi
    {
        private const string AttendancesPath = "attendances-module/attendances";

        public static Task<HttpResponseMessage> GetConferenceAsync(this HttpClient client, Guid conferenceId)
        {
            return client.GetAsync($"{AttendancesPath}/{conferenceId}");
        }
    }
}