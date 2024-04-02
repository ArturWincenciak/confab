using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Confab.Modules.Conferences.Core.DTO;

namespace Confab.Tests.Integrations.Builder.Api;

internal static class ConferencesModuleApi
{
    private readonly static string UserModule = "conferences-module";
    private readonly static string Hosts = $"{UserModule}/hosts";
    private readonly static string Conferences = $"{UserModule}/conferences";

    internal static Task<HttpResponseMessage> CreateHost(this HttpClient client, HostDto host) =>
        client.PostAsJsonAsync(Hosts, host);

    internal static Task<HttpResponseMessage> Get(this HttpClient client, Uri location) =>
        client.GetAsync(location);

    internal static Task<HttpResponseMessage> CreateConference(this HttpClient client,
        ConferenceDetailsDto conference) =>
        client.PostAsJsonAsync(Conferences, conference);
}