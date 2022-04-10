using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Confab.Modules.Agendas.Api.Controllers;

namespace Confab.Tests.Integrations.Builder.Api
{
    internal static class AgendasModuleApi
    {
        private static readonly string AgendasModule = "agendas-module";
        private static readonly string Agendas = $"{AgendasModule}/agendas";
        private static string Tracks(Guid conferenceId) => $"{Agendas}/{conferenceId}/tracks";

        internal static Task<HttpResponseMessage> CreateTrack(this HttpClient client, Guid conferenceId,
            AgendasController.CreateAgendaTrackCommand track)
        {
            return client.PostAsJsonAsync(Tracks(conferenceId), track);
        }

        internal static Task<HttpResponseMessage> GetTrack(this HttpClient client, Guid conferenceId, Guid trackId)
        {
            return client.GetAsync($"{Tracks(conferenceId)}/{trackId}");
        }
    }
}