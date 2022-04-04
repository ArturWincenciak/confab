using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Confab.Modules.Users.Core.DTO;

namespace Confab.Modules.Attendances.Tests.Integrations.Builder.Api
{
    internal static class UsersModuleApi
    {
        private static readonly string UserModule = "users-module";
        private static readonly string Accounts = $"{UserModule}/accounts";
        private static readonly string SingUp = $"{Accounts}/sign-up";
        private static readonly string SingIn = $"{Accounts}/sign-in";

        internal static Task<HttpResponseMessage> SignUp(this HttpClient client, SignUpDto signUpUser)
        {
            return client.PostAsJsonAsync(SingUp, signUpUser);
        }

        internal static Task<HttpResponseMessage> GetUser(this HttpClient client)
        {
            return client.GetAsync(Accounts);
        }

        internal static Task<HttpResponseMessage> SignIn(this HttpClient client, SignInDto signInUser)
        {
            return client.PostAsJsonAsync(SingIn, signInUser);
        }
    }
}