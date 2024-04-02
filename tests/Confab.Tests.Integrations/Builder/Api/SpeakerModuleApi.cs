using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Confab.Modules.Speakers.Core.DTO;

namespace Confab.Tests.Integrations.Builder.Api;

internal static class SpeakerModuleApi
{
    private readonly static string SpeakersModule = "speakers-module";
    private readonly static string Speakers = $"{SpeakersModule}/speakers";

    public static Task<HttpResponseMessage> CreateSpeaker(this HttpClient client, SpeakerDto speakerDto) =>
        client.PostAsJsonAsync(Speakers, speakerDto);
}