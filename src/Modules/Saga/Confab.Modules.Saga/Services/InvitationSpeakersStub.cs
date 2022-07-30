namespace Confab.Modules.Saga.Services
{
    internal class InvitationSpeakersStub : IInvitationSpeakers
    {
        public bool IsInvited(string email) =>
            email.Contains("invited");

        public string FullName(string email) =>
            $"Stub of full name for speaker with email {email}";

        public string SpeakerBio(string email) =>
            $"Lorem ipsum for speaker with email {email}.";
    }
}