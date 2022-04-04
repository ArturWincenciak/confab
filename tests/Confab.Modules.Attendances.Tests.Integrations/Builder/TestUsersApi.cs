using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Confab.Modules.Users.Core.DTO;

namespace Confab.Modules.Attendances.Tests.Integrations.Builder
{
    public static class TestUsersApi
    {
        private static readonly string UserModule = "users-module";
        private static readonly string Accounts = $"{UserModule}/accounts";
        private static readonly string SingUp = $"{Accounts}/sign-up";
        private static readonly string SingIn = $"{Accounts}/sign-in";

        public static Task<HttpResponseMessage> SignUp(this HttpClient client, SignUpDto signUpDto)
        {
            return client.PostAsJsonAsync(SingUp, signUpDto);
        }

        public static Task<HttpResponseMessage> GetUser(this HttpClient client)
        {
            return client.GetAsync(Accounts);
        }

        public static Task<HttpResponseMessage> SignIn(this HttpClient client, SignInDto signInDto)
        {
            return client.PostAsJsonAsync(SingIn, signInDto);
        }
    }
}