using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Confab.Modules.Agendas.Api.Controllers;
using Confab.Modules.Agendas.Application.Agendas.Commands;

namespace Confab.Tests.Integrations.Builder.Api
{
    internal static class AgendasModuleApi
    {
        private static readonly string AgendasModule = "agendas-module";
        private static readonly string Agendas = $"{AgendasModule}/agendas";
        private static string Tracks(Guid conferenceId) => $"{Agendas}/{conferenceId}/tracks";
        private static string Slots(Guid conferenceId) => $"{Agendas}/{conferenceId}/slots";

        internal static Task<HttpResponseMessage> CreateTrack(this HttpClient client, Guid conferenceId,
            AgendasController.CreateAgendaTrackCommand createTrackCommand)
        {
            return client.PostAsJsonAsync(Tracks(conferenceId), createTrackCommand);
        }

        internal static Task<HttpResponseMessage> GetTrack(this HttpClient client, Guid conferenceId, Guid trackId)
        {
            return client.GetAsync($"{Tracks(conferenceId)}/{trackId}");
        }

        internal static Task<HttpResponseMessage> CreateSlot(this HttpClient client, Guid conferenceId,
            CreateAgendaSlot createSlotCommand)
        {
            return client.PostAsJsonAsync(Slots(conferenceId), createSlotCommand);
        }
    }
}