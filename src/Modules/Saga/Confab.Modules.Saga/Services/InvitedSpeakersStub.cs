namespace Confab.Modules.Saga.Services
{
    internal static class InvitedSpeakersStub
    {
        public static bool IsInvited(string email) =>
            email.Contains("invited");

        public static string FullName(string email) =>
            $"Stub of full name for speaker with email {email}";

        public static string SpeakerBio(string email) =>
            $"Lorem ipsum for speaker with email {email}.";
    }
}