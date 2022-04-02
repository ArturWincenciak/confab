using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Confab.Modules.Attendances.Tests.Integrations.Builder
{
    public static class TestApi
    {
        private static readonly string AttendancesModule = "attendances-module";
        private static readonly string AttendancesPath = $"{AttendancesModule}/attendances";

        private static readonly string UserModule = "users-module";
        private static readonly string AccountPath = $"{UserModule}/accounts";

        public static Task<HttpResponseMessage> GetConferenceAsync(this HttpClient client, Guid conferenceId)
        {
            return client.GetAsync($"{AttendancesPath}/{conferenceId}");
        }

        public static Task<HttpResponseMessage> CreateUserAsync(this HttpClient client)
        {
            var user = new
            {
                Email = "email@email.com",
                Password = "strict",
                Role = "user",
                Claims = new
                {
                    Permissions = new[]
                    {
                        "conferences", "hosts", "speakers", "users", "agendas", "cfps", "submissions", "ticket-sales"
                    }
                }
            };
            return client.PostAsJsonAsync(AccountPath, user);
        }
    }
}