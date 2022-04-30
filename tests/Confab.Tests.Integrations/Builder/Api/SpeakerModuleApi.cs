using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Confab.Modules.Speakers.Core.DTO;

namespace Confab.Tests.Integrations.Builder.Api
{
    internal static class SpeakerModuleApi
    {
        private static readonly string SpeakersModule = "speakers-module";
        private static readonly string Speakers = $"{SpeakersModule}/speakers";

        public static Task<HttpResponseMessage> CreateSpeaker(this HttpClient client, SpeakerDto speakerDto)
        {
            return client.PostAsJsonAsync(Speakers, speakerDto);
        }
    }
}