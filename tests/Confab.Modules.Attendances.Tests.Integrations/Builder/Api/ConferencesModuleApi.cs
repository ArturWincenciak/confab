using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Confab.Modules.Conferences.Core.DTO;

namespace Confab.Modules.Attendances.Tests.Integrations.Builder.Api
{
    internal static class ConferencesModuleApi
    {
        private static readonly string UserModule = "conferences-module";
        private static readonly string Hosts = $"{UserModule}/hosts";

        internal static Task<HttpResponseMessage> CreateHost(this HttpClient client, HostDto host)
        {
            return client.PostAsJsonAsync(Hosts, host);
        }
    }
}