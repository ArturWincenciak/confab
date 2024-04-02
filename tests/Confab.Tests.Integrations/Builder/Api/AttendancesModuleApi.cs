using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Confab.Tests.Integrations.Builder.Api;

internal static class AttendancesModuleApi
{
    private readonly static string AttendancesModule = "attendances-module";
    private readonly static string Attendances = $"{AttendancesModule}/attendances";

    public static Task<HttpResponseMessage> GetAttendancesConference(this HttpClient client, Guid conferenceId) =>
        client.GetAsync($"{Attendances}/{conferenceId}");
}