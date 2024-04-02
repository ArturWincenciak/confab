using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Confab.Modules.Users.Core.DTO;

namespace Confab.Tests.Integrations.Builder.Api;

internal static class UsersModuleApi
{
    private readonly static string UserModule = "users-module";
    private readonly static string Accounts = $"{UserModule}/accounts";
    private readonly static string SingUp = $"{Accounts}/sign-up";
    private readonly static string SingIn = $"{Accounts}/sign-in";

    internal static Task<HttpResponseMessage> SignUp(this HttpClient client, SignUpDto signUpUser) =>
        client.PostAsJsonAsync(SingUp, signUpUser);

    internal static Task<HttpResponseMessage> GetUser(this HttpClient client) =>
        client.GetAsync(Accounts);

    internal static Task<HttpResponseMessage> SignIn(this HttpClient client, SignInDto signInUser) =>
        client.PostAsJsonAsync(SingIn, signInUser);
}