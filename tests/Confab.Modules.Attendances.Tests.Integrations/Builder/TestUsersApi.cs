using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Confab.Modules.Attendances.Tests.Integrations.Builder
{
    public static class TestUsersApi
    {
        private static readonly string UserModule = "users-module";
        private static readonly string Accounts = $"{UserModule}/accounts";
        private static readonly string SingUn = $"{Accounts}/sign-up";

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
            return client.PostAsJsonAsync(SingUn, user);
        }
    }
}