namespace Confab.Modules.Saga.Services;

internal interface IInvitationSpeakers
{
    bool IsInvited(string email);
    string FullName(string email);
    string SpeakerBio(string email);
}