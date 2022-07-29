using System.Collections.Generic;

namespace Confab.Modules.Saga.Services
{
    internal static class InvitedSpeakersStub
    {
        public static readonly Dictionary<string, string> InvitedSpeaker = new()
        {
            {"testspeaker1@confab.io", "John Smith"},
            {"testspeaker2@confab.io", "Mark Sim"}
        };
    }
}